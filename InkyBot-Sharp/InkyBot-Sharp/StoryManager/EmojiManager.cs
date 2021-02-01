using System;
using System.Collections.Generic;
using Discord;

namespace InkyBotSharp
{
    public static class EmojiManager
    {
        public static List<string> NumberEmojis = new List<string> { "1️⃣", "2️⃣", "3️⃣", "4️⃣" };

        public static Emoji GetEmojiFromNumber(int i)
        {
            switch (i)
            {
                case 1:
                    return new Emoji("1️⃣");
                case 2:
                    return new Emoji("2️⃣");
                case 3:
                    return new Emoji("3️⃣");
                case 4:
                    return new Emoji("4️⃣");
                default:
                    return null;
            }
        }

        public static int GetNumberFromEmoji (IEmote emoji)
        {
            switch (emoji.Name)
            {
                case "1️⃣":
                    return 1;
                case "2️⃣":
                    return 2;
                case "3️⃣":
                    return 3;
                case "4️⃣":
                    return 4;

                default:
                    return -1;
            }
        }
    }
}
