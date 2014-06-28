//Ryan Ball
using System;
using System.Collections.Generic;

namespace EulerProblems
{
    class ProblemChooser
    {
	static void Main(string[] args)
	{
	    Dictionary<int, IEulerProblem> problems = new Dictionary<int, IEulerProblem>();
	    problems.Add(2, new Problem2());
	    problems.Add(4, new Problem4());
	    problems.Add(14, new Problem14());
	    string userInput = "";
	    int inputInt;
	    IEulerProblem currentProblem;
	    while(userInput.ToLower() != "q")
	    {
		Console.WriteLine("--------------------------");
		Console.WriteLine("Available Euler Project problems are:");
		Console.WriteLine("2. Even Fibonacci numbers");
		Console.WriteLine("4. Largest palindrome product");
		Console.WriteLine("14. Longest Collatz sequence\n");
		Console.WriteLine("Type a number to see the answer. Type 'q' to quit.\n");
		userInput = Console.ReadLine();
		try
		{
		    if(userInput.ToLower() != "q")
		    {
			inputInt = Convert.ToInt32(userInput);
			currentProblem = problems[Convert.ToInt32(userInput)];
			Console.WriteLine("\nThe answer is:");
			Console.WriteLine(currentProblem.GetAnswer());
			Console.WriteLine();
		    }
		}
		catch(FormatException)
		{
		    Console.WriteLine("Please type a number.");
		}
		catch(KeyNotFoundException)
		{
		    Console.WriteLine("That solution is not available.");
		}
	    }
	}
    }
}