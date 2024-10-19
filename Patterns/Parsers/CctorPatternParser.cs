using Cecil.AspectN.Tokens;

namespace Cecil.AspectN.Patterns.Parsers
{
    public class CctorPatternParser : Parser
    {
        public static CctorPattern Parse(string pattern)
        {
            var tokens = TokenSourceBuilder.Build(pattern);

            var declaringType = ParseType(tokens);
            declaringType.Compile([], true);

            return new(declaringType);
        }
    }
}
