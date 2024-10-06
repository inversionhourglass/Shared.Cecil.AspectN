using Cecil.AspectN.Matchers;
using System;
using System.Collections.Concurrent;

namespace Cecil.AspectN
{
    public class PatternParser : AbstractPatternParser<IMatcher>
    {
        private readonly static PatternParser _Instance = new();
        private readonly static ConcurrentDictionary<string, IMatcher> _Cache = new();

        private PatternParser() { }

        public static IMatcher Parse(string pattern)
        {
            return _Cache.GetOrAdd(pattern, _Instance.ParsePattern);
        }

        protected override IMatcher NewNotMatcher(IMatcher matcher) => new NotMatcher(matcher);

        protected override IMatcher NewAndMatcher(IMatcher matcher1, IMatcher matcher2) => new AndMatcher(matcher1, matcher2);

        protected override IMatcher NewOrMatcher(IMatcher matcher1, IMatcher matcher2) => new OrMatcher(matcher1, matcher2);

        protected override IMatcher ParseSingle(string pattern, ref int index)
        {
            var method = ParseMethod(pattern, ref index);
            var startSymbol = pattern[index++];
            if (startSymbol != '(') throw new ArgumentException($"Expect pattern body start with '(', but got '{startSymbol}'");
            var matchPattern = ParseMatchPattern(pattern, ref index);
            var endSymbol = pattern[index++];
            if (endSymbol != ')') throw new ArgumentException($"Expect pattern body end with ')', but got '{endSymbol}'");

            return MatcherFactory.Create(method, matchPattern);
        }

        private string ParseMethod(string pattern, ref int index)
        {
            var start = index;
            while (index < pattern.Length)
            {
                var ch = pattern[index++];
                if (ch.IsWhiteSpace()) continue;
                if (ch < 'A' || ch > 'Z' && ch < 'a' || ch > 'z')
                {
                    index--;
                    break;
                }
            }

            if (start == index) throw new ArgumentException($"Unknow pattern method ({pattern[index]}) at index {index} of {pattern}");
            return pattern.Substring(start, index - start);
        }

        private string ParseMatchPattern(string pattern, ref int index)
        {
            char ch;
            var start = index;
            var groups = 0;
            var escape = false;
            var inRange = false;
            while (index < pattern.Length)
            {
                ch = pattern[index];
                switch (ch)
                {
                    case '\\':
                        escape ^= true;
                        break;
                    case '[':
                        inRange = !escape;
                        escape = false;
                        break;
                    case ']':
                        inRange = inRange && escape;
                        escape = false;
                        break;
                    case '(':
                        if (!escape && !inRange) groups++;
                        escape = false;
                        break;
                    case ')':
                        if (!escape && !inRange)
                        {
                            if (groups == 0) return pattern.Substring(start, index - start);
                            groups--;
                        }
                        escape = false;
                        break;
                    default:
                        escape = false;
                        break;
                }
                index++;
            }
            throw new ArgumentException($"Unable parse the token source from index {start} to the end of pattern({pattern})");
        }
    }
}
