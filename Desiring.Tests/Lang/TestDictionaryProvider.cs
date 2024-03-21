using Languager;

namespace Desiring.Tests.Lang
{
    public class TestDictionaryProvider : DictionaryProvider
    {
        protected override IDictionaryLoader Spanish => new SpanishDictionaryLoader();
    }
}
