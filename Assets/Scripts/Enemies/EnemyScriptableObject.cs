using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField] private float enemyMaxHealth;
    [SerializeField] private float enemyMoveSpeed;
    [SerializeField] private float enemyDamage;

    public float _enemyMaxHealth { get => enemyMaxHealth; private set => enemyMaxHealth = value; }
    public float _enemyMoveSpeed { get => enemyMoveSpeed; private set => enemyMoveSpeed = value; }
    public float _enemyDamage { get => enemyDamage; private set => enemyDamage = value; }
}
