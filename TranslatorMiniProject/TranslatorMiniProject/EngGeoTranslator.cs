using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorMiniProject
{
    internal class EngGeoTranslator : LanguageTranslator
    {
        public EngGeoTranslator(Dictionary<string, string> dict) : base(dict) { }

        public override string Translate(string text)
        {
            return dictionary.ContainsKey(text) ? dictionary[text] : null;
        }
    }
}
