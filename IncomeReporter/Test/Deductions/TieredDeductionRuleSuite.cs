using System.Collections.Generic;
using NUnit.Framework;
using Challenge.Deductions;

namespace Tests.Deductions.TieredDeductionRuleSuite
{
	[TestFixture]
	public class WithEmptyBrackets
	{
		[Test]
		public void CalculatesFlatRate()
		{
			var rule = new TieredDeductionRule("TestRule", new List<DeductionBracket>(), 0.25m);
			var amountDeducted = rule.Apply(600m);

			Assert.AreEqual(150m, amountDeducted);
		}
	}

	[TestFixture]
	public class WithSpecifiedBrackets
	{
		private TieredDeductionRule rule;

		[SetUp]
		public void Init()
		{
			rule = new TieredDeductionRule("TestRule", new List<DeductionBracket> { new DeductionBracket(0.25m, 600m) }, 0.5m);
		}

		[Test]
		public void WhenGrossExceedsBrackets_EnsureBracketsAndFinalRateAreApplied()
		{
			var amountDeducted = rule.Apply(800m);

			Assert.AreEqual(250m, amountDeducted);
		}

		[Test]
		public void WhenGrossIsContainedWithinBrackets_EnsureCorrectResult()
		{
			var amountDeducted = rule.Apply(200m); // the entire gross should use the first bracket

			Assert.AreEqual(50m, amountDeducted);
		}
	}
}
