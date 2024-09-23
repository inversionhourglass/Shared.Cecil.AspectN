using Mono.Cecil;

namespace Cecil.AspectN.Matchers
{
    public class TypeReferenceMatcher : ITypeMatcher
    {
        private readonly TypeReference _typeRef;
        private readonly TypeDefinition? _typeDef;

        public TypeReferenceMatcher(TypeReference typeRef)
        {
            _typeRef = typeRef;
            if (typeRef is TypeDefinition typeDef)
            {
                _typeDef = typeDef;
            }
        }

        public bool IsMatch(TypeSignature signature)
        {
            var typeRef = signature.Reference;
            if (_typeDef != null)
            {
                if (typeRef is not TypeDefinition typeDef)
                {
                    typeDef = typeRef.Resolve();
                }

                return _typeDef == typeDef;
            }

            return _typeRef.Scope == typeRef.Scope && _typeRef.FullName == typeRef.FullName;
        }
    }
}
