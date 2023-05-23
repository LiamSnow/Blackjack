using System;

enum CardSuit { Heart, Diamond, Club, Spade };
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
                return (int)rank + 2;
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