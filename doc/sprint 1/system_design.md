# System Design

## Cover Page

## Table of Contents

[CRC Cards](#crc-cards)

[Software Architecture Diagram](#software-architecture-diagram)

## CRC Cards

**Class Name:** ArrowController

**Object (this script is attached to):** Arrow

**Responsibilities:**

* Reflect direction camera is facing
* Keep arrow ahead of the ball

---

**Class Name:** BallCamController

**Object:** BallCam

**Responsibilities:**

* Camera follows the ball
* Camera move based on mouse position
* Camera zoom with mouse wheel
* Hide objects in way of the camera

---

**Class Name:** CollisionTrigger

**Object:** CollisionTrigger

**Responsibilities:**

* Change collision layer of ball to fall in the hole

---

**Class Name:** HitBallBehaviour

**Object:** Ball

**Responsibilities:**

* Apply a force to the ball with left mouse click
* Adjust the power applied to the ball with the number keys (TEMPORARY)

---

**Class Name:** HoleTrigger

**Object:** BallTrigger

**Responsibilities:**

* Detect when the ball is in the hole
* Reset ball position (TEMPORARY)

---

**Class Name:** HoleTrigger

**Object:** BallTrigger

**Responsibilities:**

* Detect when the ball is in the hole
* Reset ball position (TEMPORARY)

## Software Architecture Diagram