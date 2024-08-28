namespace Cecil.AspectN.Matchers
{
    public interface ITypeMatcher
    {
        bool IsMatch(TypeSignature signature);
    }
}
