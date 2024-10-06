using System;

namespace Cecil.AspectN.Matchers
{
    public class MatcherFactory
    {
        public static IMatcher Create(string method, string pattern)
        {
            return CreateCore(method.Trim().ToLower(), pattern);
        }

        private static IMatcher CreateCore(string method, string pattern) => method switch
        {
            "regex" => new RegexMatcher(pattern),
            "execution" => new ExecutionMatcher(pattern),
            "method" => new MethodMatcher(pattern),
            "getter" => new GetterMatcher(pattern),
            "setter" => new SetterMatcher(pattern),
            "property" => new PropertyMatcher(pattern),
            "attr" => new AttributeMatcher(pattern),
            _ => throw new ArgumentException($"unknow matcher type({method}) with pattern {pattern}")
        };
    }
}
