using Cecil.AspectN.Tokens;
using System.Collections.Generic;

namespace Cecil.AspectN.Patterns.Parsers
{
    public class CtorPatternParser : Parser
    {
        public static CtorPattern Parse(string pattern)
        {
            var tokens = TokenSourceBuilder.Build(pattern);

            var declaringType = ParseType(tokens);
            var parameters = ParseParameters(tokens);

            var genericParameters = new List<GenericParameterTypePattern>();
            declaringType.Compile(genericParameters, false);
            
            if (parameters is IntermediateTypePatterns itps)
            {
                foreach (var p in itps.Patterns)
                {
                    if (p is IIntermediateTypePattern itp)
                    {
                        itp.Compile(genericParameters, true);
                    }
                }
            }

            return new(declaringType, parameters);
        }
    }
}
