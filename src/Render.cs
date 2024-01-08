
namespace VesselFeud {
    public enum FgColours
    {
        Black = 0,
        DarkRed = 1,
        DarkGreen = 2,
        DarkYellow = 3,
        DarkBlue = 4,
        DarkMagenta = 5,
        DarkCyan = 6,
        Gray = 7,
        DarkGray = 8,
        Red = 9,
        Green = 10,
        Yellow = 11,
        Blue = 12,
        Magenta = 13,
        Cyan = 14,
        White = 15,
        Default = 0
    }

    public enum BgColours
    {
        Black = 0,
        DarkRed = 16,
        DarkGreen = 32,
        DarkYellow = 48,
        DarkBlue = 64,
        DarkMagenta = 80,
        DarkCyan = 96,
        Gray = 112,
        DarkGray = 128,
        Red = 144,
        Green = 160,
        Yellow = 176,
        Blue = 192,
        Magenta = 208,
        Cyan = 224,
        White = 240,
        Default = 0
    }

    public class Component
    {
        public String self;
        public void Write(string content, FgColours fg = FgColours.White, BgColours bg = BgColours.Default) { String strung = $"\x1b[38;5;{(int)fg}m\x1b[48;5;{(int)bg}m{content}\x1b[0m"; self += strung; if (dimensions.Item1 <= strung.Length) { dimensions.Item1 = strung.Length; } }
        public void WriteLine(string content, FgColours fg = FgColours.White, BgColours bg = BgColours.Default) {String strung = $"\x1b[38;5;{(int)fg}m\x1b[48;5;{(int)bg}m{content}\x1b[0m\n"; self += strung; if(dimensions.Item1 <= strung.Length) { dimensions.Item1 = strung.Length;} dimensions.Item2 += 1; }
        public Component(string self = "") => this.self = self;
        public (int, int) dimensions;
        public void Render(int x, int y)
        {
            foreach (string line in self.Split("\n"))
            {
                Console.SetCursorPosition(x, y++);
                Console.Write(line);

            }
        }


    }




}
