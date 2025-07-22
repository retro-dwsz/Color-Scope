using System.Runtime.CompilerServices;

using System.Drawing;
using System.Text;

class ColorText {
    public enum Position { Back, Fore };

    private static ConsoleColor RgbToConsoleColor(byte r, byte g, byte b)
    {
        // Naive RGB -> ConsoleColor mapping
        int index = (r > 128 ? 4 : 0) + (g > 128 ? 2 : 0) + (b > 128 ? 1 : 0);
        return (ConsoleColor)index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ColorStr(string text = "Hello, world!", uint hex = 0xFF109696, Position pos = Position.Fore)
    {
        // Parse RGB from 0xAARRGGBB or 0xRRGGBB
        byte a = 255, r, g, b;
        if (hex > 0xFFFFFF)
        {
            a = (byte)((hex >> 24) & 0xFF);
            r = (byte)((hex >> 16) & 0xFF);
            g = (byte)((hex >> 8) & 0xFF);
            b = (byte)(hex & 0xFF);
        }
        else
        {
            r = (byte)((hex >> 16) & 0xFF);
            g = (byte)((hex >> 8) & 0xFF);
            b = (byte)(hex & 0xFF);
        }

        // Convert RGB to closest ConsoleColor
        ConsoleColor color = RgbToConsoleColor(r, g, b);

        if (pos == Position.Fore)
            Console.ForegroundColor = color;
        else
            Console.BackgroundColor = color;

        return text;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Print(uint hex, Position pos, string text)
    {
        Console.WriteLine(ColorStr(text, hex, pos));
        Console.ResetColor(); // Don't leave your terminal cursed
    }
}

class Program
{
    public static string RgbToHex(int red, int green, int blue)
    {
        // Ensure the RGB values are within the valid range (0-255)
        red = Math.Clamp(red, 0, 255);
        green = Math.Clamp(green, 0, 255);
        blue = Math.Clamp(blue, 0, 255);

        // Convert each color component to a hexadecimal string
        string redHex = red.ToString("X2");
        string greenHex = green.ToString("X2");
        string blueHex = blue.ToString("X2");

        // Concatenate the hexadecimal values and return the result
        return "#" + redHex + greenHex + blueHex;
    }

    public static uint RgbToHexInt(int red, int green, int blue)
    {
        red = Math.Clamp(red, 0, 255);
        green = Math.Clamp(green, 0, 255);
        blue = Math.Clamp(blue, 0, 255);

        // Gabungkan komponen RGB menjadi 24-bit integer (0xRRGGBB)
        return (uint)((red << 16) | (green << 8) | blue);
    }

    static void Main(string[] args)
    {
        // Ganti nama file gambar di sini
        string inputFile = "gambar.jpg";

        Console.WriteLine("Processing image . . .");

        string outputFile = $"{Path.GetFileNameWithoutExtension(inputFile)}_Color.txt";

        using var image = new Bitmap(inputFile);
        var sb = new StringBuilder();

        List<int> CLR_R = new List<int>();
        List<int> CLR_G = new List<int>();
        List<int> CLR_B = new List<int>();

        for (int y = 0; y < image.Height; y++)
        {
            sb.Append($"row {(y + 1).ToString("D3")}: [ ");
            for (int x = 0; x < image.Width; x++)
            {
                Color pixel = image.GetPixel(x, y);
                sb.Append($"[{pixel.R}, {pixel.G}, {pixel.B}]");

                CLR_R.Add(pixel.R);
                CLR_G.Add(pixel.G);
                CLR_B.Add(pixel.B);

                if (x < image.Width - 1)
                    sb.Append(", ");
            }
            sb.AppendLine(" ]");
        }

        int AVG_R = CLR_R.Sum() / CLR_R.Capacity;
        int AVG_G = CLR_G.Sum() / CLR_G.Capacity;
        int AVG_B = CLR_B.Sum() / CLR_B.Capacity;

        uint HEX = RgbToHexInt(AVG_R, AVG_G, AVG_B);

        Console.WriteLine($"Averages:");
        Console.WriteLine($"[R, G, B] = [{AVG_R} {AVG_G}, {AVG_B}]");
        Console.Write($"HEX = ");
        ColorText.Print(HEX, ColorText.Position.Fore, HEX.ToString());

        File.WriteAllText(outputFile, sb.ToString());
        Console.WriteLine($"Done! Output saved to: {outputFile}");
    }
}
