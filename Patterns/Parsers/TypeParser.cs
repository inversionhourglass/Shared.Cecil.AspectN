using Cecil.AspectN.Tokens;

namespace Cecil.AspectN.Patterns.Parsers
{
    public class TypeParser : Parser
    {
        public static ITypePattern Parse(string pattern)
        {
            var tokens = TokenSourceBuilder.Build(pattern);

            var typePattern = ParseType(tokens);
            typePattern.Compile([], true);

            return typePattern;
        }
    }
}
