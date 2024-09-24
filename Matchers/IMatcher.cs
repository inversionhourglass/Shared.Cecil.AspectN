namespace Cecil.AspectN.Matchers
{
    public interface IMatcher
    {
        bool SupportDeclaringTypeMatch { get; }

        ITypeMatcher DeclaringTypeMatcher { get; }

        bool IsMatch(MethodSignature signature);
    }
}
