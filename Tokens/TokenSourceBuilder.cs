﻿using System;
using System.Collections.Generic;

namespace Cecil.AspectN.Tokens
{
    public class TokenSourceBuilder
    {
        public static TokenSource Build(string pattern)
        {
            var tokens = new List<Token>();
            var index = 0;
            while (true)
            {
                var token = Build(pattern, ref index);
                if (token == null) break;

                tokens.Add(token);
            }
            if (index != pattern.Length) throw new ArgumentException($"Cannot parse pattern({pattern}) to tokens, stopped at index {index}");

            return new TokenSource(pattern, tokens.ToArray());
        }

        private static Token? Build(string pattern, ref int index)
        {
            var start = index;
            while (index < pattern.Length)
            {
                var ch = pattern[index++];
                switch (ch)
                {
                    case ' ':
                    case '　':
                    case '\t':
                    case '\r':
                    case '\n':
                        start = index;
                        continue;
                    case '*':
                    case '+':
                    case ',':
                    case '!':
                    case '(':
                    case ')':
                    case '<':
                    case '>':
                    case '[':
                    case ']':
                    case '?':
                    case TypeSignature.NESTED_SEPARATOR:
                        return new Token(ch, start, index);
                    case '.':
                        if (index < pattern.Length)
                        {
                            var next = pattern[index];
                            if (next == '.')
                            {
                                index++;
                                return new Token(Token.ELLIPSIS, start, index);
                            }
                        }
                        return new Token(ch, start, index);
                    case '&':
                    case '|':
                        if(index < pattern.Length)
                        {
                            var next = pattern[index++];
                            if (next != ch) throw new ArgumentException($"Unexpected connector symbol '{next}' at index {index - 1} of {pattern}");
                            return new Token(new string(ch, 2), start, index);
                        }
                        throw new ArgumentException($"Unexpected connector symbol '{ch}' at index {index - 1} of {pattern}");
                    default:
                        index--;
                        return BuildNameIdentifier(pattern, ref index);
                }
            }

            return null;
        }

        private static Token BuildNameIdentifier(string pattern, ref int index)
        {
            var start = index;
            while (index < pattern.Length)
            {
                var ch = pattern[index];
                if (ch != '_' && (ch < '0' || ch > '9' && ch < 'A' || ch > 'Z' && ch < 'a' || ch > 'z')) break;

                index++;
            }

            if (start == index) throw new ArgumentException($"unknow token ({pattern[index]}) at index {index} of {pattern}");
            return new Token(pattern.Substring(start, index - start), start, index);
        }
    }
}
