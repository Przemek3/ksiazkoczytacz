//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace konsolka
{
    using System;
    using System.Collections.Generic;

    public partial class doNauczenia
    {
        public int Id { get; set; }
        public string polski { get; set; }
        public string angielski { get; set; }
        public int liczbaDobrych { get; set; }
        public Nullable<bool> czyNauczone { get; set; }

        public doNauczenia()
        {
            
        }
        public doNauczenia(int i, string ang, string pl, int lDobrych)
        {
            Id = i;
            angielski = ang;
            polski = pl;
            liczbaDobrych = lDobrych;
            czyNauczone = false;
        }
    }
}
