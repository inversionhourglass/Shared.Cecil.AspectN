namespace Cecil.AspectN.Matchers
{
    public class AndMatcher(IMatcher left, IMatcher right) : IMatcher
    {
        public IMatcher Left => left;

        public IMatcher Right => right;

        public ITypeMatcher DeclaringTypeMatcher { get; } = new AndTypeMatcher(left.DeclaringTypeMatcher, right.DeclaringTypeMatcher);

        public bool IsMatch(MethodSignature signature)
        {
            return left.IsMatch(signature) && right.IsMatch(signature);
        }

        public override string ToString()
        {
            return $"{left} && {right}";
        }
    }
}
