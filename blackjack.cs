using System;
using System.Text;

class BlackJack
{
    private static Display? display;
    private static Deck? deck;
    private static Hand? playerHand, dealerHand;

    public static void Main(string[] args)
    {
        display = new Display(ConsoleColor.DarkBlue, ConsoleColor.Gray);

        //Game Sequence
        bool gameRunning = true;
        while (gameRunning)
        {
            //draw display base
            display.ClearScreen();
            drawDisplayBase();
            Thread.Sleep(500);

            //create deck
            deck = new Deck();

            //create player hand
            playerHand = new Hand();
            playerHand.add(deck.drawRandom());
            playerHand.add(deck.drawRandom());
            drawPlayerHand();

            //create dealer hand
            dealerHand = new Hand();
            dealerHand.add(deck.drawRandom());
            dealerHand.add(deck.drawRandom());
            drawDealerHand(true);

            //Player Hitting Loop
            Thread.Sleep(1000);
            while (!dealerHand.hasAutoWon && !playerHand.hasAutoWon)
            {
                setActionMessage("HIT (h) or STAND (s)");
                char hitStandResponse = Console.ReadKey(true).KeyChar;
                clearActionMessage();
                Thread.Sleep(250);
                if (Char.ToLower(hitStandResponse) == 'h')
                {
                    playerHand.add(deck.drawRandom());
                    setActionMessage("HIT");
                    Thread.Sleep(500);
                    drawPlayerHand();
                    Thread.Sleep(500);
                    if (playerHand.hasBusted)
                    {
                        break;
                    }
                    else
                    {
                        clearActionMessage();
                        Thread.Sleep(1000);
                    }
                }
                else if (Char.ToLower(hitStandResponse) == 's')
                {
                    setActionMessage("STAND");
                    Thread.Sleep(1000);
                    clearActionMessage();
                    Thread.Sleep(1000);
                    break;
                }
            }

            //Dealer Hitting Loop
            clearActionMessage();
            drawDealerHand(false);
            while (!playerHand.hasAutoWon && !playerHand.hasBusted && !dealerHand.hasAutoWon && dealerHand.highestValidValue < 17 && !dealerHand.hasBusted)
            {
                setActionMessage("DEALER HIT");
                Thread.Sleep(1000);
                dealerHand.add(deck.drawRandom());
                drawDealerHand(false);
                clearActionMessage();
                Thread.Sleep(1000);
            }
            if (!playerHand.hasAutoWon && !playerHand.hasBusted && !dealerHand.hasBusted)
            {
                setActionMessage("DEALER STANDS");
                Thread.Sleep(1000);
                clearActionMessage();
                Thread.Sleep(1000);
            }

            //Log Solution
            Thread.Sleep(1000);
            Console.Beep();
            if (playerHand.highestValidValue == dealerHand.highestValidValue)
            {
                display.WriteCenteredCentered("TIE GAME", 0, 0);
            }
            else if (playerHand.highestValidValue > dealerHand.highestValidValue)
            {
                display.WriteCenteredCentered("YOU WON!!!", 0, 0);
            }
            else
            {
                display.WriteCenteredCentered("YOU LOST", 0, 0);
            }

            //Play Again
            Thread.Sleep(1000);
            setActionMessage("PLAY AGAIN? (y/n)");
            while (true)
            {
                char playAgainResponse = Console.ReadKey(true).KeyChar;
                Console.WriteLine("");
                if (Char.ToLower(playAgainResponse) == 'n')
                {
                    gameRunning = false;
                    break;
                }
                else if (Char.ToLower(playAgainResponse) == 'y')
                {
                    break;
                }
            }
        }
    }

    public static void drawDisplayBase() 
    {
        //draw edges
        for (int y = 1; y < display.height - 2; y++)
        {
            display.WriteTopLeft("██", 1, y);
            display.WriteTopRight("██", 2, y);
            display.WriteTopCentered("█", 0, y);
        }
        string longEdge = Display.repeat("█", display.width - 2);
        display.WriteTopLeft(longEdge, 1, 1);
        display.WriteBottomLeft(longEdge, 1, 1);

        //draw title
        display.WriteTopCentered("██ BLACKJACK ██", 0, 2);
        display.WriteTopCentered("███████████████", 0, 3);
    }

    public static void drawPlayerHand()
    {
        if (display == null || playerHand == null) return;

        display.WriteTopCentered("YOUR CARDS", -30, 3);
        int c = 0;
        while (c < playerHand.cards.Count)
        {
            display.WriteTopCentered(playerHand.cards[c].ToString(), -30, 4 + c);
            c++;
        }
        if (playerHand.hasBusted)
        {
            display.WriteTopCentered("=BUST", -30, 4 + c);
        }
        else
        {
            display.WriteTopCentered("=" + string.Join(", ", playerHand.allValidValues), -30, 4 + c);
        }
    }

    public static void drawDealerHand(bool redact)
    {
        if (display == null || dealerHand == null) return;

        display.WriteTopCentered("DEALER CARDS", 30, 3);
        int c = 0;
        while (c < dealerHand.cards.Count)
        {
            if (c == 0 && redact)
            {
                int cardStringLength = dealerHand.cards[c].ToString().Length;
                string redactText = Display.repeat("▒", cardStringLength);
                display.WriteTopCentered(redactText, 30, 4 + c);
            }
            else
            {
                display.WriteTopCentered(dealerHand.cards[c].ToString(), 30, 4 + c);
            }
            c++;
        }
        if (dealerHand.hasBusted)
        {
            display.WriteTopCentered("=BUST", 30, 4 + c);
        }
        else if (!redact)
        {
            display.WriteTopCentered("=" + string.Join(", ", dealerHand.allValidValues), 30, 4 + c);
        }
    }

    public static void setActionMessage(string str)
    {
        if (display == null) return;
        display.WriteBottomCentered(str, 0, 3);
    }

    public static void clearActionMessage()
    {
        if (display == null) return;
        display.WriteBottomCentered(Display.repeat(" ", display.width), 0, 3);
        drawDisplayBase();
    }
}