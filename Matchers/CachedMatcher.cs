using System;
using System.Collections.Generic;

namespace Cecil.AspectN.Matchers
{
    public class CachedMatcher : IMatcher
    {
        private static readonly Dictionary<Pair, bool> _Cache = [];

        private readonly IMatcher _matcher;
        private readonly Lazy<ITypeMatcher> _declaringTypeMatcher;

        public CachedMatcher(IMatcher matcher)
        {
            _matcher = matcher;
            _declaringTypeMatcher = new(() => _matcher.DeclaringTypeMatcher is CachedTypeMatcher cachedTypeMatcher ? cachedTypeMatcher : new CachedTypeMatcher(_matcher.DeclaringTypeMatcher));
        }

        public ITypeMatcher DeclaringTypeMatcher => _declaringTypeMatcher.Value;

        public bool SupportDeclaringTypeMatch => _matcher.SupportDeclaringTypeMatch;

        public bool IsMatch(MethodSignature signature)
        {
            var pair = new Pair(_matcher, signature);
            if (!_Cache.TryGetValue(pair, out var result))
            {
                result = _matcher.IsMatch(signature);
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
