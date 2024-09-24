using System.Collections.Generic;

namespace Cecil.AspectN.Matchers
{
    public class CachedTypeMatcher(ITypeMatcher typeMatcher) : ITypeMatcher
    {
        private static readonly Dictionary<Pair, bool> _Cache = [];

        public bool IsMatch(TypeSignature signature)
        {
            var pair = new Pair(typeMatcher, signature);
            if (!_Cache.TryGetValue(pair, out var result))
            {
                result = typeMatcher.IsMatch(signature);
                _Cache[pair] = result;
            }

            return result;
        }

        readonly struct Pair(ITypeMatcher matcher, TypeSignature signature)
        {
            public readonly ITypeMatcher Matcher = matcher;

            public readonly TypeSignature Signature = signature;
        }
    }

    public static class CachedTypeMatcherExtensions
    {
        public static ITypeMatcher Cached(this ITypeMatcher typeMatcher)
        {
            return new CachedTypeMatcher(typeMatcher);
        }
    }
}
