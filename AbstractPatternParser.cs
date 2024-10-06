using System;

namespace Cecil.AspectN
{
    public abstract class AbstractPatternParser<TMatcher>
    {
        protected abstract TMatcher NewNotMatcher(TMatcher matcher);

        protected abstract TMatcher NewAndMatcher(TMatcher matcher1, TMatcher matcher2);

        protected abstract TMatcher NewOrMatcher(TMatcher matcher1, TMatcher matcher2);

        protected abstract TMatcher ParseSingle(string pattern, ref int index);

        public TMatcher ParsePattern(string pattern)
        {
            var index = 0;
            return ParsePattern(pattern, ref index);
        }

        protected virtual TMatcher ParsePattern(string pattern, ref int index)
        {
            var not = ParseNot(pattern, ref index);
            var matcher = ParseSingle(pattern, ref index);
            if (not) matcher = NewNotMatcher(matcher);
            var connector = ParseConnector(pattern, ref index);
            if (connector == Connector.And)
            {
                matcher = NewAndMatcher(matcher, ParseNotOr(pattern, ref index, out connector));
            }
            if (connector == Connector.Or)
            {
                matcher = NewOrMatcher(matcher, ParsePattern(pattern, ref index));
            }

            return matcher;
        }

        protected virtual TMatcher ParseNotOr(string pattern, ref int index, out Connector connector)
        {
            var not = ParseNot(pattern, ref index);
            var matcher = ParseSingle(pattern, ref index);
            if (not) matcher = NewNotMatcher(matcher);
            connector = ParseConnector(pattern, ref index);
            if (connector == Connector.And)
            {
                matcher = NewAndMatcher(matcher, ParseNotOr(pattern, ref index, out connector));
            }

            return matcher;
        }

        protected virtual bool ParseNot(string pattern, ref int index)
        {
            var stash = index;
            while (index < pattern.Length)
            {
                var ch = pattern[index++];
                if (ch.IsWhiteSpace()) continue;
                if (ch == '!') return true;
                break;
            }
            index = stash;
            return false;
        }

        protected virtual Connector ParseConnector(string pattern, ref int index)
        {
            while (index < pattern.Length)
            {
                var ch = pattern[index++];
                if (ch.IsWhiteSpace()) continue;
                if (ch == '&')
                {
                    if (index < pattern.Length)
                    {
                        var next = pattern[index++];
                        if (next == '&') return Connector.And;
                        throw new ArgumentException($"Unexpected AND connector symbol '{next}' at index {index} of {pattern}");
                    }
                    return Connector.Eof;
                }
                if (ch == '|')
                {
                    if (index < pattern.Length)
                    {
                        var next = pattern[index++];
                        if (next == '|') return Connector.Or;
                        throw new ArgumentException($"Unexpected OR connector symbol '{next}' at index {index} of {pattern}");
                    }
                    return Connector.Eof;
                }
                throw new ArgumentException($"Unexpected connector symbol '{ch}' at index {index} of {pattern}");
            }
            return Connector.Eof;
        }

        protected enum Connector
        {
            Eof,
            And,
            Or,
        }
    }
}
