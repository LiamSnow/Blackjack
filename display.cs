using System;
using System.Text;

public class Display
{

    public int cursorTop, cursorLeft;
    public int width { get { return Console.WindowWidth; } }
    public int height { get { return Console.WindowHeight; } }

    public void WriteAt(string str, int x, int y)
    {
        try
        {
            Console.SetCursorPosition(cursorLeft + x, cursorTop + y);
            Console.Write(str);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
        }
    }

    public void WriteCentered(string str, int y) {
        WriteAt(str, (width - str.Length) / 2, y);
    }

    public void ClearScreen()
    {
        StringBuilder blankScreenBuilder = new StringBuilder();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                blankScreenBuilder.Append(" ");
            }
        }
        WriteAt(blankScreenBuilder.ToString(), 0, 0);
    }

    public Display(ConsoleColor backgroundColor, ConsoleColor foregroundColor) {
        Console.Clear();
        cursorTop = Console.CursorTop;
        cursorLeft = Console.CursorLeft;

        Console.CursorVisible = false;
        Console.BackgroundColor = backgroundColor;
        Console.ForegroundColor = foregroundColor;
        ClearScreen();
    }
}
