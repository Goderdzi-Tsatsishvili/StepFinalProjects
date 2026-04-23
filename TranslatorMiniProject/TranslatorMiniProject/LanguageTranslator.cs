using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorMiniProject
{
    abstract class LanguageTranslator
    {
        protected Dictionary<string, string> dictionary;

        public LanguageTranslator(Dictionary<string, string> dict)
        {
            dictionary = dict;
        }

        public abstract string Translate(string text);
    }
}
