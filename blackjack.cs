using System;
using System.Text;

class BlackJack
{
    public static void Main(string[] args)
    {
        Display display = new Display(ConsoleColor.DarkBlue, ConsoleColor.Gray);

        //Draw Edges
        for (int y = 1; y < display.height - 2; y++)
        {
            display.WriteAt("██", 1, y);
            display.WriteAt("██", display.width - 3, y);
            display.WriteCentered("█", y);
        }
        StringBuilder longEdgeBuilder = new StringBuilder();
        for (int x = 1; x < display.width - 1; x++)
        {
            longEdgeBuilder.Append("█");
        }
        string longEdge = longEdgeBuilder.ToString();
        display.WriteAt(longEdge, 1, 1);
        display.WriteAt(longEdge, 1, display.height - 2);

        //Draw Title
        display.WriteCentered("██ BLACKJACK ██", 2);
        display.WriteCentered("███████████████", 3);

        

        bool programRunning = true;

        //Game Sequence
        while (programRunning)
        {
            //create deck
            Deck deck = new Deck();

            //create player hand
            Hand playerHand = new Hand();
            playerHand.add(deck.drawRandom());
            playerHand.add(deck.drawRandom());
            //Console.WriteLine("Your current hand = {0}", string.Join(",", playerHand.allValues));
            //Console.Write(playerHand.ToString());

            //create dealer hand
            Hand dealerHand = new Hand();
            dealerHand.add(deck.drawRandom());
            dealerHand.add(deck.drawRandom());
            //Console.WriteLine("The dealers hand is");
            //Console.Write(dealerHand.ToString(true));

            Thread.Sleep(10000000);

            ////Player Hitting Loop
            //while (!dealerHand.hasAutoWon && !playerHand.hasAutoWon)
            //{
            //    Console.WriteLine("\nWould you like to HIT (h) or STAND (s)?");
            //    char hitStandResponse = Console.ReadKey(true).KeyChar;
            //    if (Char.ToLower(hitStandResponse) == 'h')
            //    {
            //        playerHand.add(deck.drawRandom());
            //        Console.WriteLine("HIT -> Your current hand is now = {0}", string.Join(",", playerHand.allValues));
            //        Console.Write(playerHand.ToString());
            //        if (playerHand.hasBusted)
            //        {
            //            break;
            //        }
            //    }
            //    else if (Char.ToLower(hitStandResponse) == 's')
            //    {
            //        Console.WriteLine("STAND");
            //        break;
            //    }
            //}

            ////Dealer Hitting Loop
            //Console.WriteLine("");
            //while (!playerHand.hasAutoWon && !playerHand.hasBusted && !dealerHand.hasAutoWon && dealerHand.highestValidValue < 17 && !dealerHand.hasBusted)
            //{
            //    dealerHand.add(deck.drawRandom());
            //    Console.WriteLine("DEALER HIT", string.Join(",", dealerHand.allValues));
            //}
            //if (!playerHand.hasAutoWon && !playerHand.hasBusted && !dealerHand.hasBusted)
            //{
            //    Console.WriteLine("DEALER STANDS AT {0}", string.Join(",", dealerHand.allValues));
            //    Console.Write(dealerHand.ToString());
            //}

            ////Log Solution
            //Console.WriteLine("");
            //if (playerHand.hasBusted)
            //{
            //    Console.WriteLine("YOU BUST :(((");
            //}
            //else if (dealerHand.hasBusted)
            //{
            //    Console.WriteLine("DEALER BUST -> YOU WIN!!");
            //}
            //else if (playerHand.highestValidValue == dealerHand.highestValidValue)
            //{
            //    Console.WriteLine("TIE GAME - NO WINNER");
            //}
            //else if (playerHand.highestValidValue > dealerHand.highestValidValue)
            //{
            //    Console.WriteLine("YOU WON!!! {0} > {1}", playerHand.highestValidValue, dealerHand.highestValidValue);
            //}
            //else
            //{
            //    Console.WriteLine("YOU LOST :(( {0} < {1}", playerHand.highestValidValue, dealerHand.highestValidValue);
            //}

            ////Play Again
            //while (true)
            //{
            //    Console.Write("\nWould you like to play again? (y/n): ");
            //    char playAgainResponse = Console.ReadKey(true).KeyChar;
            //    Console.WriteLine("");
            //    if (Char.ToLower(playAgainResponse) == 'n')
            //    {
            //        programRunning = false;
            //        break;
            //    }
            //    else if (Char.ToLower(playAgainResponse) == 'y')
            //    {
            //        Console.Clear();
            //        break;
            //    }
            //}
        }
    }
}