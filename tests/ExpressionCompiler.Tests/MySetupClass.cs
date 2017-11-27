using System.Globalization;
using NUnit.Framework;

namespace ExpressionCompiler.Tests
{
    [SetUpFixture]
    public class MySetupClass
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        }
    }
}