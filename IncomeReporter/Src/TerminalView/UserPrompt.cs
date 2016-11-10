using System;
using System.IO;

namespace Challenge
{
	/* A prompt designed to loop indefinitely until it receives user input that passes its validators. */
	public class UserPrompt
	{
		// process user input, either return an empty string if the input is acceptable,
		// or an error message if it is invalid
		public delegate string VerifyResponse(string userInput);

		private string _prompt;
		private VerifyResponse _verifier;
		private TextReader _input;
		private TextWriter _output;

		public UserPrompt(string prompt, VerifyResponse verifier, TextReader input=null, TextWriter output=null)
		{
			// allow IO streams to be injected
			_input = (input == null) ? Console.In : input;
			_output = (output == null) ? Console.Out : output;

			_prompt = prompt;
			_verifier = verifier;
		}

		public string Execute()
		{
			_output.WriteLine(_prompt);
			bool satisfied = false;
			string userResponse = string.Empty;
			string errorMessage = string.Empty;

			while (!satisfied)
			{
				userResponse = _input.ReadLine();
				errorMessage = _verifier(userResponse);
				satisfied = string.IsNullOrEmpty(errorMessage);

				if (!satisfied)
				{
					_output.WriteLine(errorMessage);
				}
			}

			return userResponse;
		}
	}
}
