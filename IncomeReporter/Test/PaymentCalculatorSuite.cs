using System;

using NUnit.Framework;

using Challenge;

namespace Tests
{
	[TestFixture]
	public class PaymentCalculatorSuite
	{
		[Test]
		public void GivenAnHourlyRate_ShouldYieldGrossPayment()
		{
			decimal hourlyRate = 10.00m;
			decimal hoursWorked = 40.0m;

			double expectedGrossPay = 400.00;

			var calculator = new PaymentCalculator();
			var grossPay = calculator.calculateGrossPay(hourlyRate, hoursWorked);

			Assert.AreEqual(expectedGrossPay, grossPay);
		}
	}
}
