//Ryan Ball
using System;

namespace EulerProblems
{
    class ProblemChooser
    {
	static void Main(string[] args)
	{
	    IEulerProblem p2 = new Problem2();
	    Console.WriteLine(p2.GetAnswer());
	}
    }
}