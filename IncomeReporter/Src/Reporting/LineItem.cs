namespace Challenge.Reporting
{
	/* Represents any monetary output on the Income Report */
	public class LineItem
	{
		public decimal Amount { get; private set; }
		public string Reason { get; private set; }

		public LineItem(decimal amount, string reason)
		{
			Amount = amount;
			Reason = reason;
		}
	}
}
