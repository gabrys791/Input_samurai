# Input samurai
An arcade 2D platformer built with Unity designed as an Engineering Thesis Project (*"Creation of an arcade game with a non-standard control system"* at the University of Warmia and Mazury in Olsztyn).

The project explores game ergonomics, motor adaptation, and input paradigms by letting players experience and switch between 6 different historical and alternative control schemes, alongside advanced platforming mechanics and dynamic level progression.

## Playable Build

Play in Browser (WebGL): https://gabrys790.itch.io/input-samurai

## Core Game Mechanics & Systems

### 1. Dynamic Control Schemes & Ergonomics Engine
Implemented an in-game input switching architecture supporting 6 distinct control layouts, tracking player adaptation and memory across sessions:
* **WASD** (Modern industry standard)
* **ESDF** (Alternative standard with wider key-reach, popular in *Tribes 2* and early *Quake*)
* **DCAS / ASDC** (Ergonomic layout popularized by Bungie's *Marathon*)
* **SDFSpace** (Linear 3-finger layout used in early 3D shooters and fighting games)
* **5678** (Numpad navigation layout from *ZX Spectrum / Sinclair*)
* **IJKM** (8-bit directional convention popularized by *Apple II*)

### 2. Advanced 2D Character Controller
* **Core Movement:** Horizontal velocity clamping, gravity handling, and snappy jump physics.
* **Wall Mechanics:** Wall-sliding, wall-climbing with an integrated cooldown manager, and momentum-based wall jumping.
* **Dash System:** Timed dash mechanic (2-second cooldown) to clear hazardous gaps.
* **Combat & Animation:** Melee combo system (2-stage attack animation), cooldown timers, hit detection, visual hit-flashing, and synchronized audio feedback.

### 3. Enemy AI & Behaviors
* **Goblin (Ground Patrol):** Waypoint-based patrol logic switching to immediate attack behavior upon detecting the player in frontal line-of-sight.
* **Bat (Flying Pursuer):** Radial distance-based detection triggering continuous player tracking and airborne dive attacks.

### 4. Non-Linear Level Progression (Artifact Spawner)
* **Anti-Linearity Spawning Algorithm:** Semi-randomized procedural artifact spawner (`ArtefactSpawner.cs`) distributing mandatory collectibles across predetermined anchor points while preventing duplicate instantiations.
* **Exploration Rewards:** Optional secondary crystals encouraging level exploration and score tracking.

## Architecture & Technical Highlights

* **Engine:** Unity (Version 2022.3 LTS)
* **Language:** C#
* **Input Architecture:** Custom wrapper around Unity's `Input Manager` mapping dynamic axis/key bindings based on persistent user preferences (`PlayerPrefs`).
* **Game Loop & State:** Event-driven checkpoints Lantern checkpoints, health management, level transition checks, and completion timer tracking.
* **QA & Black-Box Testing:** Conducted black-box playtests with user feedback surveys measuring cognitive load, muscle memory adaptation, and layout ergonomics across difficulty scales.
* **Platform:** WebGL & Windows Desktop.
