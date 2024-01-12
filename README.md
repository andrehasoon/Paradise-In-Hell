[![Open in Visual Studio Code](https://classroom.github.com/assets/open-in-vscode-f059dc9a6f8d3a56e377f745f24479a46679e63a5d9fe6f495e02850cd0d8118.svg)](https://classroom.github.com/online_ide?assignment_repo_id=464693&assignment_repo_type=GroupAssignmentRepo)

**The University of Melbourne**

# COMP30019 – Graphics and Interaction
# 2021 Semester 2 - Unity Game Project

Team members: Ryan Campbell, André Hasoon, Mintao Hu & Abhi Patel

Final Electronic Submission (project): **4pm, November 1**

Do not forget **One member** of your group must submit a text file to the LMS (Canvas) by the due date which includes the commit ID of your final submission.

You can add a link to your Gameplay Video here but you must have already submit it by **4pm, October 17**

# Project-2 README

You must modify this `README.md` that describes your application, specifically what it does, how to use it, and how you evaluated and improved it.

Remember that _"this document"_ should be `well written` and formatted **appropriately**. This is just an example of different formating tools available for you. For help with the format you can find a guide [here](https://docs.github.com/en/github/writing-on-github).

**Get ready to complete all the tasks:**

- [x] Read the handout for Project-2 carefully.

- [x] Brief explanation of the game.

- [x] How to use it (especially the user interface aspects).

- [x] How you designed objects and entities.

- [x] How you handled the graphics pipeline and camera motion.

- [x] The procedural generation technique and/or algorithm used, including a high level description of the implementation details.

- [ ] Descriptions of how the custom shaders work (and which two should be marked).

- [x] A description of the particle system you wish to be marked and how to locate it in your Unity project.

- [ ] Description of the querying and observational methods used, including a description of the participants (how many, demographics), description of the methodology (which techniques did you use, what did you have participants do, how did you record the data), and feedback gathered.

- [ ] Document the changes made to your game based on the information collected during the evaluation.

- [ ] References and external resources that you used.

- [ ] A description of the contributions made by each member of the group.

## Table of contents

- [Team Members](#team-members)
- [Explanation of the game](#explanation-of-the-game)
- [Technologies](#technologies)
- [Using Images](#using-images)
- [Code Snipets ](#code-snippets)

## Team Members

| Name           |     Task      |    State |
| :------------- | :-----------: | -------: |
| Student Name 1 |   MainScene   |     Done |
| Student Name 2 |    Shader     |  Testing |
| Student Name 3 | README Format | Amazing! |

## Explanation of the game

Our game is a third person shooter that is based on a wizard who is living his sentence out in hell. Luckily for our wizard, he got rolled a slice of hell not as bad as he had imagined, albeit stuck by himself. Over time, he comes to terms that what he actually posses is essentially a "Paradise" in comparison and should be grateful. With this realistaion, the ruler of the under world sees that the wizard is actually maintaing his sanity, contradictory to his expectations. Raging with anger, the Devil attempts to steal his Paradise by sending waves of demons, at first underestimating the wizard's power by sending his weaker breeds of demons, however over time, the coming demons seem to be getting stronger. Surprisingly, it always seems to be raining on this little paradise and the wizard turns out to be bright enough to know exactly how to turn this into destructive projectiles. How long will the wizard survive..... Find out.... By playing the game :)

The objective of the wizard is to try and stay well alive... for as long as possible. Time is the metric that will determine the magnitude of success. Even though the demons get stronger, thou shall not worry, as they occasionally drop items which will help empower you (the player) however only to a certain degree. Items can include direct equippable items which provide big level ups in terms of damage output, or reduction in damage intake. Along with these, potions can also be found upon the death of a demon, which can give you additional health, magic, attack or defense stats. And to further cary and spice up the play style, you can find rings which tremendously help increase dexterity (shooting speed), movement speed, vitality (health regen) or wisdom (magic regen). Only 1 ring at a time can be worn, yet they constantly keep dropping through the game, therefore allowing the player to dynamically adjust their strategy to what better suites their specific style.

## How to Play

The game and the interface have been design with ituition and simplicity in mind, therefore if it needs explaining, then we have failed. Nonetheless, the game controls are as follows; Use WASD to move around, hold down left mouse button to basic attack, and finally, press space bar to use the spell and release a water bomb, which actually moves slow, however it spawns at the location of the mouse and moves radially outwards. In order to pick up and equip items, simply walk over it and tadaaaaaaaaa.... After picking up these items, they appropriately change the in game statistics which can be seen during live gameplay on the left hand side of the screen, under the Health and Magic Bars. This HUD helps the player make informed up-to-date decisions and adjust their play style accordingly to gain the edge over the enemies.

In terms of the user interface, there is the main menu screen on which typical interactable buttons such as "Start", "Options", "Tips", and "Quit", can be found, which are all self explanatory and easy to understand. Pressing the "Start" and "Quit" well do ... just that. Within the Options tab, the options available are; to play fullscreen or not, apply VSync or not, and, change the resolution of the game for the desired quality to perfomance ratio. Upon being defeated by the demons, a text will represent the player's score, which will essentially be the time survived, after which the play will have the option available to go back to the Main Menu screen and restart the game.

## Design of Objects and Entities

For this section, the non trivial objects and their properties will be briefly explored:

MainCamera - contains a script that controls the camera movement by looking at the player with a preset offset (3rd person view).

TreeSpawner - contains a script that spawns the (fir, oak and palm) trees using a procedural generation technique (later discussed in more detail) to keep the obstacle terrain unique every game.

Player - contains a PlayerController script that takes care of updating the player statistics throughout the game. This entity is contains a RigidBody component so that it can interact with the environment and it contains a collider that can react appropriately to collisions, for example when picking up items or getting hit by enemy projectiles.

EnemySpawner - contains a script that spawns the enemies periodically on the map as long as the player is alive.

LootSpawner - contains a script that keeps track of all equipable items and drops them accordingly when an enemy dies.

StatDisplayer - contains a script which pulls the stats from player to display onto the in game HUD.

Rain - has a particle system attached to it, in our case its rain, which generates rain across the entire map.

HUD - displays the character stats contained in the StatDisplayer onto the screen.

EndGameLayout - displays the end game screen when the player dies with the score and the option to go to the main menu again.

## Graphics Pipeline and Camera Motion

Someone needs to talk about the graphics pipeline

The camera essentially follows the player movement with an offest which has been predetermined such that the correct portion of the map is exposed at a given time (decided by the team).

## Procedural Generation

In order to make the game more visually appealing every time the game is reloaded, the properties associated with the trees were also psedo-randomally generated. The map is split into 4 quadrants, each quadrant will have a set number of lone standing trees and a set number of tree clusters. Through random random generation, the positions of the lone standing trees and the centres of the tree clusters were generated. The tree clusters would also have a minimum and maximum values associated with the number of trees that cluster can have. Hence, the amount of trees spawned within a cluster would vary randomly within this range and the trees of the clusters would be spawned around the centres randomly as well, however they will be spawned within a set radius of the centre.

There are 3 tree types; Fir, Oak and Palm, with Palm trees typically being the talest in nature. This was pseudo coded such that the single palm trees that spawn are scaled in size by 1.5x, whereas the Fir and Oak trees would remain unscalled. In terms of the cluster spawning, a bigger tree was always spawned in the centre of the cluster surrounded by smaller trees of the same type. The size of the mid tree was pseudo randomly generated ranging between values [1.1x, 1.3x] such that the mid tree is always bigger than the surrounding trees however, not too big such that it dwarves everything else. Similarily, the sorrounding trees of the cluster were scalled by [0.75x, 1.0x] such that they are always smaller than the mid tree by a small amount yet not abosolutely tiny in comparison.

Along with the scale being pseudo randomly generated, the rotation of the trees within the cluster were also randomly generated so that they look unique oreientation wise.

However, since this does not take into account that the player can spawn such that they end up being trapped, a functionality was added to make sure that within a square of a certain size with the centre at (x=0,z=0), no trees would spawn within this region. Although theoretically it is still possible to have a generation in which the player is completely surrounded by trees upon spawning, the likelihood of it occurring would be incredibly low.

Using this technique, we were able to achieve tree spawning which would change everytime and would seem natural and appealing, adding a new layer of experience to the gameplay which could possibly challenge or assist the player in their quest of survival.

## Shaders & Particle Systems

**Shader 1**

The enemy projectile game objects were given a 'fireball' shader as fire is often associated with evil. This shader is called Shader1 and is located in the Materials folder within the Assets folder. The shader incorporates the Phong shader code given in the workshops. This allows the projectiles to be lit more realistically and the shininess present in the Phong lighting model gives the sense that the projectiles are very powerful. The Phong effect is as usual implemented in the pixel shader. The other effect present in this shader is the continuous shift in color between yellow, orange and red, giving the impression of a crackling fire surrounding each projectile. This effect is achieved by using the current game time inside a sine function, and altering the input color in the pixel shader. This effect could also have been implemented in the vertex shader by altering the output color. The advantage of using a shader for this effect is that shaders use a computers GPU, which can operate on multiple data streams continuously. So, in the pixel shader, a good GPU may process as many as 24 pixels at the same time, where an equivalent CPU based approach would only be able to process one at a time.

**Shader 2**
TODO

**Particle System**

A rain particle system was implemented to enhance the game's aesthetic. As the player fights against terrifying monsters, it seems fitting that the environment be somewhat wet, dark and unforgiving. In the Scenes folder, the MapGeneration unity file is the finished game. The rain is implemented in this scene using the Unity particle system object. The rain particles are stochastically generated in a defined region above the game environment. They are affected by a simulated gravity force, causing the particles to accelerate toward the ground. The particles are not spherical, rather rectangular prisms, stretched in the y-axis. This is to simulate how the 'tail' of a raindrop lags behind the main volume of water. As the particles collide with the game's ground object, they become extinct.

**Querying method**

A custom made questionnaire was given to 6 participants.
Evaluation method

**Changes made**

**References**

**Contributions**

## Technologies

Project is created with:

- Unity 2021.1.13f1
- Ipsum version: 2.33
- Ament library version: 999

## Using Images

You can use images/gif by adding them to a folder in your repo:

<p align="center">
  <img src="Gifs/Q1-1.gif"  width="300" >
</p>

To create a gif from a video you can follow this [link](https://ezgif.com/video-to-gif/ezgif-6-55f4b3b086d4.mov).

## Code Snippets

You can include a code snippet here, but make sure to explain it!
Do not just copy all your code, only explain the important parts.

```c#
public class firstPersonController : MonoBehaviour
{
    //This function run once when Unity is in Play
     void Start ()
    {
      standMotion();
    }
}
```
