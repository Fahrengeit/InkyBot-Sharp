using System;
using System.Collections.Generic;
using Discord;

namespace InkyBotSharp
{
    public class EmojiManager
    {
        public List<string> NumberEmojis = new List<string> { "1️⃣", "2️⃣", "3️⃣", "4️⃣", "5️⃣", "6️⃣", "7️⃣", "8️⃣", "9️⃣" };

        public Emoji GetEmojiFromNumber(int i)
        {
            if (i <= NumberEmojis.Count && i > 0)
            {
                return new Emoji(NumberEmojis[i-1]);
            }
            else
            {
                return null;
            }
        }

        public int GetNumberFromEmoji (IEmote emoji)
        {
            if (NumberEmojis.Contains(emoji.Name))
            {
                return NumberEmojis.IndexOf(emoji.Name)+1;
            }
            else
            {
                return -1;
            }
        }
    }
}
