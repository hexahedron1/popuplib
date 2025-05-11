using PopupLib;

//Popup.Shading = '·';
SelectPopup p = SelectPopup.FromEnum<PopupType>("Select one:");
while (true) {
    var option = p.Show();
    p.SetContent($"You selected: {option}\nSelect one:");
    if (option == "Custom") break;
}
Console.Clear();
Console.CursorVisible = true;