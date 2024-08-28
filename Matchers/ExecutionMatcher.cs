using Cecil.AspectN.Patterns;
using Cecil.AspectN.Patterns.Parsers;

namespace Cecil.AspectN.Matchers
{
    public class ExecutionMatcher : IMatcher
    {
        private readonly string _pattern;
        private readonly ExecutionPattern _executionPattern;

        public ExecutionMatcher(string pattern)
        {
            _pattern = pattern;
            _executionPattern = ExecutionPatternParser.Parse(pattern);
            DeclaringTypeMatcher = new TypeMatcher(_executionPattern.DeclaringTypeMethod.DeclaringType);
        }

        public string Pattern => _pattern;

        public ITypeMatcher DeclaringTypeMatcher { get; }

        public bool IsMatch(MethodSignature signature)
        {
            return _executionPattern.IsMatch(signature);
        }

        public override string ToString()
        {
            return $"execution({_pattern})";
        }
    }
}
