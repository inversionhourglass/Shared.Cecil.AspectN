using Mono.Cecil;

namespace Cecil.AspectN.Matchers
{
    public class TypeReferenceMatcher(TypeReference typeRef) : ITypeMatcher
    {
        public bool IsMatch(TypeSignature signature)
        {
            return typeRef.Scope.ToString() == signature.Reference.Scope.ToString() && typeRef.FullName == signature.Reference.FullName;
        }
    }
}
