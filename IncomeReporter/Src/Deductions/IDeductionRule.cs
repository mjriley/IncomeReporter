using System;
namespace Challenge.Deductions
{
	/* Reprents a rule to process gross pay and produce a deduction amount */
	public interface IDeductionRule
	{
		string Reason { get; }
		decimal Apply(decimal grossPay);
	}
}
