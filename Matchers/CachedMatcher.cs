using System.Collections.Generic;

namespace Cecil.AspectN.Matchers
{
    public class CachedMatcher(IMatcher matcher) : IMatcher
    {
        private static readonly Dictionary<Pair, bool> _Cache = [];

        public ITypeMatcher DeclaringTypeMatcher { get; } = matcher.DeclaringTypeMatcher is CachedTypeMatcher cachedTypeMatcher ? cachedTypeMatcher : new CachedTypeMatcher(matcher.DeclaringTypeMatcher);

        public bool IsMatch(MethodSignature signature)
        {
            var pair = new Pair(matcher, signature);
            if (!_Cache.TryGetValue(pair, out var result))
            {
                result = matcher.IsMatch(signature);
                _Cache[pair] = result;
            }

            return result;
        }

        readonly struct Pair(IMatcher matcher, MethodSignature signature)
        {
            public readonly IMatcher Matcher = matcher;

            public readonly MethodSignature Signature = signature;
        }
    }

    public static class CachedMatcherExtensions
    {
        public static IMatcher Cached(this IMatcher matcher)
        {
            return new CachedMatcher(matcher);
        }
    }
}
