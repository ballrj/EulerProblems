//Ryan Ball
using System;
using System.IO;
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

    class Problem54 : IEulerProblem
    {
	//I don't want to define Two as 1 or anything, so cards just can't be ranked 1 at all. It should be fine.
	enum CardType : int{Jack=11, Queen, King, Ace};

	//It looks like suit rankings aren't used in this problem, so they don't need to be compared, but enums still seem like a good way to handle this.
	enum CardSuit : int{Club, Diamond, Heart, Spade};

	enum CardRank : int{HighCard, OnePair, TwoPairs, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalFlush};

	class Hand : IComparable
	{
	    class Card : IComparable
	    {
		public int Num{ get; private set; }
		public int Suit{ get; private set; }

		public Card(string cardString)
		{
		    char numChar = cardString.First<Char>();
		    if(numChar == 'T')
		    {
			Num = 10;
		    }
		    else if(numChar == 'J')
		    {
			Num = (int)CardType.Jack;
		    }
		    else if(numChar == 'Q')
		    {
			Num = (int)CardType.Queen;
		    }
		    else if(numChar == 'K')
		    {
			Num = (int)CardType.King;
		    }
		    else if(numChar == 'A')
		    {
			Num = (int)CardType.Ace;
		    }
		    else
		    {
			Num = (int)Char.GetNumericValue(numChar);
		    }
		    char suitChar = cardString.Last<Char>();
		    if(suitChar == 'C')
		    {
			Suit = (int)CardSuit.Club;
		    }
		    else if(suitChar == 'D')
		    {
			Suit = (int)CardSuit.Diamond;
		    }
		    else if(suitChar == 'H')
		    {
			Suit = (int)CardSuit.Heart;
		    }
		    else
		    {
			Suit = (int)CardSuit.Spade;
		    }
		}

		public int CompareTo(object obj)
		{
		    Card otherCard = obj as Card;
		    return Num.CompareTo(otherCard.Num);
		}
	    }

	    Card[] Cards;
	    public int Rank{ get; private set; }

	    //the number of the cards making up the rank; will be null for some ranks that it isn't relevent for
	    public int RankNum{ get; private set; }

	    //only relevent for two pairs; not sure if it's necessary, the question was ambiguous
	    public int SecondaryRankNum{ get; private set; }

	    public Hand(string[] cardStrings)
	    {
		Cards = new Card[5];

		for(int i = 0; i < 5; i++)
		{
		    Cards[i] = new Card(cardStrings[i]);
		}
		Array.Sort(Cards);

		//this is just in another method to break it up some
		FindRank();
	    }

	    private void FindRank()
	    {
		//whether there's a pair, three of a kind, or four of a kind
		int ofAKind = 1;

		//if there's another pair, for two pairs and full houses
		bool otherPair = false;

		//if it's a straight
		bool straight = true;

		//if it's a flush
		bool flush = true;

		//When the highest card is an Ace and the lowest card is a Two, one break is allowed when checking for a straight.
		bool straightBreakAllowed = false;
		if(Cards[0].Num == 2 && Cards[4].Num == (int)CardType.Ace)
		{
		    straightBreakAllowed = true;
		}

		//tracks how many cards in a row have been the same
		int curOfAKind = 1;

		//the card number of the cards for ofAKind; needed for otherPair
		int ofAKindNum = 0;

		//the suit of the first card
		int suit = Cards[0].Suit;

		//the number of the previous card; 0 means it's the first cards
		int prevNum = 0;

		//this loop analyzes the hand for these traits
		foreach(Card card in Cards)
		{
		    if(card.Num == prevNum)
		    {
			curOfAKind++;
			if(curOfAKind >= ofAKind)
			{
			    if(ofAKind == 2 && ofAKindNum != card.Num)
			    {
				otherPair = true;
			    }
			    ofAKindNum = card.Num;
			    SecondaryRankNum = RankNum;
			    RankNum = card.Num;
			    ofAKind = curOfAKind;
			}
			else if(ofAKind == 3)
			{
			    //this must mean that there's a full house
			    otherPair = true;
			}
		    }
		    else
		    {
			curOfAKind = 1;
		    }
		    if(straight && prevNum != 0 && card.Num != prevNum + 1)
		    {
			if(straightBreakAllowed)
			{
			    straightBreakAllowed = false;
			}
			else
			{
			    straight = false;
			}
		    }
		    if(card.Suit != suit)
		    {
			flush = false;
		    }
		    prevNum = card.Num;
		}

		//now it's time to assign a rank
		if(straight && flush && Cards[4].Num == 10)
		{
		    Rank = (int)CardRank.RoyalFlush;
		}
		else if(straight && flush)
		{
		    Rank = (int)CardRank.StraightFlush;
		}
		else if(ofAKind == 4)
		{
		    Rank = (int)CardRank.FourOfAKind;
		}
		else if(ofAKind == 3 && otherPair)
		{
		    Rank = (int)CardRank.FullHouse;
		}
		else if(flush)
		{
		    Rank = (int)CardRank.Flush;
		}
		else if(straight)
		{
		    Rank = (int)CardRank.Straight;
		}
		else if(ofAKind == 3)
		{
		    Rank = (int)CardRank.ThreeOfAKind;
		}
		else if(ofAKind == 2 && otherPair)
		{
		    Rank = (int)CardRank.TwoPairs;
		    if(SecondaryRankNum > RankNum)
		    {
			int temp = RankNum;
			RankNum = SecondaryRankNum;
			SecondaryRankNum = temp;
		    }
		}
		else if(ofAKind == 2)
		{
		    Rank = (int)CardRank.OnePair;
		}
		else
		{
		    Rank = (int)CardRank.HighCard;
		}
	    }

	    public int CompareTo(object obj)
	    {
		Hand otherHand = obj as Hand;
		if(Rank != otherHand.Rank)
		{
		    return Rank.CompareTo(otherHand.Rank);
		}
		if(Rank != (int)CardRank.Flush && RankNum != otherHand.RankNum)
		{
		    return RankNum.CompareTo(otherHand.RankNum);
		}
		if(Rank == (int)CardRank.TwoPairs && SecondaryRankNum != otherHand.SecondaryRankNum)
		{
		    return SecondaryRankNum.CompareTo(otherHand.SecondaryRankNum);
		}
		for(int i = 4; i >= 0; i--)
		{
		    if(Cards[i].CompareTo(otherHand.Cards[i]) != 0)
		    {
			return Cards[i].CompareTo(otherHand.Cards[i]);
		    }
		}
		return 0;
	    }
	}

	public string GetAnswer()
	{
	    StreamReader reader = new StreamReader("poker.txt");
	    string line;
	    string[] cardArray;
	    int total = 0;
	    try
	    {
		while (reader.Peek() != -1)
		{
		    line = reader.ReadLine();
		    cardArray = line.Split(new Char[] { ' ' });
		    if((new Hand(cardArray.Take(5).ToArray())).CompareTo(new Hand(cardArray.Skip(5).ToArray())) > 0)
		    {
			total++;
		    }
		}
	    }
	    catch(Exception e)
	    {
		return e.Message;
	    }
	    return total.ToString();
	}
    }
}