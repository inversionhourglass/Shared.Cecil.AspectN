using Cecil.AspectN.Tokens;

namespace Cecil.AspectN.Patterns.Parsers
{
    public interface ITypePrefixParser
    {
        bool IsMatch(Token token);

        IIntermediateTypePattern ParseType(TokenSource tokens);
    }
}
