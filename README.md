A library for showing popup windows in the terminal.  
<<<<<<< HEAD
# Basic usage
You can either use the quick method:
```cs
Popup.Quick("Hello, world!")
```
or create an object to reuse the same popup:
```cs
Popup popup = new("Hello, world!")
=======
![image](https://github.com/user-attachments/assets/1c940825-c92f-457d-b339-2cf6d1aeef91)
# Basic usage
You can either use the quick method:
```cs
Popup.Quick("Hello, world!");
```
or create an object to reuse the same popup:
```cs
Popup popup = new("Hello, world!");
>>>>>>> parent of 4071aaa (day 5 of torturing README.md)
popup.Show();
```
# Popup types
Currently there are 2 types:
- **Popup** - simple popup, shows text, closes on key press and returns the pressed key
<<<<<<< HEAD
- **SelectPopup** - provides a selection of options to the user, closes on enter key and returns selected item  
=======
- **SelectPopup** - provides a selection of options to the user, closes on enter key and returns selected item
>>>>>>> parent of 4071aaa (day 5 of torturing README.md)
More types will be added in the future
