using System.Collections.Generic;

using DynamicConsole.Commands.Errors;
using DynamicConsole.Commands.Input;

namespace DynamicConsole.Tests
{
    using global::DynamicConsole.Commands.Signatures;

    static internal class TypeSignatureTestHelpers
    {
        public static bool TestLine<T>(TypedSignature<T> sig, string line) where T : class, new()
        {
            var ci = CommandInput.Parse(line);
            var can = sig.CanRun(ci);
            var run = false;
            if (can)
            {
                run = sig.Run(ci, new List<CommandError>());
            }
            return can && run;
        }
    }
}