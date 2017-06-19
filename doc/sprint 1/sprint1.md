# Sprint 1 Meeting

## Participants:
(1) John Fraser
(2) Swarnajyoti Datta
(3) Karen Ngo
(4) Bryan Delgado-Bosso
(5) Michael Chiu
(6) Zixing Gong
(7) Eugenia (Yujia) Zhang

##Task Assignment Legend:
- Task 1 [1, 2, 3] means that John (1), Swarnajyoti (2), and Karen (3) are assigned to Task 1
- Story 1 [4] means that Bryan (4) is assigned to all the tasks in Story 1
- Story 2 [5, 6, 7] means that Micheal (5), Zixing (6), and Eugenia (7) are assigned to some tasks in Story 2

## Sprint 1 Backlog, Task Breakdown, and Task Assignment
- As a player, I want a simple play area to hit the ball around, so that I can test and get used to the game. [2]
-- Create a plane. 
-- Create walls to prevent balls from falling off.
-- Colour some grass/other material onto it.
- As a player, I want a third-person camera that I can rotate around my ball, so that I can aim my ball accurately towards any point on the XZ plane. Additionally, I want the camera to follow my ball around while it is in motion. [2]
-- Fix camera to third person perspective off-set from the ball  even while in motion.
-- Allow for rotation around the sphere of the ball with the mouse, keeping it pointed at the ball at all times.
- As a player, I want to be able to hit a ball, so that I can punt it around a play area. [6]
-- Allow a ball to struck in the direction the camera is facing.
-- Disallow the ball to be struck while it is in motion.
-- Allow it to be struck again when it is still.
- As a player I want a hole to hit my ball into so that I can score points. [1, 4]
-- Create a hole on the ground such that the ball will fall into it. [1]
-- Have a zone that triggers the end of hole function. [1]
-- When all balls have completed, unload and load next scene. [4]
- As a gamer, I want my ball to respawn when I score it into a hole.
-- Reset the ball to it's starting location after it falls into a hole.

*Mid Sprint Note: Additional user stories that were not part of the planned sprint 1 backlog were completed due to Unity being more powerful and easier to work with than anticipated by the team, they are listed below

- As a player, I want to be able to zoom the camera in and out in order to get a better view of the stage. [2]
-- Given the camera focused on the ball, when I scroll the mouse wheel, I want the camera to zoom in or out
- As a player, I want a visual indicator in 3rd-person view  for seeing which direction my shot will go, so that I have a better picture of the result and can play better. [4]
-- Add a visual indicator (arrow, line, etc) based on the position of the camera and the ball.
-- Arrow should disappear while the ball is in motion
- As a player, I would like a simple bird's eye view of the playing field so I know where I currently am, as well as where my goal is. [7]
-- Set up camera for minimap
-- Add icons/symbols to minimap representing the player
-- Add icons/symbols to minimap representing turf
-- Add icons/symbols to minimap representing the goal
- As a player, I want to be able to see the ball even when a wall is obscuring my view of the field. [2, 4]
-- Given an in-game match, when a wall is obscuring my view of the ball, the wall should be hidden in order to make the proper shot. [2, 4]