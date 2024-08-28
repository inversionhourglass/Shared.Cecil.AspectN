using Cecil.AspectN.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cecil.AspectN.Patterns.Parsers
{
    public interface ITypePrefixParser
    {
        bool IsMatch(Token token);

        IIntermediateTypePattern ParseType(TokenSource tokens);
    }
}
