using System;

namespace Cecil.AspectN.Matchers
{
    public class AndMatcher : IMatcher
    {
        private readonly Lazy<ITypeMatcher> _declaringTypeMatcher;

        public AndMatcher(IMatcher left, IMatcher right)
        {
            Left = left;
            Right = right;
            SupportDeclaringTypeMatch = left.SupportDeclaringTypeMatch && right.SupportDeclaringTypeMatch;
            _declaringTypeMatcher = new(() => new AndTypeMatcher(left.DeclaringTypeMatcher, right.DeclaringTypeMatcher));
        }

        public IMatcher Left { get; }

        public IMatcher Right { get; }

        public ITypeMatcher DeclaringTypeMatcher => _declaringTypeMatcher.Value;

        public bool SupportDeclaringTypeMatch { get; }

        public bool IsMatch(MethodSignature signature)
        {
            return Left.IsMatch(signature) && Right.IsMatch(signature);
        }

        public override string ToString()
        {
            return $"{Left} && {Right}";
        }
    }
}
