using System;
using System.Text;
class Hand
{
    public List<Card> cards { get; } = new List<Card>();

    public void add(Card card)
    {
        cards.Add(card);
    }

    ///<summary> all the possible values this hand can equal (including all cases for aces = 1 or 11) </summary>
    public int[] allValues
    {
        get
        {
            //sum up all the cards (except aces)
            int sum = 0;
            int numAces = 0;
            foreach (Card card in cards)
            {
                if (card.rank == CardRank.Ace)
                {
                    numAces++;
                }
                else
                {
                    sum += card.value;
                }
            }

            //find all the valid combinations of ace sums
            /*note: we do not need to find all different combinations of aces equaling 1 or 11
                    we can optimize this by only finding 0/4=11, 1/4=11, etc.
                     -> num combinations = num aces + 1
             */
            int numAceCombs = numAces + 1;
            int[] aceCombs = new int[numAceCombs];
            for (int i = 0; i < numAceCombs; i++)
            {
                aceCombs[i] = sum + (i * 11) + (numAces - i);
            }
            return aceCombs;
        }
    }

    ///<summary> all the possible valid values (<=21) this hand can equal (including all cases for aces = 1 or 11) </summary>
    public int[] allValidValues
    {
        get
        {
            return allValues.Where(n => n <= 21).ToArray();
        }
    }

    ///<summary> the highest value this hand can equal </summary>
    public int highestValue
    {
        get { return allValues.Max(); }
    }

    ///<summary> the highest valid value (<=21) this hand can equal (or -1 if there are no valid values) </summary>
    public int highestValidValue
    {
        get
        {
            if (allValidValues.Length > 0)
            {
                return allValidValues.Max();

            }
            else return -1;
        }
    }

    ///<summary> if the player has exceeded 21 </summary>
    public bool hasBusted
    {
        get
        {
            //calc values will return all valid values of this hand (<=21)
            //so we know the player busted if it returns no valid values
            return allValidValues.Length == 0;
        }
    }

    //check if the player automatically won (value == 21)
    public bool hasAutoWon
    {
        get { return allValues.Contains(21); }
    }
}
