using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ksiazkoczytacz;

namespace testy1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIng()
        {
            testWyrazow("doing", "do");
            testWyrazow("stalking", "stalk");
            testWyrazow("stalker", "stalk");
            testWyrazow("making", "make");
            testWyrazow("having", "have");
            testWyrazow("dying", "die");
        }
        [TestMethod]
        public void TestEd()
        {
            testWyrazow("weed", "we");
            testWyrazow("stalked", "stalk");
            testWyrazow("died", "die");
            testWyrazow("had", "have");
            testWyrazow("hurried", "hurry");
        }
        [TestMethod]
        public void TestStopniowania()
        {
            testWyrazow("darker", "dark");
            testWyrazow("smarter", "smart");
            testWyrazow("hotter", "hot");
            testWyrazow("shyer", "shy");
            testWyrazow("drier", "dry");
            testWyrazow("driest", "dry");
            testWyrazow("safer", "safe");
            testWyrazow("safer", "safest");
            testWyrazow("happier", "happy");
            testWyrazow("happiest", "happy");
            testWyrazow("cleverer", "clever");
        }
        [TestMethod]
        public void TestLiczbyMnogiej()
        {
            testWyrazow("wives", "wife");
            testWyrazow("wolves", "wolf");
            testWyrazow("boys", "boy");
            testWyrazow("kisses", "kiss");
            testWyrazow("kisss", "kiss");
            testWyrazow("churches", "church");
        }
        
        private void testWyrazow(string zKoncowka, string bezKoncowki)
        {
            if (kontrolaKoncowek.czyWyrazyPasuja(zKoncowka, bezKoncowki))
            {
                Console.WriteLine(zKoncowka + "\tTak");
            }
            else Console.WriteLine(zKoncowka + "\tNie");
        }
    }
}
