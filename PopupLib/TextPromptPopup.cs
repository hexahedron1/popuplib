namespace PopupLib;

public class TextPromptPopup : Popup {
    public TextPromptPopup(string content, string placeholder = "", int? limit = null, string? title = null, int wrap = 32,
        PopupType type = PopupType.Info) : base(content, title, wrap, type) {
        Placeholder = placeholder;
        Width = Math.Max(Width, Placeholder.Length + 4);
        Height = Content.Length + 3;
        Limit = limit;
    }

    public string Prompt { get; private set; } = "";
    public string Placeholder { get; set; }
    public int? Limit { get; set; }
    public int Offset { get; set; }
    public override void Render(int x = -1, int y = -1) {
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
        Console.SetCursorPosition(x + 2, y + Content.Length + 3);
        if (Prompt.Length < Limit - 8 || Limit is null) Console.ResetColor();
        else Console.ForegroundColor = Prompt.Length != Limit ? ConsoleColor.Yellow : ConsoleColor.Red;
        Console.Write(Limit is null ? $"{Prompt.Length}" : $"{Prompt.Length}/{Limit}");
        Console.ResetColor();
        Console.SetCursorPosition(x + 2, y + Content.Length + 2);
        if (Prompt.Length == 0) {
            Console.Write("\e[1m");
            Console.Write(Placeholder);
            Console.Write("\e[0m");
            Console.SetCursorPosition(x + 2, y + Content.Length + 2);
            Console.CursorVisible = false;
        } else {
            if (Prompt.Length > (Width - 1)) {
                //Console.ForegroundColor = ConsoleColor.Black;
                //Console.BackgroundColor = ConsoleColor.White;
                Console.Write("\e[7m<\e[27m");
                //Console.ResetColor();
                Console.SetCursorPosition(x + 3, y + Content.Length + 2);
            }
            Console.Write("\e[1m");
            Console.Write(Prompt.Length > (Width - 1) ? Prompt[(Offset + 2)..] : Prompt);
            Console.Write("\e[0m");
            Console.CursorVisible = true;
        }
    }
    public new string Show(int x = -1, int y = -1) {
        Console.CursorVisible = false;
        while (true) {
            Render(x, y);
            var key = Console.ReadKey(true);
            switch (key.Key) {
                case ConsoleKey.Enter:
                    Console.Clear();
                    return Prompt;
                case ConsoleKey.Backspace:
                    if (Prompt.Length > 0)
                        Prompt = Prompt.Remove(Prompt.Length - 1, 1);
                    break;
                default:
                    if (!char.IsControl(key.KeyChar) && (Limit is null || Prompt.Length < Limit)) {
                        Prompt += key.KeyChar;
                    }
                    //Title = string.Join(" ", System.Text.Encoding.Unicode.GetBytes(key.KeyChar.ToString()));
                    break;
            }
            Offset = Prompt.Length > Width ? Prompt.Length - Width : 0;
        }
    }public static string Quick(string content, int x = -1, int y = -1, string placeholder = "", int? limit = null, string? title = null, int wrap = 32,
        PopupType type = PopupType.Info) {
        return new TextPromptPopup(content, placeholder, limit, title, wrap, type).Show(x, y);
    }
}