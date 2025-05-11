![NuGet Version](https://img.shields.io/nuget/v/PopupLib)


A library for showing popup windows in the terminal.  
![image](https://private-user-images.githubusercontent.com/145489575/442511978-1c940825-c92f-457d-b339-2cf6d1aeef91.png?jwt=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3NDY5NzU0NjEsIm5iZiI6MTc0Njk3NTE2MSwicGF0aCI6Ii8xNDU0ODk1NzUvNDQyNTExOTc4LTFjOTQwODI1LWM5MmYtNDU3ZC1iMzM5LTJjZjZkMWFlZWY5MS5wbmc_WC1BbXotQWxnb3JpdGhtPUFXUzQtSE1BQy1TSEEyNTYmWC1BbXotQ3JlZGVudGlhbD1BS0lBVkNPRFlMU0E1M1BRSzRaQSUyRjIwMjUwNTExJTJGdXMtZWFzdC0xJTJGczMlMkZhd3M0X3JlcXVlc3QmWC1BbXotRGF0ZT0yMDI1MDUxMVQxNDUyNDFaJlgtQW16LUV4cGlyZXM9MzAwJlgtQW16LVNpZ25hdHVyZT0yNDhiMDc4YTU1MTgzMWRkMmE2MDcwNDIzZTU0YjhlODlmZTA4ZDU0ODNhNTJhNTFmODQ1Yzg3OGRmM2U4YTFlJlgtQW16LVNpZ25lZEhlYWRlcnM9aG9zdCJ9.0x4H9VlWce_RCH-GjQgeQJDFVwz3tGD3Iyi_vJZHEHE)
# Basic usage
You can either use the quick method:
```cs
Popup.Quick("Hello, world!")
```
or create an object to reuse the same popup:
```cs
Popup popup = new("Hello, world!")
popup.Show();
```
# Popup types
Currently there are 2 types:
- **Popup** - simple popup, shows text, closes on key press and returns the pressed key
- **SelectPopup** - provides a selection of options to the user, closes on enter key and returns selected item

More types will be added in the future
