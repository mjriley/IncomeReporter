using System.Collections.Generic;

namespace Challenge.Reporting
{
	/* An item-by-item breakdown of a person's earnings */
	public class IncomeReport
	{
		public string Location { get; private set; }
		public decimal Gross { get; private set; }
		public List<LineItem> Deductions { get; private set; }
		public decimal Net { get; private set; }

		// TODO add localization dependency
		public IncomeReport(string location, decimal gross, List<LineItem> deductions, decimal net)
		{
			Location = location;
			Gross = gross;
			Deductions = deductions;
			Net = net;
		}

		// NOTE: I took some formatting liberties here. I didn't like the spacing used in the example.
		// The output here should resemble:
		// Employee Location: X
		// 
		// Gross Amount: X.00
		//
		// Less Deductions
		// ---------------
		// Reason 1: X.00
		// Reason 2: X.00
		//
		// Net Amount: X.00
		public override string ToString()
		{
			string deductionOutput = "";
			foreach (var deduction in Deductions)
			{
				deductionOutput += string.Format("{0}: ${1:0.00}\n", deduction.Reason, deduction.Amount);
			}

			string result = string.Format(@"Employee Location: {0}

Gross Amount: ${1:0.00}

Less Deductions
---------------
{2}
Net Amount: ${3:0.00}", Location, Gross, deductionOutput, Net);
			return result;
		}
	}
}
