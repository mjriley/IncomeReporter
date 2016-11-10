using System.Collections.Generic;

namespace Challenge.Deductions
{
	/* A Tiered Deduction Rule progressively applies each rate specified in its brackets
	 * until it either has processed all gross earnings, or can apply the finalRate to the remaining earnings */
	public class TieredDeductionRule : IDeductionRule
	{
		public string Reason { get; }
		public List<DeductionBracket> Brackets { get; }
		public decimal FinalRate { get; private set; }

		public TieredDeductionRule(string reason, List<DeductionBracket> brackets, decimal finalRate)
		{
			Reason = reason;
			Brackets = brackets;
			FinalRate = finalRate;
		}

		public decimal Apply(decimal gross)
		{
			decimal remainingPay = gross;
			decimal totalDeduction = 0m;

			// iterate through tiered brackets
			for (int i = 0; ((i < Brackets.Count) && (remainingPay > 0m)); ++i)
			{
				decimal currentDeductionRate = Brackets[i].Rate;
				decimal currentPayThreshold = Brackets[i].Amount;
				decimal deductibleAmount = (remainingPay > currentPayThreshold) ? currentPayThreshold : remainingPay;

				totalDeduction += deductibleAmount * currentDeductionRate;

				remainingPay -= deductibleAmount;
			}

			// add the flat rate on anything remaining
			if (remainingPay > 0)
			{
				totalDeduction += remainingPay * FinalRate;
			}

			return totalDeduction;
		}
	}
}
