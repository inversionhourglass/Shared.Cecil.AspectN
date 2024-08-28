using Cecil.AspectN.Patterns;
using Cecil.AspectN.Patterns.Parsers;

namespace Cecil.AspectN.Matchers
{
    public class TypeMatcher : ITypeMatcher
    {
        private readonly ITypePattern _typePattern;

        public TypeMatcher(string pattern)
        {
            _typePattern = TypeParser.Parse(pattern);
        }

        public TypeMatcher(ITypePattern typePattern)
        {
            _typePattern = typePattern;
        }

        public bool IsMatch(TypeSignature signature)
        {
            return _typePattern.IsMatch(signature);
        }
    }
}
