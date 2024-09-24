using System;

namespace Cecil.AspectN.Matchers
{
    public class OrMatcher : IMatcher
    {
        private readonly IMatcher _left;
        private readonly IMatcher _right;
        private readonly Lazy<ITypeMatcher> _declaringTypeMatcher;

        public OrMatcher(IMatcher left, IMatcher right)
        {
            _left = left;
            _right = right;
            _declaringTypeMatcher = new(() => new OrTypeMatcher(left.DeclaringTypeMatcher, right.DeclaringTypeMatcher));
        }

        public IMatcher Left => _left;

        public IMatcher Right => _right;

        public ITypeMatcher DeclaringTypeMatcher => _declaringTypeMatcher.Value;

        public bool SupportDeclaringTypeMatch => _left.SupportDeclaringTypeMatch && _right.SupportDeclaringTypeMatch;

        public bool IsMatch(MethodSignature signature)
        {
            return _left.IsMatch(signature) || _right.IsMatch(signature);
        }

        public override string ToString()
        {
            return $"{_left} || {_right}";
        }
    }
}
