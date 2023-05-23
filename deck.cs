using System;

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

    public Card drawRandom()
    {
        Random rand = new Random();
        int drawnCardIndex = rand.Next(cards.Count);
        Card drawnCard = cards[drawnCardIndex];
        cards.RemoveAt(drawnCardIndex);
        return drawnCard;
    }
}