using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    public PassiveItemScriptableObject passiveItemData;
    protected PlayerStats player;

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }
    private void Update()
    {
        
    }

    protected virtual void ApplyModifier()
    {

    }
}
