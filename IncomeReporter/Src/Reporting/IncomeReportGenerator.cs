using System.Collections.Generic;
using Challenge.Deductions;

namespace Challenge.Reporting
{
	/* Uses work data to generate an earnings report */
	public class IncomeReportGenerator
	{
		private PaymentCalculator _calc;
		private List<IDeductionRule> _deductionRules;

		public IncomeReportGenerator(List<IDeductionRule> deductionRules)
		{
			_deductionRules = deductionRules;
			_calc = new PaymentCalculator();
		}

		public IncomeReport GenerateReport(string location, decimal hoursWorked, decimal hourlyRate)
		{
			var gross = _calc.calculateGrossPay(hourlyRate, hoursWorked);
			var deductions = new List<LineItem>();

			var netPay = gross;

			foreach (var rule in _deductionRules)
			{
				var deductionAmount = rule.Apply(gross);
				netPay -= deductionAmount;
				deductions.Add(new LineItem(deductionAmount, rule.Reason));
			}

			return new IncomeReport(location, gross, deductions, netPay);
		}
	}
}
