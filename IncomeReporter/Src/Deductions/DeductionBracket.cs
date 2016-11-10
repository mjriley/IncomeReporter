namespace Challenge.Deductions
{
	/* A DeductionBracket represents a rate at which to deduct from gross earnings
	 * until a specified amount is reached */
	public struct DeductionBracket
	{
		public decimal Rate;
		public decimal Amount;

		public DeductionBracket(decimal rate, decimal amount)
		{
			Rate = rate;
			Amount = amount;
		}
	}
}
