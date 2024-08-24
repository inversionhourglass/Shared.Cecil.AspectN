﻿using System.Collections.Generic;

namespace Cecil.AspectN.Patterns
{
    public interface IIntermediateTypePattern : ITypePattern
    {
        GenericNamePattern SeparateOutMethod();

        DeclaringTypeMethodPattern ToDeclaringTypeMethod(params string[] methodImplicitPrefixes);

        void Compile(List<GenericParameterTypePattern> genericParameters, bool genericIn);
    }
}
