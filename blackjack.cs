using System;
using System.Text;

enum CardSuit { Heart, Diamond, Club, Spade};
enum CardRank { N2, N3, N4, N5, N6, N7, N8, N9, N10, Jack, Queen, King, Ace };

readonly struct Card
{
    public readonly CardSuit suit;
    public readonly CardRank rank;

    public Card(CardSuit suit, CardRank rank)
    {
        this.suit = suit;
        this.rank = rank;
    }

    public int value
    {
        get
        {
            //Ace
            if (rank == CardRank.Ace)
            {
                return 1;
            }
            //2-10
            else if (rank >= CardRank.N2 && rank <= CardRank.N10)
            {
                return (int)rank+2;
            }
            //Face
            else
            {
                return 10;
            }
        }
    }

    public override string ToString()
    {
        if (rank >= CardRank.N2 && rank <= CardRank.N10)
        {
            return $"{value} of {suit}s";
        }
        else
        {
            return $"{rank} of {suit}s";
        }
    }
}

class Hand
{
    public List<Card> cards { get; } = new List<Card>();

    public void add(Card card) {
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
        get {
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
        get {
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

    public override string ToString()
    {
        return ToString(false);
    }

    public string ToString(bool isDealer) {
        StringBuilder sb = new StringBuilder();
        bool redactFirst = isDealer;
        foreach (Card card in cards)
        {
            //TODO dealer code
            sb.Append(" - ");
            sb.Append(redactFirst ? "REDACTED" : card.ToString());
            sb.Append("\n");
            redactFirst = false;
        }
        return sb.ToString();
    }
}

class Deck 
{ 
    public List<Card> cards { get; } = new List<Card>();

    public Deck()
    {
        foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
        {
            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }

    public Card drawRandom() {
        Random rand = new Random();
        int drawnCardIndex = rand.Next(cards.Count);
        Card drawnCard = cards[drawnCardIndex];
        cards.RemoveAt(drawnCardIndex);
        return drawnCard;
    }
}

class BlackJack
{
    public static void Main(string[] args)
    {
        bool programRunning = true;

        //Game Sequence (each loop in a game)
        while (programRunning)
        {
            //intro
            Console.WriteLine("\t<<< BLACKJACK >>>\n");

            //create deck
            Deck deck = new Deck();

            //create player hand
            Hand playerHand = new Hand();
            playerHand.add(deck.drawRandom());
            playerHand.add(deck.drawRandom());
            Console.WriteLine("Your current hand = {0}", string.Join(",", playerHand.allValues));
            Console.Write(playerHand.ToString());

            //create dealer hand
            Hand dealerHand = new Hand();
            dealerHand.add(deck.drawRandom());
            dealerHand.add(deck.drawRandom());
            Console.WriteLine("The dealers hand is");
            Console.Write(dealerHand.ToString(true));

            //Player Hitting Loop
            while (!dealerHand.hasAutoWon && !playerHand.hasAutoWon)
            {
                Console.WriteLine("\nWould you like to HIT (h) or STAND (s)?");
                char hitStandResponse = Console.ReadKey(true).KeyChar;
                if (Char.ToLower(hitStandResponse) == 'h')
                {
                    playerHand.add(deck.drawRandom());
                    Console.WriteLine("HIT -> Your current hand is now = {0}", string.Join(",", playerHand.allValues));
                    Console.Write(playerHand.ToString());
                    if (playerHand.hasBusted)
                    {
                        break;
                    }
                }
                else if (Char.ToLower(hitStandResponse) == 's')
                {
                    Console.WriteLine("STAND");
                    break;
                }
            }

            //Dealer Hitting Loop
            Console.WriteLine("");
            while (!playerHand.hasAutoWon && !playerHand.hasBusted && !dealerHand.hasAutoWon && dealerHand.highestValidValue < 17 && !dealerHand.hasBusted) 
            {
                dealerHand.add(deck.drawRandom());
                Console.WriteLine("DEALER HIT", string.Join(",", dealerHand.allValues));
            }
            if (!playerHand.hasAutoWon && !playerHand.hasBusted && !dealerHand.hasBusted) {
                Console.WriteLine("DEALER STANDS AT {0}", string.Join(",", dealerHand.allValues));
                Console.Write(dealerHand.ToString());
            }

            //Check Who Won
            Console.WriteLine("");
            if (playerHand.hasBusted)
            {
                Console.WriteLine("YOU BUST :(((");
            }
            else if (dealerHand.hasBusted)
            {
                Console.WriteLine("DEALER BUST -> YOU WIN!!");
            }
            else if (playerHand.highestValidValue == dealerHand.highestValidValue) 
            {
                Console.WriteLine("TIE GAME - NO WINNER");
            }
            else if (playerHand.highestValidValue > dealerHand.highestValidValue)
            {
                Console.WriteLine("YOU WON!!! {0} > {1}", playerHand.highestValidValue, dealerHand.highestValidValue);
            }
            else
            {
                Console.WriteLine("YOU LOST :(( {0} < {1}", playerHand.highestValidValue, dealerHand.highestValidValue);
            }

            //Play Again
            while (true)
            {
                Console.Write("\nWould you like to play again? (y/n): ");
                char playAgainResponse = Console.ReadKey(true).KeyChar;
                Console.WriteLine("");
                if (Char.ToLower(playAgainResponse) == 'n')
                {
                    programRunning = false;
                    break;
                }
                else if (Char.ToLower(playAgainResponse) == 'y')
                {
                    Console.Write("\n\n");
                    break;
                }
            }
        }
    }
}