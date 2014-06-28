//Ryan Ball
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
}