# Product Backlog

## User Stories

> We used the [MoSCoW](https://dzone.com/articles/6-backlog-prioritization) system to assign priority for each user story.
> M = MUST have this; S = SHOULD have this if at all possible; C = COULD have this if it does not effect anything else.
> W = WON'T have this time but would like in the future.
> 
> We used this [Planning Poker](https://www.pointingpoker.com/) tool to estimate our user story sizes.
>
> **UPDATE:** User stories have been fleshed out and made independent in our meetings during sprint 1, new ones have been added, and points re-evaluated/converted based on a Pivotal Tracker system.

---

1. As a player, I want a simple play area to hit the ball around, so that I can test and get used to the game.
- Given that a player runs the game
- When they start it
- They should have a simple play area to hit the ball around in
- Priority: M
- Size: 0
2. As a player, I want a third-person camera that I can rotate around my ball, so that I can aim my ball accurately towards any point on the XZ plane. Additionally, I want the camera to follow my ball around while it is in motion.
- Given that a player is playing the game
- When they move the mouse or the ball moves
- The camera rotates around the ball/stays on it
- Priority: M
- Size: 2
3. As a player, I want to be able to zoom the camera in and out in order to get a better view of the stage.
- Given that a player is playing the game
- When they are using the third-person camera around the ball and scrolling up/down on the mouse
- The camera zooms in/out
- Priority: S
- Size: 0
4. As a player, I want to be able to hit a ball, so that I can punt it around a play area.
- Given that a player is playing the game
- When they click the mouse
- The ball moves forward (relative to the camera)
- Priority: M
- Size: 1
5. As a player, I want a visual indicator in 3rd-person view for seeing which direction my shot will go, so that I have a better picture of the result and can play better.
- Given that a player is playing the game
- When they are using the third-person camera
- An arrow projects the direction the ball would be shot towards
- Priority: M
- Size: 1
6. As a player, I would like a simple bird's eye view of the playing field so I know where I currently am, as well as where my goal is.
- Given that a player is playing the game
- When the player is looking for a bigger perspective on their position
- They can look at at a mini-map displaying a bird's eye view of the field
- Priority: S
- Size: 1
7. As a player I want a hole to hit my ball into so that I can score points.
- Given that a player is playing the game
- When the player wants to score/progress to a victory
- They can attempt to hit their ball into a hole
- Priority: M
- Size: 2
8. As a gamer, I want my ball to respawn when I score it into a hole.
- Given that a player is playing the game
- When they hit a ball into a hole
- Their ball should respawn somewhere depending on the game settings
- Priority: M
- Size: 1
9. As a player, I want to be able to see through walls, so I have better vision of of the play area from awkward angles.
- Given that a player is playing the game
- When an object is in-between their ball and their camera point-of-view
- The object is temporarily hidden/transparent
- Priority: S
- Size: 1
10. As a casual gamer, I want a clear and concise HUD, so that I can know the state of the game and what I should be doing at all times.
- Given that the game has states
- When a user is playing the game
- They should be able to discern the state of the game and make plans based on the conditions
- Priority: S
- Size: 1
11. As a social person, I want to chat in-game, so that I can bond and build relationships while playing with my friends.
- Given that a game is running and the players are a group of friends
- When a player wants to communicate
- Then they should have the option to communicate via text
- Priority: M
- Size: 1
12. As a competitive gamer, I want to be able to play against other players so that I can fulfill my competitive urges.
- Given that Jimmy and someone else (ie. a friend) have the game
- When they want to play together
- They should be able to face each other in a competitive match
- Priority: S
- Size: 5
13. As an experienced player, I want to be able to adjust my third-person look sensitivity as well as adjust the default height the camera is relative to the ball so that I can control the camera to my personal preference.
- Given that a player wants to tweak their camera sensitivity
- When they open the options menu
- They should be able to change their sensitivity
- Priority: C
- Size: 1
14. As a player I should have a variety of terrains to navigate  so that I may have complexity in the design of levels
- Given that a player is playing the game
- When they play on a level
- There are interesting details and varying sets of terrain present
- Priority: S
- Size: 3
15. As a player, I want to be able to control the power of my hits so that I have more control over where my ball ends up.
- Given the ball, when I hit it I should be able to control the power I hit it with
- Add a UI element that shows me the power I will hit the ball with.
- Priority: M
- Size: 3
16. As a player I want to be able to host and join multiplayer matches so that I can shoot a ball in the same play area as other players.
- Convert the sprint 1 code-base to be multi-player capable.
- Set-up network/game manager to handle all future feature implementations.
- Priority: M
- Size: 5
17. As a user, I would like to pause my current game from the game menu so that I can temporarily stop gameplay and enter the game menu to access options/actions.
- Add function to stop gameplay
- Add UI element to trigger function to stop gameplay/enter menu
- Priority: S
- Size: 1
18. As a user, I would like to unpause my current game from the game menu so that I can resume gameplay and exit the game menu.
- Add function to resume gameplay
- Add UI element to trigger function to resume gameplay/exit menu
- Priority: S
- Size: 1
19. As a player, I would like to be able to reset my current game from the game menu so that I can start the current game anew.
- Add function to reset current gameplay
- Add UI element to trigger function to reset current gameplay
- Priority: S
- Size: 1
20. As a player, I want to be able go back to the main game menu so I can perform actions from the main game menu.
- Add main menu options (Start Game, Multiplayer, Options, Exit, etc)
- Make main menu the starting scene
- Priority: S
- Size: 1
21. As an experienced player, I want there to be winds that can affect the ball while it is mid-air so that the game is more challenging.
- Add wind gameplay mechanics
- Add a UI element that shows the direction and speed of the wind for the current shot
- Priority: S
- Size: 1
22. As a non-gamer, I want to be able to start playing the game in an easy level in order to adjust to the game mechanics.
- Create Level 1 Layout
- Create simple obstacles
- Add clouds to show wind direction in environment
- End goal added
- Priority: M
- Size: 1
23. As a player, I want the to choose a set of maps and play through the maps in that set, so that i don't need to keep choosing maps after each game.
- Transition from one stage to another
- Have a set of maps to play
- A notification that all maps in the set have been played through
- Priority: S
- Size: 1
24. As a minigolf fan I expect to see a turning windmill, so I have an obstacle that I must time my shot to get by
- Create the propeller object
- Script object to turn
- Priority: C
- Size: 1
25. As a player of games I want to have collectivles throughout the stage, so that I have a reward for going elaborate paths
- Create Coin model
- Script collider to coins so that balls may collect coins
- Priority: C
- Size: 2
26. As a golf player, I want there to be able to traverse the course vertically, so that I can experience an extra dimension beyond regular golf
- Bounce pad model
- Bounce pad scripting
- Priority: C
- Size: 1
27. As an experienced player, I want there to be speed pads so that when the ball touches it, the ball recieve a large boost of speed
- Create speed pad prefab
- Assign collider script that adds speed to entering balls
- Priority: C
- Size: 1
28. As a player, I would like to be able to set the sensitivity of the game camera's movement so that I can customize the rate I pan the camera during gameplay. 
- Add UI element to trigger change to game camera's movement sensitivity
- Priority: C
- Size: 0
29. As a player, I would like to be able to set the sensitivity of the game camera's zoom so that I can customize the rate I zoom during gameplay. 
- Add UI element to trigger change to game camera's zoom sensitivity
- Priority: C
- Size: 0
