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
	    string userInput = "";
	    int inputInt;
	    IEulerProblem currentProblem;
	    while(userInput.ToLower() != "q")
	    {
		Console.WriteLine("--------------------------");
		Console.WriteLine("Available Euler Project problems are:");
		Console.WriteLine("2. Even Fibonacci numbers\n");
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