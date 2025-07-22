# 🎨 Image RGB Analyzer & ConsoleColor Mapper

A small C# console app that:
- Reads pixel data from an image
- Logs RGB values row by row
- Computes average color
- Converts that color to a simplified `ConsoleColor`
- Prints the average colorized text in the terminal!

Perfect for understanding basic image processing, RGB math, and terminal coloring in C#.

---

## 🚀 Features

✅ Load a bitmap image  
✅ Extract and log all RGB pixel values  
✅ Auto-format output with padded row numbers  
✅ Calculate average RGB across the entire image  
✅ Convert RGB → Hex → ConsoleColor  
✅ Display average color as a colored string in the terminal  
✅ Save full RGB log as a text file

---

## 📂 Output Example

```

row 001: \[ \[45, 134, 200], \[50, 128, 190], ..., \[255, 255, 255] ]
row 002: \[ \[40, 120, 180], \[47, 115, 178], ..., \[200, 200, 200] ]
...

```

Console Output:

```

Processing image . . .
Averages:
\[R, G, B] = \[112, 137, 189]
HEX = 007189BD ← printed in color!
Done! Output saved to: gambar\_Color.txt

````

---

## 🧠 How It Works

### `ColorText.ColorStr(string, hex, pos)`
- Converts a `uint` hex (e.g., `0xFF336699`) to `ConsoleColor`
- Applies color to **Foreground** or **Background**
- Returns the string with color applied

### `RgbToHexInt(R, G, B)`
- Converts 3 RGB integers into `0xRRGGBB` uint

---

## 🛠️ Requirements

- .NET 6.0+  
- An image file named `gambar.jpg` in the working directory

---

## 🧪 How To Use

1. Clone or copy the code  
2. Place an image named `gambar.jpg` next to the `.exe`  
3. Run the app:

```bash
dotnet run
````

4. Check the output text file:
   `gambar_Color.txt`

---

## 📦 TODO / Improvements

* [ ] Auto image resizing to avoid massive logs
* [ ] Full 24-bit RGB → Terminal color with ANSI support (not just `ConsoleColor`)
* [ ] CLI arguments: input/output filenames, image scaling, etc.
* [ ] Render actual "ASCII art" using dominant pixel colors

---

## 🧃 Dev Notes

* Console coloring is limited to 16 basic colors due to `ConsoleColor`
* Terminal RGB precision is lossy: this code uses a naive mapping
* `MethodImplOptions.AggressiveInlining` is used for performance hinting

---

## 📸 Screenshot (Sample)

> Not available in Markdown. Just run it and see ✨
> Image size too big? Scale it down first!

---

## 📝 License

free to use, modify, share, or whatever.

---

## 👨‍💻 Author

Built with ❤️ by a Gen-Z dev