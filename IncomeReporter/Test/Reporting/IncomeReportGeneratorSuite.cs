using NUnit.Framework;
using System.Collections.Generic;
using Challenge.Reporting;
using Challenge.Deductions;

namespace Tests.Reporting.ItemizedIncomeCalculatorSuite
{
	[TestFixture]
	public class GivenAppropriateInputs
	{
		private IncomeReportGenerator generator;
		[SetUp]
		public void Init()
		{
			var deductionRule = new FlatRateDeductionRule("TestDeduction", 0.25m);
			generator = new IncomeReportGenerator(new List<IDeductionRule> { deductionRule });
		}

		private IncomeReport GenerateReport()
		{
			var hourlyRate = 10m;
			var hoursWorked = 40m;

			return generator.GenerateReport("dummy location", hoursWorked, hourlyRate);
		}

		[Test]
		public void GenerateReport_IncludesGrossPay()
		{
			var report = GenerateReport();
			Assert.AreEqual(400m, report.Gross);
		}

		[Test]
		public void GenerateReport_IncludesDeductions()
		{
			var report = GenerateReport();

			var deductions = report.Deductions;
			Assert.AreEqual(1, deductions.Count);
			Assert.AreEqual("TestDeduction", deductions[0].Reason);
			Assert.AreEqual(100m, deductions[0].Amount);
		}

		[Test]
		public void GenerateReport_IncludesNet()
		{
			var report = GenerateReport();

			Assert.AreEqual(300m, report.Net);
		}

		[Test]
		public void GenerateReport_IncludesLocation()
		{
			var report = GenerateReport();

			Assert.AreEqual("dummy location", report.Location);
		}
	}
}
