using NUnit.Framework;
using System.IO;

using Challenge;

namespace Tests.TerminalView
{
	[TestFixture]
	public class UserPromptSuite
	{
		private StringReader input;
		private StringWriter output;
		UserPrompt.VerifyResponse verifier;

		[SetUp]
		public void Init()
		{
			input = new StringReader("Dummy");
			output = new StringWriter();

			verifier = ((userInput) => string.Empty);
		}

		private UserPrompt CreatePrompt()
		{
			return new UserPrompt("Please enter your location", verifier, input, output);
		}

		[Test]
		public void WhenNoVerificationFailures_ReturnsResult()
		{
			var prompt = CreatePrompt();
			var result = prompt.Execute();

			Assert.AreEqual("Dummy", result);
		}

		[Test]
		public void WhenNoVerificationFailures_ProducesCorrectUserOutput()
		{
			var prompt = CreatePrompt();
			prompt.Execute();

			var actualOutput = new StringReader(output.ToString());
			string line1 = actualOutput.ReadLine();
			Assert.AreEqual("Please enter your location", line1);
		}

		[Test]
		public void WithVerificationFailures_DisplaysErrorMessage()
		{
			// set up the error message to return
			var errorMessage = "There was an error!";
			verifier = (userInput) => (userInput == "TestString") ? string.Empty : errorMessage;
			input = new StringReader("BadString\nTestString");

			var prompt = CreatePrompt();
			prompt.Execute();

			var actualOutput = new StringReader(output.ToString());
			actualOutput.ReadLine(); // skip past the prompt
			var line2 = actualOutput.ReadLine();
			Assert.AreEqual("There was an error!", line2);
		}
	}
}
