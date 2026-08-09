Version: 0.2.0

# Unity 6 Top-Down Game Starter Kit

A lightweight 2D top-down starter kit for Unity 6, designed to help game projects get started faster.

The kit includes many common features. Scroll down for the full list.

<!-- <img width="854" height="480" alt="templatedemo" src="https://github.com/user-attachments/assets/e5669270-bade-493c-985d-f2daa9325897" /> -->

<img width="1280" height="720" alt="unitytemplate020-ezgif com-video-to-gif-converter" src="https://github.com/user-attachments/assets/0de42d15-5833-47a7-a221-88f096941c7c" />


## Unity Version

Built with Unity 6.4 / Unity Editor 6000.4.6f1.

## Development Note

This starter kit was developed with assistance from generative AI for planning, code iteration, debugging, and documentation. The project was manually implemented, tested, organized, and published in Unity.


## Starting

Open `Assets/_TopDownStarter/Scenes/00_Boot.unity` and press Play to start the template. The boot scene creates the persistent core managers, then loads `00_Splash`, which fades into `01_MainMenu`.

The `02_Game` and `03_Tutorial` scenes can also be played directly because they include a Bootstrapper.


## Features

Scene flow and structure:

- Boot scene and persistent core managers
- Splash screen with fade timing and skip input
- Direct scene testing with Bootstrapper
- Main menu
- Tutorial button
- Instructions panel
- Settings menu
- Credits popup
- Scene loading and scene transition
- Tutorial scene

Gameplay systems:

- Pause menu
- Game Over screen
- Victory screen
- New Input System top-down player movement
- Smooth camera follow
- Health system
- Health display
- World-space health bars
- Score system
- Score display
- Countdown timer
- Timer display
- Score pickups
- Health pickups
- Damage zones
- Collect-all objective
- Simple chaser enemy

Tutorial systems:

- Step-based tutorial system
- Tutorial goal overlay
- Tutorial trigger volumes

Audio and settings:

- Audio manager
- Scene music
- SFX and UI sound support
- Saved audio volume settings
- Resettable audio volume settings

UI helpers:

- UI hover scale, shine, pulse, rotation, and scrolling background helpers
- Open URL button helper


## Scene Overview

### `00_Boot`

Initial startup scene.

Creates the persistent `Core` object, which contains core systems such as:

- GameManager
- SceneLoader
- AudioManager
- SceneTransition

In a normal build, the game should start from this scene.


### `00_Splash`

Splash/loading intro scene.

Includes:

- Fade in, hold, and fade out timing
- Keyboard, mouse, and gamepad skip support
- Automatic transition to the main menu

<img width="1340" height="878" alt="image" src="https://github.com/user-attachments/assets/2f274366-7b38-46e6-a759-73a00d466109" />


### `01_MainMenu`

Main menu scene.

Includes:

- Start Game button
- Tutorial button
- "How To Play" panel
- Settings menu
- Credits popup
- Quit button
- Menu music
- Discord and "Our Website" buttons to open a custom URL in a browser

<img width="1348" height="893" alt="image" src="https://github.com/user-attachments/assets/9032df35-fcf5-4ca8-b543-7e8ef8665e58" />




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

<img width="1349" height="885" alt="image" src="https://github.com/user-attachments/assets/c87a69e3-fa76-4b62-8542-0e8672533361" />



### `03_Tutorial`

Guided tutorial gameplay scene.

Includes:

- Tutorial steps with title, body text, optional image, and goal text
- Optional pause-on-step-start behavior
- Tutorial trigger volumes
- Goal overlay
- Victory trigger on tutorial completion

<img width="1344" height="878" alt="image" src="https://github.com/user-attachments/assets/1f799195-1716-4c63-a806-ae65f5f99434" />


## Folder Structure

```text
Assets/
  _TopDownStarter/
    Art/
    Audio/
    Materials/
    Prefabs/
    Scenes/
    Scripts/
    ScriptableObjects/
    Settings/
```
	

## Controls

Keyboard:

* Move: WASD / Arrow Keys
* Pause: Escape / P
* Splash skip: Any key

Gamepad:

* Move: Left Stick
* Pause: Start
* Splash skip: South button

Mouse:

* Splash skip: Left button

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

### Core

Located in:

Assets/_TopDownStarter/Prefabs/Core/


Creates the persistent core systems used by the scene flow.

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


## Tutorial

The tutorial system is driven by `TutorialManager`.

It supports:

* Ordered tutorial steps
* Full-screen tutorial panels
* Optional tutorial images
* Gameplay goal overlay text
* Optional pause when a step starts
* Step-specific trigger volumes
* Victory when the tutorial is completed

Tutorial triggers use `TutorialTrigger` and only activate for their assigned step index.


## Game States

The starter kit uses these game states:

* Booting
* MainMenu
* Playing
* Paused
* GameOver
* Victory

`GameManager` exposes an `OnGameStateChanged` event so UI and gameplay systems can react when the state changes.


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

Volume settings can be reset through `AudioManager.ResetVolumeSettings()`.


## How to Play 

The screen can be customized to help new players get started quickly.

<img width="1341" height="875" alt="image" src="https://github.com/user-attachments/assets/c58c43cd-9e09-47d5-9740-cef22a71c48f" />


## Settings

The Settings menu currently supports:

* Master Volume
* Music Volume
* SFX Volume
* UI Volume

SFX and UI volume sliders can play sample sounds when released.

<img width="1332" height="867" alt="image" src="https://github.com/user-attachments/assets/1ca1118f-45b2-4414-a335-15592d2835fc" />



## Credits

This starter kit includes CC0 audio and graphics.

When making a game with this kit, update the Credits panel with:

* Music credits
* Sound effect credits
* Font credits
* Asset credits
* Tool credits



## License

See LICENSE (file)
