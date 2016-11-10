using NUnit.Framework;
using System.Collections.Generic;
using Challenge.Reporting;

namespace Tests.Reporting
{
	[TestFixture]
	public class IncomeReportSuite
	{
		[Test]
		public void ToString_ProducesCorrectOutput()
		{
			var deductions = new List<LineItem> { new LineItem(200m, "Bribery") };
			var report = new IncomeReport("Happy Place", 1000m, deductions, 400m);

			var expectedReport =
				@"Employee Location: Happy Place

Gross Amount: $1000.00

Less Deductions
---------------
Bribery: $200.00

Net Amount: $400.00";

			Assert.AreEqual(expectedReport, report.ToString());
		}

		[Test]
		public void ToString_RoundsToTwoPlaces()
		{
			var deductions = new List<LineItem> { new LineItem(200m, "Bribery") };
			var report = new IncomeReport("Happy Place", 100.777m, deductions, 400m);

			var expectedReport =
	@"Employee Location: Happy Place

Gross Amount: $100.78

Less Deductions
---------------
Bribery: $200.00

Net Amount: $400.00";

			Assert.AreEqual(expectedReport, report.ToString());
		}
	}
}
