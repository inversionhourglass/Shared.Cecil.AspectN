using Cecil.AspectN.Patterns;
using Cecil.AspectN.Patterns.Parsers;

namespace Cecil.AspectN.Matchers
{
    public class CtorMatcher : IMatcher
    {
        private readonly CtorPattern _ctorPattern;

        public CtorMatcher(string pattern)
        {
            Pattern = pattern;
            _ctorPattern = CtorPatternParser.Parse(pattern);
            DeclaringTypeMatcher = new TypeMatcher(_ctorPattern.DeclaringType);
        }

        public string Pattern { get; }

        public bool SupportDeclaringTypeMatch => true;

        public ITypeMatcher DeclaringTypeMatcher { get; }

        public bool IsMatch(MethodSignature signature)
        {
            return _ctorPattern.IsMatch(signature);
        }

        public override string ToString()
        {
            return $"ctor({Pattern})";
        }
    }
}
