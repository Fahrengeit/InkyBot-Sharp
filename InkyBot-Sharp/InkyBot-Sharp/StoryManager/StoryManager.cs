using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Ink.Runtime;

namespace InkyBotSharp
{
    public class StoryManager
    {
        private Story _story;
        private DiscordSocketClient _discord;

        public StoryManager(
            DiscordSocketClient discord)
        {
            _discord = discord;
            
            _discord.ReactionAdded += OnReactionAdded;
        }


        private string GetJsonFromResources(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(name);

            using (StreamReader r = new StreamReader(stream))
            {
                string json = r.ReadToEnd();
                return json;
            }
        }

        public async Task StartStory(string path, ISocketMessageChannel channel)
        {
            if (path == null || path == "")
            {
                _story = new Story(GetJsonFromResources("InkyBotSharp.Ink.intercept.json"));
            }
            await ContinueStory(channel);
        }

        public async Task MakeChoice(int choiceIndex, ISocketMessageChannel channel)
        {
            if (_story.currentChoices.Count > choiceIndex)
            {
                if (choiceIndex != -1)
                {;
                    _story.ChooseChoiceIndex(choiceIndex);
                    await ContinueStory(channel);
                }
            }
        }

        private async Task OnReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (_story == null) return;
            if (!reaction.User.Value.IsBot && EmojiManager.NumberEmojis.Contains(reaction.Emote.Name))
            {
                var message = await channel.GetMessageAsync(reaction.MessageId) as IUserMessage;
                var allEmotes = message.Reactions.Keys.ToArray();
                message.RemoveReactionsAsync(message.Author, allEmotes);

                int choiceIndex = EmojiManager.GetNumberFromEmoji(reaction.Emote);

                await MakeChoice(choiceIndex - 1, channel);
            }
        }

        


        private async Task ContinueStory(ISocketMessageChannel channel)
        {
            if (_story == null) return;

            string storyText = "```";
            // Read all the content until we can't continue any more
            while (_story.canContinue)
            {
                // Continue gets the next line of the story
                string text = _story.Continue();
                // This removes any white space from the text.
                text = text.Trim();
                if (text == null || text == "")
                    continue;
                storyText += $"{text}\n\n";
            }

            storyText += "```";
            // Send text to Discord
            await channel.SendMessageAsync(storyText);

            // Display all the choices, if there are any!
            if (_story.currentChoices.Count > 0)
            {
                string choices = "";
                for (int i = 0; i < _story.currentChoices.Count; i++)
                {
                    Choice choice = _story.currentChoices[i];
                    choices += $"*{i + 1}. {choice.text.Trim()}*\n";
                }
                // Show choices
                var choiceMessage = await channel.SendMessageAsync(choices);
                for (int i = 0; i < _story.currentChoices.Count; i++)
                {
                    var emoji = EmojiManager.GetEmojiFromNumber(i + 1);
                    if (emoji != null)
                        await choiceMessage.AddReactionAsync(emoji);
                }
            }

        }



    }
}
