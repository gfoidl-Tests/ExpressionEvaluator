using System.Globalization;
using NUnit.Framework;

namespace ExpressionEvaluator.Tests
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