using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [Header("Spell Attributes")]
    public SpellScriptableObject spellData;
    private float spellCooldown;
    protected PlayerMovement pm;
    protected GameObject enemyPlayer;

    protected virtual void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
        spellCooldown = spellData._spellCooldownDur;
    }

    protected virtual void Update()
    {
        spellCooldown -= Time.deltaTime;
        if (spellCooldown <= .0f && Input.GetKey(KeyCode.Space) && enemyPlayer != null)
        {
            Attack();
        }      
    }

    protected virtual void Attack()
    {
        spellCooldown = spellData._spellCooldownDur;
    }
}
