using PopupLib;
Console.Clear();
bool sq = SelectPopup.Quick("Display squares?", ["Yes", "No"]) == "Yes";
ConsoleColor col = ColorPickerPopup.Quick("test", squares: sq);
Console.Clear();
Console.ForegroundColor = col;
Console.WriteLine("The quick brown fox jumps over the lazy dog");
Console.ResetColor();
Console.CursorVisible = true;