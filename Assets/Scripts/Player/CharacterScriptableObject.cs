using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField] private GameObject startingSkill;
    [SerializeField] private Sprite characterIcon;
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private float maxHealth;
    [SerializeField] private float recovery;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float might;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float magnetRadius;
    [SerializeField] private float cooldown;

    public string _characterName { get => characterName; private set => characterName = value; }
    public GameObject _startingSkill { get => startingSkill; private set => startingSkill = value; }
    public Sprite _characterIcon { get => characterIcon; private set => characterIcon = value; }
    public Sprite _characterSprite { get => characterSprite; private set => characterSprite = value; }
    public float _maxHealth { get => maxHealth; private set => maxHealth = value; }
    public float _recovery { get => recovery; private set => recovery = value; }
    public float _moveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    public float _might { get => might; private set => might = value; }
    public float _projectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }
    public float _magnetRadius { get => magnetRadius; private set => magnetRadius = value; }
    public float _cooldown { get => cooldown; private set => cooldown = value; }

}
