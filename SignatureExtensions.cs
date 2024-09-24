using Cecil.AspectN.Matchers;
using Mono.Cecil;
using System.Collections.Generic;

namespace Cecil.AspectN
{
    public static class SignatureExtensions
    {
        private static readonly Dictionary<Pair, MethodDefinition?> _Cache = [];

        public static MethodDefinition? FindMethod(this TypeSignature signature, IMatcher matcher, bool compositeAccessibility)
        {
            var pair = new Pair(signature, matcher);
            if (!_Cache.TryGetValue(pair, out var def))
            {
                foreach (var methodDef in signature.Reference.ToDefinition().Methods)
                {
                    var methodSignature = SignatureParser.ParseMethod(methodDef, compositeAccessibility);
                    if (matcher.IsMatch(methodSignature))
                    {
                        def = methodDef;
                        break;
                    }
                }
                _Cache.Add(pair, def);
            }

            return def;
        }

        readonly struct Pair(TypeSignature signature, IMatcher matcher)
        {
            public readonly TypeSignature Signature = signature;

            public readonly IMatcher Matcher = matcher;
        }
    }
}
