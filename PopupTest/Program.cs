using PopupLib;
Console.Clear();
Popup popup = new Popup("Hello World!", type: PopupType.Custom) {
    Icon = '*',
    Format = "95m"
};
popup.Show();
Console.CursorVisible = true;