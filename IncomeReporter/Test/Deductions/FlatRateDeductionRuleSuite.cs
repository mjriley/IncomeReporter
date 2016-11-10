using NUnit.Framework;
using Challenge.Deductions;

namespace Tests.Deductions
{
	[TestFixture]
	public class FlatRateDeductionRuleSuite
	{
		[Test]
		public void CalculatesFlatRates()
		{
			var rule = new FlatRateDeductionRule("TestRule", .25m);
			var amountDeducted = rule.Apply(600m);

			Assert.AreEqual(150m, amountDeducted);
		}
	}
}
