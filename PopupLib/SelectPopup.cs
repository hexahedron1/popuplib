namespace PopupLib;
/// <summary>
/// A popup which offers a collections of items for the user to choose from.
/// </summary>
public class SelectPopup : Popup {
    private string[] options = [];

    public string[] Options {
        get {
            return options;
        }
        set {
            options = value;
            Height = Content.Length + options.Length + 1;
            foreach (var t in options) Width = Math.Max(Width, t.Length + 2 + (options.Length).ToString().Length);
        }
    }
    /// <summary>
    /// Creates a SelectPopup
    /// </summary>
    /// <param name="content">The message of the popup</param>
    /// <param name="options">The options of the popup</param>
    /// <param name="wrap">The ideal amount of characters per line</param>
    /// <param name="type">The type of the popup</param>
    /// <param name="title">The text in the title bar</param>
    public SelectPopup(string content, string[] options, string? title = null, int wrap = 32, PopupType type = PopupType.Info) : base(content, title, wrap, type) {
        Options = options;
    }

    /// <summary>
    /// Creates a popup with the values of an enum as options
    /// </summary>
    /// <param name="content">The message of the popup</param>
    /// <param name="wrap">The ideal amount of characters per line</param>
    /// <param name="type">The type of the popup</param>
    /// <param name="title">The text in the title bar</param>
    /// <typeparam name="TEnum">The enum you want to use for the options</typeparam>
    /// <returns>The popup</returns>
    public static SelectPopup FromEnum<TEnum>(string content, string? title = null, int wrap = 32,
        PopupType type = PopupType.Info) where TEnum : Enum =>
        new(content, [], title, wrap, type) { Options = Enum.GetNames(typeof(TEnum)) };
    public override int SetContent(string content, int wrap = 32) {
        int w = base.SetContent(content, wrap);
        Height = Content.Length + options.Length + 1;
        return w;
    }

    private void Render(int selected = 0, int x = -1, int y = -1) {
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
        for (int i = 0; i < Options.Length; i++) {
            Console.SetCursorPosition(x + 2, y + Content.Length + i + 2);
            if (i == selected)
                Console.Write("\e[7m");
            Console.Write($"{i.ToString().PadRight(Options.Length.ToString().Length, '0')}. {Options[i]}");
            if (i == selected)
                Console.Write("\e[0m");
        }
    }
    /// <summary>
    /// Shows the popup and waits for the user to choose a value
    /// </summary>
    /// <param name="x">The x coordinate of the top-left corner. Set to -1 to use center of console window</param>
    /// <param name="y">The y coordinate of the top-left corner. Set to -1 to use center of console window</param>
    /// <returns>The selected option</returns>
    public new string Show(int x = -1, int y = -1) {
        Console.CursorVisible = false;
        int selected = 0;
        while (true) {
            Render(selected, x, y);
            var key = Console.ReadKey(true);
            switch (key.Key) {
                case ConsoleKey.UpArrow when selected > 0: selected--; break;
                case ConsoleKey.DownArrow when selected < Options.Length - 1: selected++; break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    return Options[selected];
            }
        }
    }
    /// <summary>
    /// Shortcut method for showing a popup
    /// </summary>
    /// <param name="content">The message of the popup</param>
    /// <param name="options">The options of the popup</param>
    /// <param name="x">The x coordinate of the top-left corner. Set to -1 to use center of console window</param>
    /// <param name="y">The y coordinate of the top-left corner. Set to -1 to use center of console window</param>
    /// <param name="wrap">The ideal amount of characters per line</param>
    /// <param name="type">The type of the popup</param>
    /// <param name="title">The text in the title bar</param>
    /// <returns>The key pressed by the user</returns>
    public static string Quick(string content, string[] options, int x = -1, int y = -1, string? title = null, int wrap = 32,
        PopupType type = PopupType.Info) {
        return new SelectPopup(content, options, title, wrap, type).Show(x, y);
    }
}
