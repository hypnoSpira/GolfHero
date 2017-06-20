# Golf Hero: System Design

Our software is being designed in Unity for Windows machines.
It is programmed in C# and organized using the MVC model. For our first sprint, we are only focusing
on the View and Control aspects of the design as shown below in our Software Architecture Diagram. The CRC
Cards below also show the class names, the objects they are attached to, and their responsibilities.

---
---
---

# Table of Contents

1. [CRC Cards](#crc-cards)
1. [Software Architecture Diagram](#software-architecture-diagram)

---
---
---

# CRC Cards

**Class Name:** ArrowController

**Object (this script is attached to):** Arrow

**Responsibilities:**

* Reflect direction camera is facing
* Keep arrow ahead of the ball

---

**Class Name:** BallCamController

**Object:** Camera

**Responsibilities:**

* Camera follows the ball
* Camera move based on mouse position
* Camera zoom with mouse wheel
* Hide objects in way of the camera

---

**Class Name:** CollisionTrigger

**Object:** Collision Trigger

**Responsibilities:**

* Change collision layer of ball to fall in the hole

---

**Class Name:** HitBallBehaviour

**Object:** Ball

**Responsibilities:**

* Apply a force to the ball with left mouse click
* Adjust the power applied to the ball with the number keys (TEMPORARY)
* Hide arrow while in motion
* Only allow shooting when stopped

---

**Class Name:** HoleTrigger

**Object:** Bottom Trigger

**Responsibilities:**

* Detect when the ball is in the hole
* Reset ball position (TEMPORARY)

---

**Class Name:** MinimapIconBehaviour

**Object:** Ball Icon

**Responsibilities:**

* Keep the 2D minimap in place

---
---
---

# Software Architecture Diagram

## Expected system(s)
Our project is expected to run within a desktop environment (e.g. Windows), where the user is able to provide input using their keyboard and mouse and output is able to be sent and displayed on their screen(s) attached to their computer.


## Software Architecture Diagram
[![Software Architecture Diagram](http://i.imgur.com/N8ig6mY.png)](http://i.imgur.com/N8ig6mY.png)

## Dealing with errors & exceptions
TBA



