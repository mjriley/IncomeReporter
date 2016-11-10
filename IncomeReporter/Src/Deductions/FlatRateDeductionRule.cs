using System.Collections.Generic;

namespace Challenge.Deductions
{
	/* A Flat Rate Deduction Rule processes all earnings by the given rate */
	public class FlatRateDeductionRule : TieredDeductionRule
	{
		public FlatRateDeductionRule(string reason, decimal rate)
			: base(reason, new List<DeductionBracket>(), rate) { }
	}
}
