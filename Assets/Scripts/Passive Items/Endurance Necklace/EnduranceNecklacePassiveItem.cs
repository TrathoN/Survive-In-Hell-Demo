using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnduranceNecklacePassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentRecovery += passiveItemData._multiplier / 100f;
    }
}
