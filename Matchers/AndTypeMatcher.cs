namespace Cecil.AspectN.Matchers
{
    public class AndTypeMatcher(ITypeMatcher left, ITypeMatcher right) : ITypeMatcher
    {
        public bool IsMatch(TypeSignature signature)
        {
            return left.IsMatch(signature) && right.IsMatch(signature);
        }

        public override string ToString()
        {
            return $"{left} && {right}";
        }
    }
}
