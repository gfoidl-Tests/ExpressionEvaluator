using System.Collections.Generic;

namespace ExpressionCompiler
{
    public interface ICompiler
    {
        IReadOnlyCollection<string> Parameters { get; }
        //---------------------------------------------------------------------
        bool Compile(string expression, OptimizationLevel optimizationLevel = OptimizationLevel.None);
    }
}