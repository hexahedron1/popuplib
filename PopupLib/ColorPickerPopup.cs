namespace PopupLib;

/// <summary>
/// A popup that lets the user select a ConsoleColor
/// </summary>

public class ColorPickerPopup : Popup {
    /// <summary>
    /// Creates a ColorPickerPopup
    /// </summary>
    /// <param name="content">The message of the popup</param>
    /// <param name="title">The text in the title bar</param>
    /// <param name="wrap">The ideal amount of characters per line</param>
    /// <param name="type">The type of the popup</param>
    /// <param name="squares">Wheter to display <c>█░</c> in the preview rather than <c>Aa</c></param>
    public ColorPickerPopup(string content, string? title = null, int wrap = 32, PopupType type = PopupType.Info, bool squares = false) :
        base(content, title, wrap, type) {
        Height = content.Length + 2;
        Width = Math.Max(16, Width);
        Squares = squares;
    }
    /// <summary>
    /// Shortcut method for showing a popup
    /// </summary>
    /// <param name="content">The message of the popup</param>
    /// <param name="x">The x coordinate of the top-left corner. Set to -1 to use center of console window</param>
    /// <param name="y">The y coordinate of the top-left corner. Set to -1 to use center of console window</param>
    /// <param name="wrap">The ideal amount of characters per line</param>
    /// <param name="type">The type of the popup</param>
    /// <param name="title">The text in the title bar</param>
    /// <param name="squares">Wheter to display <c>█░</c> in the preview rather than <c>Aa</c></param>
    /// <returns>The key pressed by the user</returns>
    public static ConsoleColor Quick(string content, int x = -1, int y = -1, string? title = null, int wrap = 32,
        PopupType type = PopupType.Info, bool squares = false) {
        return new ColorPickerPopup(content, title, wrap, type, squares).Show(x, y);
    }
    public bool Squares { get; set; }
    public void Render(int selected = 0, int x = -1, int y = -1) {
        if (Console.WindowWidth < Width + 4 || Console.WindowHeight < Height + 2) {
            Console.Clear();
            Console.Write("\e[91mUnable to fit window on screen\e[0m");
            return;
        }
        if (x == -1) x = (Console.WindowWidth - Width) / 2;
        if (y == -1) y = (Console.WindowHeight - Height) / 2;
        base.Render(x, y);
        Console.SetCursorPosition(x, y + Content.Length + 1);
        Console.Write($"╟{new string('─', Width + 2)}╢{Shading}");//╟─╢
        if (selected % 8 == 0) {
            Console.SetCursorPosition(x + 1, y + Content.Length + 2 + selected / 8);
            Console.Write(">");
        }

        for (int i = 0; i < 16; i++) {
            Console.SetCursorPosition(x + 2 + (i%8)*2, y + Content.Length + 2 + (i/8));
            if (selected == i) {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = (ConsoleColor)i;
            } else {
                Console.ForegroundColor = (ConsoleColor)i;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.Write(Squares ? "█░" : "Aa");
        }
        /*Console.SetCursorPosition(x, y + Content.Length + 4);
        Console.Write($"╟{new string('─', Width + 2)}╢{Shading}");//╟─╢
        Console.SetCursorPosition(x + 2, y + Content.Length + 5);
        //                 ................
        Console.WriteLine("Use arrows to");
        Console.SetCursorPosition(x + 2, y + Content.Length + 6);
        Console.WriteLine("navigate, use");
        Console.SetCursorPosition(x + 2, y + Content.Length + 7);
        Console.WriteLine("enter to submit");*/
        Console.ResetColor();
    }
    public new ConsoleColor Show(int x = -1, int y = -1) {
        Console.CursorVisible = false;
        int selected = 0;
        while (true) {
            Render(selected, x, y);
            var key = Console.ReadKey(true);
            switch (key.Key) {
                case ConsoleKey.UpArrow when selected > 7: selected -= 8; break;
                case ConsoleKey.DownArrow when selected < 8: selected += 8; break;
                case ConsoleKey.LeftArrow when selected > 0: selected--; break;
                case ConsoleKey.RightArrow when selected < 15: selected++; break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    return (ConsoleColor)selected;
            }
        }
    }
}