# osumosis
The osu! client to play on the [osmosis server](https://github.com/Tina-otoge/osmosis-server),
a server that entirely disregards combo to calculate scores.

## This is a fork of the official osu!

Codename "lazer" is the upcoming version of the free-to-play rhythm game "osu!".
This project is modification of this client which adds a few features there and
there.
=======

Read all about [osu! lazer on their own GitHub](https://github.com/ppy/osu).

## Notable changes between osumosis and osu! (lazer)
=======

:warning: osumosis does not change any behavior related to the official server,
this means you'll still connect to your osu! account, see the official
leaderboards, and the chat. The only server-related added feature is the fact
that your scores are submitted to the osmosis server when you finish a song.

Since this is not an official release, you can not submit scores to the official
server using it. This also means no multiplayer support!

- Scores are submitted to the open source server implementation at https://osmosis.tina.moe
- A mod has been added to disable the passive HP drain ("No Drain")
- Two new score calculation algorithms have been added (see "Score display mode" in settings):
  - "Accuracy" is a 7 digits representation of your accuracy, combo isn't used at all (this is osmosis' prefered scoring)
  - "Ex score" inspired by BEMANI, where perfects grant 2 points each, lower judges scale from this
- More "grades" (or "score rank") have been added:
	- A+ is awarded between an A and an S
	- S+ and S++ can be awarded between an S and an SS

### Planned / Temporarily removed features

- Being able to display FAST or SLOW alongside judges
- Being able to set a pacemaker, allow you to set goals (ie: S rank) and see how close you are to it in real-time
- Output the current state of the game to a text file, for ease of use in streaming setups

## Notable changes between osu! lazer and the current stable osu!

- lazer is open source, stable is not
- Since it's open source, it's easy to modify it, add new mods, features or even custom modes
- Inputs are polled on a separate thread
  - This means your FPS do not impact your input lag, you can run the game at 10 FPS and still get <1ms accurate hits
- Not only the entire codebase but also the UI has been revamped
- You can do most of what you can do on the website directly ingame
  - This includes downloading mapsets and checking user profiles
- lazer scoring is different than stable's scoring and stable's ScoreV2
- lazer judges the accuracy on slider heads, this means you can get a "100" or "50" on sliders
  - This can be disabled using the "Classic" mod
  - The "Classic" mod makes lazer behaves as closely as possible to stable
- You can customize the difficulty settings (AR, HP, OD, CS) of beatmaps using a mod called "Difficulty Adjust"
- You can customize the rate of Double Time and Half Time
- There are plenty of fun mods to alter gameplay
- A density graph is displayed in the bottom during songs, allowing you to be aware of hard and slow parts in advance

And many other stuffs I did not mention.

## Running osumosis

:warning: Beware! osumosis is in a gray area where it's not really allowed to be
distributed. osu! is trademarked and usage of its name, logo and assets without
permission is illegal.

osumosis and the osmosis server are projects I run as a hobby. I like osu! as a
game and project and I like having the possibility to play Ouendan-style
gameplay on my PC. However being discontent about some aspects of the game,
mainly but not only combo-based scoring, I decided to modify the game to my
liking.

By running osumosis, you must be aware that this is an unsupported version of
osu! run by someone who is not an official staff, and that the existence of this
project has never been approved by osu!.

### How to actually run it

- You need the .NET 6 SDK, for Windows and macOS, go there:  
  https://dotnet.microsoft.com/download/dotnet-core/3.1  
  And download the corresponding installer in the "Build apps - SDK" section

- [Download the source code](https://github.com/Tina-otoge/osumosis/archive/master.zip)
  and extract it somwhere.

- Run `dotnet run --project osu.Desktop -c Release` in it  
  Alternatively, run the `launch.bat` script that runs it for you

TLDR: Download .NET SDK 6 and the code of osumosis, double click on `launch.bat`, enjoy.
