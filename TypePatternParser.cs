using Cecil.AspectN.Matchers;
using System;
using System.Collections.Concurrent;

namespace Cecil.AspectN
{
    public class TyppePatternParser : AbstractPatternParser<ITypeMatcher>
    {
        private readonly static TyppePatternParser _Instance = new();
        private readonly static ConcurrentDictionary<string, ITypeMatcher> _Cache = new();

        public static ITypeMatcher Parse(string pattern)
        {
            return _Cache.GetOrAdd(pattern, _Instance.ParsePattern);
        }

        protected override ITypeMatcher NewNotMatcher(ITypeMatcher matcher) => new NotTypeMatcher(matcher);

        protected override ITypeMatcher NewAndMatcher(ITypeMatcher matcher1, ITypeMatcher matcher2) => new AndTypeMatcher(matcher1, matcher2);

        protected override ITypeMatcher NewOrMatcher(ITypeMatcher matcher1, ITypeMatcher matcher2) => new OrTypeMatcher(matcher1, matcher2);

        protected override ITypeMatcher ParseSingle(string pattern, ref int index)
        {
            var typePattern = ParseTypePattern(pattern, ref index);

            return new TypeMatcher(typePattern);
        }

        private static string ParseTypePattern(string pattern, ref int index)
        {
            char ch;
            var start = index;
            var groups = 0;
            var generics = 0;
            var inRange = false;
            while (index < pattern.Length)
            {
                ch = pattern[index];
                switch (ch)
                {
                    case '[':
                        if (inRange) throw Throw(ch, index, pattern);
                        inRange = true;
                        break;
                    case ']':
                        inRange = false;
                        break;
                    case '(':
                        if (inRange) throw Throw(ch, index, pattern);
                        groups++;
                        break;
                    case ')':
                        if (inRange) throw Throw(ch, index, pattern);
                        if (--groups < 0) throw Throw(ch, index, pattern);
                        break;
                    case '<':
                        if (inRange) throw Throw(ch, index, pattern);
                        generics++;
                        break;
                    case '>':
                        if (inRange) throw Throw(ch, index, pattern);
                        if (--generics < 0) throw Throw(ch, index, pattern);
                        break;
                    case ' ':
                        if (!inRange && groups == 0 && generics == 0) return pattern.Substring(start, index - start);
                        break;
                    case '|':
                        if (index + 1 < pattern.Length && pattern[index + 1] == '|' && !inRange && groups == 0 && generics == 0) return pattern.Substring(start, index - start);
                        break;
                    case '&':
                        if (index + 1 < pattern.Length && pattern[index + 1] == '&' && !inRange && groups == 0 && generics == 0) return pattern.Substring(start, index - start);
                        break;
                }
                index++;
            }

            return pattern.Substring(start, index - start);

            static ArgumentException Throw(char ch, int index, string pattern) => new($"Incorrect pattern format, unexpected token '{ch}' found at index {index} in the pattern: {pattern}");
        }
    }
}
