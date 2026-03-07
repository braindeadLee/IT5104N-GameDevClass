# WEEK 1

## Instructions:

For today, Jan 31, Week 1 let's do the following: 

title of activity : Simple Scene with a moving node
1) create a new godot project. Try to create a simple 2D or 3D project showing "hello world" text.
2) upload to github, update readme.md and put your screenshots and some details of the activity.
3) email me the link to your github project (mention your schedule please)

## Work

Achieved using TextMeshProGUI as the text, and simple Square objects with white as the sprite color for the background.

![Level Text](Pictures/week1.png)

# WEEK 2.1

## Instructions 

Week 2 : Activity 1
Gameplay Mechanics
Subtopics: Handling input (keyboard/gamepad), physics bodies (rigid/kinematic), collision detection. Basics of player controllers (movement, jumping).
Exercises: Build a dodge mechanic or simple platformer character; test with physics tweaks.'

## Work

Movement was achieved using Unity's Input System + a custom PlayerMovement.cs script

![Level Text](Pictures/week2-1-1.png)
![Level Text](Pictures/week2-1-2.png)

Unity's built in collision system was applied by using the Collision2D & RigidBody2D into the player GameObject. It allows it to give physics to player movement and interaction with the floor (not falling through).

![Level Text](Pictures/week2-1-3.png)

# WEEK 2.2

## Instructions

Week 2 : Activity 2 Level Design (add this in readme.md)
Tilemaps for grid-based levels, adding hazards (spikes/traps), designing flow (pacing, difficulty curves). 
Activity : Design of 2 levels for an endless runner (2D or 3D); Level 1 should be noticeable easier than level 2. Implement traps. No HP, once caught in trap restart from the beginning of the level. There should be a notification when entering level 2.

# Work 

A simple "runner game" with Geometry Dash like gameplay was made. Spikes generate from the right off-screen and come in waves that the player has to avoid by jumping. 

![alt text](Pictures/week2-2-1.png)