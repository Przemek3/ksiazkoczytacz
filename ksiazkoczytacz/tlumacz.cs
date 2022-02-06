using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ksiazkoczytacz
{
    class tlumacz
    {
        public string odpowiedz = "";
        private static readonly string subscriptionKey = "4eb203cd817b4324af90d4c4b9665b12";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";
        private string plikZTlumaczeniem = @"C:\Users\pkolo\Documents\zTlumaczeniem.txt";
        private string przetlumaczoneZksiazki = @"C:\Users\pkolo\Documents\PrzetlumaczoneZ.txt";

        // Add your location, also known as region. The default is global.
        // This is required if using a Cognitive Services resource.
        private static readonly string location = "global";

        public async Task Translate()
        {
            // Input and output languages are defined as parameters.
            string route = "/translate?api-version=3.0&from=en&to=pl";
            string textToTranslate = "poster";
            object[] body = new object[] { new { Text = textToTranslate } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(result);
                //odpowiedz = result;
                TranslationResult[] deserializedOutput = JsonConvert.DeserializeObject<TranslationResult[]>(result);
                foreach (TranslationResult o in deserializedOutput)

                    odpowiedz = deserializedOutput[0].Translations[0].Text;
            }
        }
        public class TranslationResult
        {
            public DetectedLanguage DetectedLanguage { get; set; }
            public TextResult SourceText { get; set; }
            public Translation[] Translations { get; set; }
        }
        public class DetectedLanguage
        {
            public string Language { get; set; }
            public float Score { get; set; }
        }

        public class TextResult
        {
            public string Text { get; set; }
            public string Script { get; set; }
        }

        public class Translation
        {
            public string Text { get; set; }
            public TextResult Transliteration { get; set; }
            public string To { get; set; }
            public Alignment Alignment { get; set; }
            public SentenceLength SentLen { get; set; }
        }

        public class Alignment
        {
            public string Proj { get; set; }
        }

        public class SentenceLength
        {
            public int[] SrcSentLen { get; set; }
            public int[] TransSentLen { get; set; }
        }

        public void TlumaczZPliku(List<string> zKsiazki)
        {
            Encoding enc = Encoding.GetEncoding("iso -8859-2");
            string tlumaczenie;
            using (StreamReader inputFile = new StreamReader(plikZTlumaczeniem, enc))
            {
                using (StreamWriter outputFile = new StreamWriter(przetlumaczoneZksiazki))
                {
                    for (int x = 0; x < zKsiazki.Count;)
                    {
                        tlumaczenie = inputFile.ReadLine();
                        switch (String.Compare(zKsiazki[x], tlumaczenie.Split(' ')[0]))
                        {
                            case -1:
                                x = x + 1;
                                break;
                            case 0:
                                outputFile.WriteLine(tlumaczenie);
                                x = x + 1;
                                break;
                            case 1:

                                break;
                        }
                    }
                }

            }
        }
    }
}
