using System;
using System.Collections.Generic;

namespace Challenge
{
	using Deductions;
	using Reporting;

	class MainClass
	{
		public static void Main(string[] args)
		{
			var irishDeductions = new List<IDeductionRule>
			{
				new TieredDeductionRule("Income Tax", new List<DeductionBracket> {new DeductionBracket(0.25m, 600m)}, 0.4m),
				new TieredDeductionRule("Universal Social Charge", new List<DeductionBracket> { new DeductionBracket(0.07m, 500m) }, 0.08m),
				new FlatRateDeductionRule("Pension", 0.04m)
			};

			var italianDeductions = new List<IDeductionRule>
			{
				new FlatRateDeductionRule("Income Tax", 0.25m),
				new FlatRateDeductionRule("Social Security", 0.0919m)
			};

			var germanDeductions = new List<IDeductionRule>
			{
				new TieredDeductionRule("Income Tax", new List<DeductionBracket> { new DeductionBracket(0.25m, 400m) }, 0.32m),
				new FlatRateDeductionRule("Pension", 0.02m)
			};

			var deductionsToLocationMap = new Dictionary<string, List<IDeductionRule>>();
			deductionsToLocationMap["ireland"] = irishDeductions;
			deductionsToLocationMap["italy"] = italianDeductions;
			deductionsToLocationMap["germany"] = germanDeductions;

			var hoursPrompt = new UserPrompt("Please enter the hours worked: ", EnforceDecimal);
			decimal hoursWorked = decimal.Parse(hoursPrompt.Execute());

			var ratePrompt = new UserPrompt("Please enter the hourly rate: ", EnforceDecimal);
			decimal hourlyRate = decimal.Parse(ratePrompt.Execute());

			var locationPrompt = new UserPrompt("Please enter the employee's location", CreateLocationValidator(deductionsToLocationMap.Keys));
			string location = locationPrompt.Execute();

			var deductionRules = deductionsToLocationMap[location];

			var generator = new IncomeReportGenerator(deductionRules);
			var report = generator.GenerateReport(location, hoursWorked, hourlyRate);
			Console.WriteLine(report);
		}

		private static string EnforceDecimal(string input)
		{
			decimal dec = 0m;
			if (!decimal.TryParse(input, out dec))
			{
				return "Invalid decimal supplied: " + input + ". Please enter a valid decimal number.";
			}

			return string.Empty;
		}

		private static UserPrompt.VerifyResponse CreateLocationValidator(ICollection<string> validLocations)
		{
			return delegate (string input)
			{
				if (!validLocations.Contains(input.ToLower()))
				{
					return "Please enter a valid location from: " + string.Join(", ", validLocations);
				}

				return string.Empty;
			};
		}
	}
}
