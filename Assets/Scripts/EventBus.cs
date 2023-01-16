using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    // Using for update score when projectile interact with correct box
    public static Action onCorrectBoxTouch;

    // Using for update UI when game start cooldown changed
    public static Action onCooldownToStartChanges;

    public static Action onCooldownToEndChanges;

    public static Action onWrongBoxTouch;

}
