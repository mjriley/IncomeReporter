namespace Challenge
{
	/* Performs payment calculations. Currently limited to simple gross pay calculations */
	public class PaymentCalculator
	{
		public decimal calculateGrossPay(decimal hourlyRate, decimal hoursWorked)
		{
			return hourlyRate * hoursWorked;
		}

		public PaymentCalculator()
		{
		}
	}
}
