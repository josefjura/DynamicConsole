using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicConsole.Tests
{
    using System.Linq.Expressions;

    using global::DynamicConsole.Commands.Signatures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class TypeSignatureAssert
    {
        public static void LineRuns<T>(bool shouldRun, TypedSignature<T> sig, string line) where T : class, new()
        {
            var result = TypeSignatureTestHelpers.TestLine(sig, line);
            Assert.AreEqual(shouldRun, result);
        }
    }
}
