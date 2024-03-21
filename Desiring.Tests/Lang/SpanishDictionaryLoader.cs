using Languager;

namespace Desiring.Tests.Lang
{
    public class SpanishDictionaryLoader : IDictionaryLoader
    {
        public IEnumerable<Word> Words => new List<Word>
        {
            new Word("storylet_description", "Storylet description"),

            new Word("storylet_1_interaction_description", "Storylet 1 description"),
            new Word("storylet_11_interaction_description", "Storylet 11 description"),
            new Word("storylet_12_interaction_description", "Storylet 12 description")
        };
    }
}
