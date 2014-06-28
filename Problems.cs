//Ryan Ball
using System;
using System.Linq;

namespace EulerProblems
{
    interface IEulerProblem
    {
	string GetAnswer();
    }

    class Problem2 : IEulerProblem
    {
	public string GetAnswer()
	{
	    int previous = 1;
	    int current = 2;
	    int temp = 0;
	    int sum = 0;
	    while(current <= 4000000)
	    {
		if(IsEven(current))
		{
		    sum += current;
		}
		temp = previous;
		previous = current;
		current = temp + previous;
	    }
	    return sum.ToString();
	}

	private bool IsEven(int x)
	{
	    if(x % 2 == 0)
	    {
		return true;
	    }
	    return false;
	}
    }

    class Problem4 : IEulerProblem
    {
	int product = 0;
	int result = 0;
	public string GetAnswer()
	{
	    //This loop should only test each combination once.
	    for(int i = 100; i <= 999; i++)
	    {
		for(int j = 100; j <= i; j++)
		{
		    product = i * j;
		    if(isPalindrome(product) && (product > result))
		    {
			result = product;
		    }
		}
	    }
	    return result.ToString();
	}

	private bool isPalindrome(int num)
	{
	    string numString = num.ToString();
	    if(numString == new string(numString.Reverse().ToArray()))
	    {
		return true;
	    }
	    return false;
	}
    }

    class Problem14 : IEulerProblem
    {
	private int[] stepArray;

	public Problem14()
	{
	    stepArray = new int[1000000];
	}

	public string GetAnswer()
	{
	    int steps;
	    int maxSteps = 0;
	    int result = 0;
	    long current = 0L;
	    for(int i = 1; i < 1000000; i++)
	    {
		current = i;
		steps = 0;
		while(current != 1)
		{
		    steps++;
		    if(IsEven(current))
		    {
			current = current / 2;
			if(current < i)
			{
			    //Console.WriteLine(current);
			    steps += stepArray[(int)current];
			    current = 1;
			}
		    }
		    else
		    {
			current = (current * 3) + 1;
		    }
		}
		stepArray[i] = steps;
		if(steps > maxSteps)
		{
		    maxSteps = steps;
		    result = i;
		}
	    }
	    return result.ToString();
	}

	private bool IsEven(long x)
	{
	    if(x % 2 == 0)
	    {
		return true;
	    }
	    return false;
	}
    }
}