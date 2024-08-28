namespace Cecil.AspectN.Matchers
{
    public class OrMatcher : IMatcher
    {
        private readonly IMatcher _left;
        private readonly IMatcher _right;

        public OrMatcher(IMatcher left, IMatcher right)
        {
            _left = left;
            _right = right;
            DeclaringTypeMatcher = new OrTypeMatcher(left.DeclaringTypeMatcher, right.DeclaringTypeMatcher);
        }

        public IMatcher Left => _left;

        public IMatcher Right => _right;

        public ITypeMatcher DeclaringTypeMatcher { get; }

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
