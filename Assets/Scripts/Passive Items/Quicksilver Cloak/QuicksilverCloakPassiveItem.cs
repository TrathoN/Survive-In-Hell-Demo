using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuicksilverCloakPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentCooldown *= 1 - passiveItemData._multiplier / 100f;
    }
}
