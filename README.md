Version: 0.1.0

# Unity 6 Top-Down Game Jam Starter Kit

A lightweight 2D top-down starter kit for Unity 6, designed to help game jam projects get started faster.

The kit includes a ready-made scene flow, player controller, UI states, audio, settings, pickups, hazards, enemies, timer, scoring, and common gameplay systems.


## Unity Version

Built with Unity 6.4.

## Development Note

This starter kit was developed with assistance from generative AI for planning, code iteration, debugging, and documentation. The project was manually implemented, tested, organized, and published in Unity.

Recommended template:

- Universal 2D


## Starting

Open `Assets/_TopDownStarter/Scenes/00_Boot.unity` and press Play to start the template. The normal startup scene is `00_Boot`. The `02_Game` scene can also be played directly because it includes a Bootstrapper.


## Features

- Boot scene and persistent core managers
- Direct scene testing with Bootstrapper
- Main menu
- Settings menu
- Credits popup
- Pause menu
- Game Over screen
- Victory screen
- Scene loading and scene transition
- New Input System top-down player movement
- Smooth camera follow
- Health system
- Health display
- Score system
- Score display
- Countdown timer
- Timer display
- Score pickups
- Health pickups
- Damage zones
- Collect-all objective
- Simple chaser enemy
- Audio manager
- Scene music
- SFX and UI sound support
- Saved audio volume settings


## Scene Overview

### `00_Boot`

Initial startup scene.

Creates the persistent `Core` object, which contains core systems such as:

- GameManager
- SceneLoader
- AudioManager
- SceneTransition

In a normal build, the game should start from this scene.


### `01_MainMenu`

Main menu scene.

Includes:

- Start Game button
- Settings menu
- Credits popup
- Quit button
- Menu music

<img width="1200" height="860" alt="ghmenu" src="https://github.com/user-attachments/assets/bbf25058-f78a-43f9-b653-a1d52c6fca19" />



### `02_Game`

Example gameplay scene.

Includes:

- Player
- Camera follow
- HUD
- Pause menu
- Game Over UI
- Victory UI
- Pickups
- Hazards
- Enemy
- Timer
- Objective manager

This scene can also be tested directly thanks to the Bootstrapper.

<img width="1181" height="662" alt="hero-gameplay" src="https://github.com/user-attachments/assets/2d4e37a6-f6b4-4670-806c-182680df02aa" />



## Folder Structure

* Assets/
*   _TopDownStarter/
*     Art/
*     Audio/
*     Materials/
*     Prefabs/
*     Scenes/
*     Scripts/
*     ScriptableObjects/
*     Settings/
	

## Controls

Keyboard:

* Move: WASD / Arrow Keys
* Pause: Escape / P

Gamepad:

* Move: Left Stick
* Pause: Start

Controls are defined in:

Assets/_TopDownStarter/Settings/TopDownControls.inputactions



## How to Start a New Game

1. Duplicate the `02_Game` scene.
2. Rename it for your project or level.
3. Replace the placeholder sprites.
4. Adjust player speed, health, pickups, hazards, and enemies.
5. Update scene music through the `SceneMusic` object.
6. Update credits with any third-party assets used.
7. Add the new scene to Build Profiles if needed.


## Common Prefabs

### Player

Located in:

Assets/_TopDownStarter/Prefabs/Player/


Includes:

* Rigidbody2D
* Collider2D
* TopDownPlayerController
* Health
* PlayerDeathHandler
* PauseInput

### CoinPickup

Adds score when collected.

### HealthPickup

Restores player health when collected.

### DamageZone

Damages the player on contact. Can be configured to damage once or repeatedly.

### ChaserEnemy

Moves toward the player and damages the player on contact.


## Game States

The starter kit uses these game states:

Booting
MainMenu
Playing
Paused
GameOver
Victory

`GameManager` exposes an `OnGameStateChanged` event so UI and gameplay systems can react when the state changes.

<img width="1200" height="653" alt="ghpause" src="https://github.com/user-attachments/assets/08ef4847-1de2-464d-bd95-6dd92f246251" />


## Audio

Audio is handled by `AudioManager`.

It supports:

* Music
* SFX
* UI sounds
* Master volume
* Music volume
* SFX volume
* UI volume

Audio settings are saved with `PlayerPrefs`.

Scene music is assigned through the `SceneMusic` component.


## Settings

The Settings menu currently supports:

* Master Volume
* Music Volume
* SFX Volume
* UI Volume

SFX and UI volume sliders can play sample sounds when released.

<img width="1200" height="660" alt="ghsettings" src="https://github.com/user-attachments/assets/a4c5bb0e-35fc-4699-86eb-2d5fdc1779d5" />


## Credits

This starter kit includes CC0 audio and graphics.

When making a game with this kit, update the Credits panel with:

* Music credits
* Sound effect credits
* Font credits
* Asset credits
* Tool credits


## Known Limitations

* The kit is designed for small 2D top-down games and game jams.


## License

See LICENSE (file)
