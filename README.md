# CannonWars

**CannonWars** is an arcade-style mobile game where players defend against incoming projectiles by matching them to the correct boxes. Cannons shoot various types of projectiles toward the bottom center of the screen, where several static boxes are positioned. Each projectile corresponds to a specific box. The player gains points for matching projectiles to the correct box and loses lives for mismatches. The goal is to score as many points as possible while maintaining lives.

## Gameplay Overview
- **Objective**: Score by correctly matching the projectile type to its associated box.
- **Lives System**: The player loses one life per mismatch. Game over occurs when all lives are lost.
- **Score System**: Points are awarded for correct matches. The game tracks high scores.
  
## Key Classes and Files

### 1. **`Projectile.cs`**
   - Handles logic for each projectile shot from cannons.
   - Tracks type and movement direction.
   - Detects collisions with boxes, triggering scoring or life deduction based on match accuracy.

### 2. **`Box.cs`**
   - Defines each static box’s behavior.
   - Manages the box type, ensuring only projectiles of the same type are accepted.
   - Works with the score manager to update points.

### 3. **`Cannon.cs`**
   - Controls the cannon mechanics, including firing rate and projectile type.
   - Randomly selects projectile types to add challenge and variety to the game.

### 4. **`GameManager.cs`**
   - Central controller for game state, including tracking lives and score.
   - Handles game over scenarios and restarts.
   - Manages the interaction between projectiles and boxes.

### 5. **`ScoreManager.cs`**
   - Manages player score updates, including correct hits and bonuses.
   - Tracks high scores and saves them persistently.

### 6. **`UIManager.cs`**
   - Handles the user interface elements such as score display, life counter, and game over screens.
   - Provides player feedback and game control (pause, restart).
