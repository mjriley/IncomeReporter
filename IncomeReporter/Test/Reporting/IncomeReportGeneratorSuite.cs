using NUnit.Framework;
using System.Collections.Generic;
using Challenge.Reporting;
using Challenge.Deductions;

namespace Tests.Reporting.ItemizedIncomeCalculatorSuite
{
	[TestFixture]
	public class GivenAppropriateInputs
	{
		[Test]
		public void GenerateReport_IncludesGrossPay()
		{
			var generator = new IncomeReportGenerator(new List<IDeductionRule>());
			// note: add timesheet in the future
			var hourlyRate = 10m;
			var hoursWorked = 40m;

			var report = generator.GenerateReport("dummy location", hourlyRate, hoursWorked);
			Assert.AreEqual(400m, report.Gross);
		}

		[Test]
		public void GenerateReport_IncludesDeductions()
		{
			var deductionRule = new FlatRateDeductionRule("TestDeduction", 0.25m);

			var generator = new IncomeReportGenerator(new List<IDeductionRule> { deductionRule });

			var hourlyRate = 10m;
			var hoursWorked = 40m;

			var report = generator.GenerateReport("dummy location", hourlyRate, hoursWorked);

			var deductions = report.Deductions;
			Assert.AreEqual(1, deductions.Count);
			Assert.AreEqual("TestDeduction", deductions[0].Reason);
			Assert.AreEqual(100m, deductions[0].Amount);
		}

		[Test]
		public void GenerateReport_IncludesNet()
		{
			var deductionRule = new FlatRateDeductionRule("TestDeduction", 0.25m);

			var generator = new IncomeReportGenerator(new List<IDeductionRule> { deductionRule });

			var hourlyRate = 10m;
			var hoursWorked = 40m;

			var report = generator.GenerateReport("dummy location", hourlyRate, hoursWorked);

			Assert.AreEqual(300m, report.Net);
		}
	}
}
