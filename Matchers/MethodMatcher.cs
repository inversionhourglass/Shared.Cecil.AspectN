using Cecil.AspectN.Patterns.Parsers;
using Cecil.AspectN.Patterns;

namespace Cecil.AspectN.Matchers
{
    public class MethodMatcher : IMatcher
    {
        private readonly string _pattern;
        private readonly MethodPattern _methodPattern;

        public MethodMatcher(string pattern)
        {
            _pattern = pattern;
            _methodPattern = MethodPatternParser.Parse(pattern);
            DeclaringTypeMatcher = new TypeMatcher(_methodPattern.DeclaringTypeMethod.DeclaringType);
        }

        public string Pattern => _pattern;

        public ITypeMatcher DeclaringTypeMatcher { get; }

        public bool IsMatch(MethodSignature signature)
        {
            return _methodPattern.IsMatch(signature);
        }

        public override string ToString()
        {
            return $"method({_pattern})";
        }
    }
}
