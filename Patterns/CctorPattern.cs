namespace Cecil.AspectN.Patterns
{
    public class CctorPattern(ITypePattern declaringType)
    {
        public ITypePattern DeclaringType { get; } = declaringType;

        public virtual bool IsMatch(MethodSignature signature)
        {
            if (!signature.Definition.IsConstructor || !signature.Definition.IsStatic) return false;

            return DeclaringType.IsMatch(signature.DeclareType);
        }
    }
}
