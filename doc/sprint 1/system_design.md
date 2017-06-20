# Golf Hero: System Design

Our software is being designed in Unity for Windows machines.
It is programmed in C# and organized using the MVC model. For our first sprint, we are only focusing
on the View and Control aspects of the design as shown below in our Software Architecture Diagram. The CRC
Cards below also show the class names and their responsibilities.


# Table of Contents

1. [CRC Cards](#crc-cards)

1. [Software Architecture Diagram](#software-architecture-diagram)


# CRC Cards

**Class Name:** ArrowController

**Responsibilities:**

* Reflect direction camera is facing
* Keep arrow ahead of the ball

---

**Class Name:** BallCamController

**Responsibilities:**

* Camera follows the ball
* Camera move based on mouse position
* Camera zoom with mouse wheel
* Hide objects in way of the camera

---

**Class Name:** CollisionTrigger

**Responsibilities:**

* Change collision layer of ball to fall in the hole

---

**Class Name:** HitBallBehaviour

**Responsibilities:**

* Apply a force to the ball with left mouse click
* Adjust the power applied to the ball with the number keys (TEMPORARY)
* Hide arrow while in motion
* Only allow shooting when stopped

---

**Class Name:** HoleTrigger

**Responsibilities:**

* Detect when the ball is in the hole
* Reset ball position (TEMPORARY)

---

**Class Name:** MinimapIconBehaviour

**Responsibilities:**

* Keep the 2D minimap in place


# Software Architecture Diagram

## Expected system(s)
* Our project is expected to run within a desktop environment (e.g. Windows), where the user is able to provide input using their keyboard and mouse and output is able to be sent and displayed on their screen(s) attached to their computer.


## Software Architecture Diagram
[![Software Architecture Diagram](http://i.imgur.com/XOgFYjw.png)](http://i.imgur.com/XOgFYjw.png)
* Using Unity, our project's architecture is made up of mostly components.
* The view (which the player sees) is the current scene (menu, level, etc.) that they are on.
* The model (which 'model' items found in golf - thank you Professor Dema for the clarification) are the game objects/prefabs. This includes (and is not limited to) the cameras objects, the player object, as well as the objects making up the level.
* The controller are the scripts which are part of the game objects within the model.

## Dealing with errors & exceptions
* Unity's game engine will handle most errors/exceptions which occur during the run-time of the game. 
* For user errors (e.g. User providing a display name which contains invalid characters), an in-game dialogue will appear informing the player of the error and how they should fix it.
