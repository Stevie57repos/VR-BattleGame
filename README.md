# VR Card Battle Game
VR Single Player Battle Game - Gameplay Youtube Trailer

**Click on the image below to watch a YouTube gameplay Trailer**

[![Header Image](https://github.com/stevie57/VR-BattleGame/blob/main/Assets/Textures/Screen%20shot.png)](https://www.youtube.com/watch?v=IIg7JPc2Cis&t=15s)

## VR Game Description
Battle in VR using magical cards that grant temporary weapons and grow stronger through defeating enemies and collecting loot. Enemies' behaviors are flexible and programmed based on either a state-machine or node based behavior tree. 

I made a video going over the core game systems in this prototype along with a write up which you can check out on my blog if you are interested at http://www.stevievu.com/2021/07/vr-card-battle-game-july-15th.html .

## Overview
The game play loop is very simple and is a cycle of defeating your enemies to get rewards which allow you to build a stronger card deck and beat more enemies. Victory is achieved when the enemies health is reduced to zero. You accomplish this by using magical weapons which spawn from a selection of magical cards. Defeated enemies provide several magical cards which you can incorporate into your deck for the next enemy. 

## Card System
Decks are based on a list of Card Scriptable Object which contain the basic information for the card. The deck gets loaded at the beginning of a match for the player. When you draw your hand a generic Card is created which is then passed the Card Scriptable object which will determine the look and effects of the card.

## Scriptable Objects & Events
One of my focus for this project was to implement SOLID design principles as well as to try and avoid creating a massive singleton. This led me to learning more about how to use scriptable objects as a bridge and the power of combining events with scriptable objects which is incredibly useful. 

## Enemy AI
To challenge myself I decided to try and implement two enemies which use either a finite state machine or a simple behavior tree. The enemy checks if it needs to change state based on what card the player currently has selected. Each card selection state will lead to attack state where you can plug in what you want to occur. You just add the corresponding attack scriptable object which would be how many projectiles and which spawn location you want it to occur from.

The second enemy uses a behavior tree that relies on scriptable objects which fall into three types of nodes which are the Selector, Sequence and Tasks nodes. It has a default idle behavior which is to look at the player and wait for card selection. When the player has selected a card the behavior tree runs the corresponding attack sequence. 

## Check out my other VR Projects
* **VR Food Truck Game** : https://github.com/stevie57/VR-Cooking-Training-Prototype
* **My VR Web Portfolio and Blog** : http://www.stevievu.com/p/about-me_31.html

