using System.Net.Quic;
using System.Runtime.CompilerServices;

namespace PopupLib;
/// <summary>
/// A simple popup that returns the key pressed.
/// </summary>
public class Popup {
    /// <summary>
    /// The width of the popup window.
    /// </summary>
    public int Width { get; set; } = 0;

    /// <summary>
    /// The height of the popup window.
    /// </summary>
    public int Height { get; set; } = 0;
    /// <summary>
    /// The title of the popup, shown inline with the top border.
    /// </summary>
    public string? Title { get; set; }
    public PopupType Type { get; set; }
    /// <summary>
    /// An override for the icon for the popup.
    /// </summary>
    public char? Icon { get; set; }
    /// <summary>
    /// An override for the ANSI formatting of the icon.
    /// Can be any valid ANSI code expression. May omit the <code>\e[</code> at the beginning.
    /// </summary>
    public string? Format { get; set; }
    /// <summary>
    /// The text content of the popup.
    /// Don't set manually, use <c>SetContent</c> instead.
    /// </summary>
    public string[] Content { get; set; }
    internal static readonly Dictionary<PopupType, Tuple<char, string>> Formatting = new() {
        { PopupType.Info, new('i', "94m") },
        { PopupType.Question, new('?', "96m") },
        { PopupType.Warning, new('!', "93m") },
        { PopupType.Error, new('x', "91m") },
        { PopupType.Custom, new('◆', "5;97m") },
    };
    /// <summary>
    /// The character used for the shadow.
    /// </summary>
    public static char Shading = '░';
    /// <summary>
    /// Splits the content into lines based on wrapping parameter and adjusts width if necessary
    /// </summary>
    /// <param name="content">The new content</param>
    /// <param name="wrap">How many characters can be fit into a line in an ideal case</param>
    /// <returns>The actual width</returns>
    public virtual int SetContent(string content, int wrap = 32) {
        List<string> lines = [];
        string[] words = content.Split(' ');
        int totalLength = 0;
        List<string> wordBucket = [];
        int maxLength = 0;
        foreach (var word in words) {
            if (totalLength > wrap) {
                string[] idk = string.Join(' ', wordBucket).Split('\n');
                for (int i = 0; i < idk.Length; i++) {
                    idk[i] = idk[i].Trim();
                    maxLength = Math.Max(maxLength, idk[i].Length);
                }
                lines = lines.Concat(idk).ToList();
                wordBucket.Clear();
                totalLength = 0;
            }
            totalLength += word.Length + (wordBucket.Count == 0 ? 0 : 1);
            wordBucket.Add(word);
        }
        {
            string[] idk = string.Join(' ', wordBucket).Split('\n');
            for (int i = 0; i < idk.Length; i++) {
                idk[i] = idk[i].Trim();
                maxLength = Math.Max(maxLength, idk[i].Length);
            }
            lines = lines.Concat(idk).ToList();
        }
        Content = lines.ToArray();
        Width = new[] { maxLength, (Title?.Length + 2) ?? 0, Width }.Max();
        Height = Content.Length;
        return maxLength;
    }
    /// <summary>
    /// The primary constructor.
    /// </summary>
    /// <param name="content">The message of the popup</param>
    /// <param name="wrap">The ideal amount of characters per line</param>
    /// <param name="type">The type of the popup</param>
    /// <param name="title">The text in the title bar</param>

    public Popup(string content,  string? title = null, int wrap = 32, PopupType type = PopupType.Info) {
        Content = []; // This exists solely to make rider shut up
        Width = 0; // And this
        Title = title;
        Type = type;
        SetContent(content, wrap);
    }
    /// <summary>
    /// The method that displays the popup window in console
    /// <param name="x">The x coordinate of the top-left corner. Set to -1 to use center of console window</param>
    /// <param name="y">The y coordinate of the top-left corner. Set to -1 to use center of console window</param>
    /// </summary>
    public virtual void Render(int x = -1, int y = -1) {
        if (Console.WindowWidth < Width + 4 || Console.WindowHeight < Height + 2) {
            Console.Clear();
            Console.Write("\e[91mUnable to fit window on screen\e[0m");
            return;
        }
        if (x == -1) x = (Console.WindowWidth - Width) / 2;
        if (y == -1) y = (Console.WindowHeight - Height) / 2;
        Console.SetCursorPosition(x, y);
        var format = Formatting[Type];
        Console.Write($"╔╡\e[{Format ?? format.Item2}{Icon ?? format.Item1}\e[0m{(Title is null ? ('╞' + new string('═', Width - 1)) : $"║{Title}╞".PadRight(Width, '═'))}╗"); //╡
        for (int i = 0; i < Height; i++) {
            Console.SetCursorPosition(x, y + i + 1);
            Console.Write($"║{new string(' ', Width + 2)}║{Shading}");
        }
        for (int i = 0; i < Content.Length; i++) {
            Console.SetCursorPosition(x + 2, y + i + 1);
            Console.Write(Content[i]);
        }
        Console.SetCursorPosition(x, y + Height + 1);
        Console.Write($"╚{new string('═', Width + 2)}╝{Shading}");
        Console.SetCursorPosition(x, y + Height + 2);
        Console.Write(' ' + new string(Shading, Width + 4));
    }
    /// <summary>
    /// Shows the popup and handles all interaction.
    /// </summary>
    /// <param name="x">The x coordinate of the top-left corner. Set to -1 to use center of console window</param>
    /// <param name="y">The y coordinate of the top-left corner. Set to -1 to use center of console window</param>
    /// <returns>The key pressed by the user</returns>
    public ConsoleKeyInfo Show(int x = -1, int y = -1) {
        Render(x, y);
        Console.CursorVisible = false;
        var key = Console.ReadKey(true);
        Console.Clear();
        return key;
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
/// <returns>The key pressed by the user</returns>
    public static ConsoleKeyInfo Quick(string content, int x = -1, int y = -1, string? title = null, int wrap = 32,
        PopupType type = PopupType.Info) {
        return new Popup(content, title, wrap, type).Show(x, y);
    }
}



public enum PopupType {
    Info,
    Question,
    Warning,
    Error,
    Custom
}