namespace Cecil.AspectN.Matchers
{
    public interface IMatcher
    {
        ITypeMatcher DeclaringTypeMatcher { get; }

        bool IsMatch(MethodSignature signature);
    }
}
