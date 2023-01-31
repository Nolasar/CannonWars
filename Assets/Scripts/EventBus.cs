using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    // Using for update score when projectile interact with correct box
    public static Action onCorrectBoxTouch;

    // Using for update UI when game start/end cooldowns changing
    public static Action onCooldownToStartChanges;
    public static Action onCooldownToEndChanges;

    // Using for update lives when projectile interact with wrong box
    public static Action onWrongBoxTouch;

    // Activate events when game is over
    public static Action onGameOver;

    // Activate events when game starts
    public static Action onGameStart;

}
