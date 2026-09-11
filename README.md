# 2D Flash Game: Escape From Earth

A 2D action game developed with Unity and C#.

The player must defeat incoming enemies, avoid various attacks, collect useful items, and survive until the boss battle.

## 🎮 Features

- Left and right movement
- Double jump
- Left and right sword attacks
- Enemy spawning from both sides
- Enemy speed gradually increases over time
- Score system
  - +100 points for defeating an enemy
  - -50 points for a missed attack
- Horizontal laser attacks
- Vertical bombing attacks
- Item drop system
  - Slow item
  - Shield item
- Shield protects the player from one attack
- Boss battle after reaching 3,000 points
- Boss HP UI
- Random boss attack patterns
  - Meteor attack
  - Mud wave attack
- Boss must be hit 200 times to defeat it
- Red screen flashing effect when entering the boss stage
- Game Over system

## 🕹 Controls

| Key | Action |
|---|---|
| A | Move Left |
| D | Move Right |
| Space | Jump |
| K | Attack Left |
| L | Attack Right |

## 👾 Enemy System

Enemies continuously spawn from both sides of the screen.

The enemy speed gradually increases as the game progresses, making the game increasingly difficult over time.

Players can defeat enemies by attacking them from the correct direction.

- Enemy defeated: **+100 points**
- Missed attack: **-50 points**

## ⚡ Attack & Hazard System

Before the boss stage, the player must avoid multiple environmental attacks.

### Laser

Horizontal lasers appear during the normal stage.

### Bomb

Bombs fall vertically from the sky after a warning indicator appears on the ground.

## 🎁 Item System

Items begin to appear after a certain amount of time has passed.

### Slow Item

Slows down the game, giving the player more time to react.

### Shield Item

The shield protects the player from one incoming attack.

The shield item has a lower drop probability than the slow item because of its stronger defensive effect.

## 👹 Boss Battle

When the player reaches **3,000 points**, the boss stage begins.

A red screen flashing effect is displayed several times to indicate the transition into the boss stage.

The boss appears in the center of the screen and has a dedicated HP bar displayed at the top.

The boss has **200 HP** and must be hit 200 times to defeat it.

During the boss battle, two attack patterns are randomly selected:

- ☄️ Meteor attack
- 🌊 Mud wave attack

The boss continuously uses these attacks in random order until it is defeated.

## 🛠 Technologies

- Unity 6
- C#
- Unity 2D Physics
- TextMeshPro
- Unity UI

## 📂 Main Scripts

- `Player.cs` - Player movement, jumping, attacking, damage, and shield system
- `Enemy.cs` - Enemy movement and collision
- `Spawner.cs` - Enemy, laser, bomb, and item spawning
- `GameManager.cs` - Score, difficulty, game state, and boss stage management
- `Boss.cs` - Boss HP and attack patterns
- `Bomb.cs` - Falling bomb behavior
- `WarningLine.cs` - Bomb warning system
- `Meteor.cs` - Meteor attack behavior
- `MudWave.cs` - Mud wave attack behavior

## 🎯 Objective

Survive the increasing difficulty, defeat incoming enemies, reach 3,000 points, and defeat the boss while avoiding its random attack patterns.
