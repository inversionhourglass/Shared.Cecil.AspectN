namespace Cecil.AspectN.Matchers
{
    public class NotMatcher : IMatcher
    {
        private readonly IMatcher _matcher;

        public NotMatcher(IMatcher matcher)
        {
            _matcher = matcher;
            DeclaringTypeMatcher = new NotTypeMatcher(matcher.DeclaringTypeMatcher);
        }

        public IMatcher InnerMatcher => _matcher;

        public ITypeMatcher DeclaringTypeMatcher { get; }

        public bool IsMatch(MethodSignature signature)
        {
            return !_matcher.IsMatch(signature);
        }

        public override string ToString()
        {
            return $"!{_matcher}";
        }
    }
}
