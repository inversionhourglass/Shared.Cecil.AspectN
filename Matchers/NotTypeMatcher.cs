namespace Cecil.AspectN.Matchers
{
    public class NotTypeMatcher(ITypeMatcher matcher) : ITypeMatcher
    {
        public bool IsMatch(TypeSignature signature)
        {
            return !matcher.IsMatch(signature);
        }

        public override string ToString()
        {
            return $"!{matcher}";
        }
    }
}
