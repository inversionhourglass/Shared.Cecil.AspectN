using Cecil.AspectN.Patterns;
using Cecil.AspectN.Patterns.Parsers;

namespace Cecil.AspectN.Matchers
{
    public class CctorMatcher : IMatcher
    {
        private readonly CctorPattern _cctorPattern;

        public CctorMatcher(string pattern)
        {
            Pattern = pattern;
            _cctorPattern = CctorPatternParser.Parse(pattern);
            DeclaringTypeMatcher = new TypeMatcher(_cctorPattern.DeclaringType);
        }

        public string Pattern { get; }

        public bool SupportDeclaringTypeMatch => true;

        public ITypeMatcher DeclaringTypeMatcher { get; }

        public bool IsMatch(MethodSignature signature)
        {
            return _cctorPattern.IsMatch(signature);
        }

        public override string ToString()
        {
            return $"cctor({Pattern})";
        }
    }
}
