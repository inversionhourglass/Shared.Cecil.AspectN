namespace Cecil.AspectN.Patterns
{
    public class CtorPattern(ITypePattern declaringType, ITypePatterns parameters)
    {
        public ITypePattern DeclaringType { get; } = declaringType;

        public ITypePatterns Parameters { get; } = parameters;

        public virtual bool IsMatch(MethodSignature signature)
        {
            if (!signature.Definition.IsConstructor || signature.Definition.IsStatic) return false;

            if (Parameters.Count != signature.MethodParameters.Length) return false;

            if (!DeclaringType.IsMatch(signature.DeclareType)) return false;

            return Parameters.IsMatch(signature.MethodParameters);
        }
    }
}
