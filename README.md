# InkyBot-Sharp


This project creates a bot for Discord that allows to play scripts that made in Ink (https://github.com/inkle/ink)

At the moment fuctionality of a bot is purely basic: it can play a generated story "intercept.json" in Discord, allowing you to make a choice with emoji reactions.

Steps that you should make:
1. Create a bot for Discord (if you really have wanted to create a bot, you're on this step already)
2. Use "config.dist.json" in root folder and rename it to "config.json". In config you can change the prefix, if you doesn't want to shout (!) in your messages. 
But most importantly - in "token" field goes you Discord bot token, nothing will work without it.
3. Open up a project and make sure that all packages through nuget are there.
4. Build/play.
5. Go to your Discord bot and type "!start" (if you haven't changed the prefix). If you didn't change the story in Ink folder, it should play Intercept.
6. Generate another stories with the help of Ink, maybe do something much cooler and useful that I haven't made (basically, everything)