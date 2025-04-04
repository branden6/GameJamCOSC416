# Retro Game Jam GDD Template

## Game Title:
### Donkey Kong

## Team Name:
Team Monkey Business

## Team Members:
List all members and their student IDs:
- Branden Kennedy (42474551)
- Dylan Alexander (16394025)
- Necmi Kaan Sapoglu (17014796)
- Fahad Aljahlan (26955369)

## Core Concept
Our game is inspired by Donkey Kong, with the added twist that upon death, the character’s body will remain where they died last to be used as an additional mechanism to pass obstacles and solve puzzles. This will be limited to specific amounts depending on the level’s complexity so that the map is not overwhelmed or the game isn’t trivialized.

## Core Gameplay

**Game Loop:**  
Describe the basic actions the player takes and how they interact with the game.  
The player must move left, right, up, and down to avoid enemy sprites and barrels thrown by Donkey Kong. The player must collect items (hammers, his own body’s powerups) to power up their character. These powerups will be used to either make the enemies vulnerable to being hit or to help complete the level. Bodies will also be able to use the powerups in different contexts. There are also items around the map that grant the player points. The game's objective is to save the princess (original game it was Pauline) from Donkey Kong.

There will be sound, screen shake, and contrasting colours to give feedback. When enemies are vulnerable, they will flash a different colour. Once they are hit, there will be a 0.5-second pause on the screen while the enemy sprite dies. When the player is hit (either by a sprite or an obstacle), there will be a 0.5-second pause on the screen then the character will rotate 180 degrees on the y-axis and fall off of the screen. The player leaves behind an angel version of itself at the location where it died, which can be used on respawning. The angel version performs different functions, according to the powerup that the player had equipped before they died.

## Player Controls
List the controls clearly and simply.

**Keyboard Inputs:**
- **Move:** Arrow Keys/WASD
- **Jump:** Spacebar
- **Activate Body’s Mechanism/Ability:** Shift
- **Use Powerup Ability:** E
- **Pause Button:** Tab

## Level & Progression
With each level, new obstacles will be introduced. The game will have 4 handcrafted levels, all of which must be completed for the game to be won. As the game progresses, the frequency of obstacles will increase as well as their speed, gradually increasing the difficulty. Passing a level replenishes player lives.

**Scoring & Win/Loss Conditions:**  
The player earns points based on how fast they complete the level ahead of the time allotted. The player can also gain additional points for picking up items and completing the game with extra lives. The game will be over if the player runs out of time or loses all of their lives. The player must progress through the levels, reach, and complete the final level of the game to win. Losing singular lives will reset the player to the start of the current level while losing all lives sets the player back to the beginning of the game.

## Timeline & Milestones

### Week 1: Core Mechanics & Gameplay Elements
- Set up a basic Unity project <Branden>
- Implement player input and core mechanics (moving, jumping, climbing) <Fahad>
- Design and create 4 levels <Branden>
- Enemy tracking (“AI”) and movement <Dylan>
- Implement timer <Kaan>
- Implement scoring system <Fahad>
- Implement power-ups <Dylan>
- Collision handling (between ladders, barrels, player, etc.) <Kaan>
- Implement post-death player mechanics. <Branden>
- Create a rough UI (score display, lives, timer) <Fahad>
- Create Models and Art for the Game <Necmi Kaan Sapoglu>
- Find/Create Sound Effects and Music <Dylan Alexander>
- Basic animations (barrel roll, player/sprites/Donkey Kong movement) <Kaan>

====

### Week 2: Polish & Finalization
- Polish levels <Fahad>
- Camera adjustments <Kaan>
- Difficulty balancing <Branden>
- Add sound effects, screen shake, and other polish <Dylan>
- Improve UI and menus <Fahad>
- Make a short intro text screen <Branden>
- Package the final build <Kaan>

## Assets

### Models & Art:
Provide links or sources for sprites, models, and animations.
- Custom-made sprites for the main character
- [Sunny Land](https://assetstore.unity.com/packages/2d/characters/sunny-land-103349)
- [CC0 CC BY Asset](https://itch.io/queue/c/2865858/cc0-cc-by-asset?game_id=1019131)
- Other models and sprites may be used from the Unity Asset Store

### Sound & Music:
Provide links or sources for music and sound effects.
- Sounds & music will be custom-made
- [Free Casual Game SFX Pack](https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116)
- Other music and sound effects may be used from the Unity Asset Store

### UI:
- UI elements will be custom-made
- [Dark Theme UI](https://assetstore.unity.com/packages/2d/gui/dark-theme-ui-199010)
- Other UI elements may be used from the Unity Asset Store

## Feedback which was also implemented:
- Restriction on the number of clones
- Change the lie to numbers to easily know how many lives are left
- The player should die when they're trying to get out of the level

## Contributions by Team Member
## Fahad Aljahlan: [YouTube Video](https://www.youtube.com/watch?v=0B1zE8kY5S8)
- Pull Request # 1: [#31](https://github.com/branden6/GameJamCOSC416/pull/31)
- Pull Request # 2: https://github.com/branden6/GameJamCOSC416/pull/32
- Pull Request # 3: https://github.com/branden6/GameJamCOSC416/pull/51
- Pull Request # 4: https://github.com/branden6/GameJamCOSC416/pull/52
- Pull Request # 5: https://github.com/branden6/GameJamCOSC416/pull/69

## Dylan Alexander:
## Feature 1: Created Levels 1, 2, 3 and 4
### Video: [Levels demo](https://www.youtube.com/watch?v=u8rqQqziJuw)
- Pull Request #1: https://github.com/branden6/GameJamCOSC416/pull/25
- Pull Request #2: https://github.com/branden6/GameJamCOSC416/pull/36
## Feature 2: Implemented all SFX and Music, including composing an original theme for the game
### Video: [Audio demo](https://www.youtube.com/watch?v=3KH5iJZF2SY)
- Pull Request: https://github.com/branden6/GameJamCOSC416/pull/77
## Feature 3: Designed the Main Menu and How To Play Canvas
### Video: [Menu demo](https://www.youtube.com/watch?v=Jq4Mri-PWjg)
- Pull Request: https://github.com/branden6/GameJamCOSC416/pull/49

## Necmi Kaan Sapoglu:
## Feature 1: Created all Pixel Art Graphics for the Game including all sprites, and Implemented all Animations to Character Actions
### Video: [Art and Animation Showcase](https://youtu.be/Oj59v-A99_U)
- Pull Request #1: https://github.com/branden6/GameJamCOSC416/pull/42
- Pull Request #2: https://github.com/branden6/GameJamCOSC416/pull/36
- Pull Request #3: https://github.com/branden6/GameJamCOSC416/pull/53
- Pull Request #4: https://github.com/branden6/GameJamCOSC416/pull/56
- Pull Request #5: https://github.com/branden6/GameJamCOSC416/pull/66
## Feature 2: Created the 2 original clones to summon, and coded all of the clones abilities
### Video: [Clone Functionality](https://youtu.be/enkXArmHpDw)
- Pull Request #1: https://github.com/branden6/GameJamCOSC416/pull/35
- Pull Request #2: https://github.com/branden6/GameJamCOSC416/pull/39
- Pull Request #3: https://github.com/branden6/GameJamCOSC416/pull/45
- Pull Request #4: https://github.com/branden6/GameJamCOSC416/pull/53
- Pull Request #5: https://github.com/branden6/GameJamCOSC416/pull/61
## Feature 3: Created the introductory cutscene
### Video: [Intro Cutscene Showcase](https://youtu.be/X2wuYaoqshE)
- Pull Request: https://github.com/branden6/GameJamCOSC416/pull/64
## Feature 4: Created the HUD for the Game
### Video: [HUD Walkthrough](https://youtu.be/r70oOsli5sQ)
- Pull Request: https://github.com/branden6/GameJamCOSC416/pull/26


## Branden Kennedy:
## Feature 1: Created core gameplay mechanics with player movement, barrel logic, barrel spawner, ladder climbing
### Video: https://youtu.be/jXqEm8UG9cw
- Pull Request #1: https://github.com/branden6/GameJamCOSC416/pull/27
- Pull Request #2: https://github.com/branden6/GameJamCOSC416/pull/59
## Feature 2: Implemented all levels with prefabs, polished levels and player movement to go along with the skeleton of the levels setup.
Also, I polished all levels and their colours, backgrounds, ladders, movement, scripts, and canvasses so that the game looked, felt, and played better.
### Video: https://youtu.be/YHDv_0CtY1o
- Pull Request #1: https://github.com/branden6/GameJamCOSC416/pull/54
- Pull Request #2: https://github.com/branden6/GameJamCOSC416/pull/59
- Pull Request #3: https://github.com/branden6/GameJamCOSC416/pull/62
- Pull Request #4: https://github.com/branden6/GameJamCOSC416/pull/67
- Pull Request #5: https://github.com/branden6/GameJamCOSC416/pull/68
- Pull Request #6: https://github.com/branden6/GameJamCOSC416/pull/71
- Pull Request #7: https://github.com/branden6/GameJamCOSC416/pull/72


## Link to play the game
- [itch.io](https://fahadmaj.itch.io/donkey-kong-beyond-the-barrels)
