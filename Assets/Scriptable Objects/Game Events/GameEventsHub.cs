using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventsHub
{
    public static Hub_PlayerHealthUpdate PlayerHealthUpdate = new Hub_PlayerHealthUpdate();
    public static Hub_PlayerManaUpdate PlayerManaUpdate = new Hub_PlayerManaUpdate();
    public static Hub_SwordDamage SwordDamage = new Hub_SwordDamage();
    public static Hub_ShieldDestroyed ShieldDestroyed = new Hub_ShieldDestroyed();
}

