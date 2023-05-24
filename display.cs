using System;
using System.Text;

public class Display
{

    public int cursorTop, cursorLeft;
    public int width { get { return Console.WindowWidth; } }
    public int height { get { return Console.WindowHeight; } }

    public void WriteTopLeft(string str, int x, int y)
    {
        try
        {
            Console.SetCursorPosition(cursorLeft + x, cursorTop + y);
            Console.Write(str);
        }
        catch (ArgumentOutOfRangeException e) { }
    }

    public void WriteTopRight(string str, int x, int y)
    {
        WriteTopLeft(str, width - x - 1, y);
    }

    public void WriteBottomLeft(string str, int x, int y)
    {
        WriteTopLeft(str, x, height - y - 1);
    }

    public void WriteBottomRight(string str, int x, int y)
    {
        WriteBottomLeft(str, width - x - 1, y);
    }

    public void WriteTopCentered(string str, int x, int y) {
        WriteTopLeft(str, ((width - str.Length) / 2) + x, y);
    }

    public void WriteBottomCentered(string str, int x, int y)
    {
        WriteBottomLeft(str, ((width - str.Length) / 2) + x, y);
    }

    public void WriteCenteredLeft(string str, int x, int y)
    {
        WriteTopLeft(str, x, ((height) / 2) + y);
    }

    public void WriteCenteredRight(string str, int x, int y)
    {
        WriteTopRight(str, x, ((height) / 2) + y);
    }

    public void WriteCenteredCentered(string str, int x, int y)
    {
        WriteTopLeft(str, ((width - str.Length) / 2) + x, ((height) / 2) + y);
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
        WriteTopLeft(blankScreenBuilder.ToString(), 0, 0);
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

    public static string repeat(string str, int times) { 
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < times; i++)
        { 
            builder.Append(str);
        }
        return builder.ToString();
    }

    public static string clear(string str) {
        return repeat(" ", str.Length);
    }
}
