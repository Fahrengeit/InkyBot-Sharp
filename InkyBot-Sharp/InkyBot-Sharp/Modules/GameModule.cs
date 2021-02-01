using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace InkyBotSharp.Modules
{
    public class GameModule : ModuleBase<SocketCommandContext>
    {
        private readonly StoryManager _storyManager;

        public GameModule(StoryManager storyManager)
        {
            _storyManager = storyManager;
        }


        [Command("start")]
        public async Task StartGame(string path = "")
        {
            await _storyManager.StartStory(path, Context.Channel);
        }

    }
}
