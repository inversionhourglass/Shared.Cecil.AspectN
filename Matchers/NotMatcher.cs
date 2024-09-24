using System;

namespace Cecil.AspectN.Matchers
{
    public class NotMatcher : IMatcher
    {
        private readonly Lazy<ITypeMatcher> _declaringTypeMatcher;
        private readonly IMatcher _matcher;

        public NotMatcher(IMatcher matcher)
        {
            _matcher = matcher;
            _declaringTypeMatcher = new(() => new NotTypeMatcher(matcher.DeclaringTypeMatcher));
        }

        public IMatcher InnerMatcher => _matcher;

        public ITypeMatcher DeclaringTypeMatcher => _declaringTypeMatcher.Value;

        public bool SupportDeclaringTypeMatch => _matcher.SupportDeclaringTypeMatch;

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
