using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellScriptableObject", menuName = "ScriptableObjects/Spell")]
public class SpellScriptableObject : ScriptableObject
{
    [SerializeField] private string spellName;
    [SerializeField] private string spellDescription;
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private GameObject spellNextLevelPrefab;
    [SerializeField] private SpellScriptableObject spellNextLevelData;
    [SerializeField] private Sprite spellSpriteIcon;
    [SerializeField] private float spellDamage;
    [SerializeField] private float spellExplosionDamage;
    [SerializeField] private float spellSpeed;
    [SerializeField] private int spellPierce;
    [SerializeField] private float spellCooldownDur;
    [SerializeField] private float spellExplosionRadius;
    [SerializeField] private int spellLevel;

    public string _spellName { get => spellName; private set => spellName = value; }
    public string _spellDescription { get => spellDescription; private set => spellDescription = value; }
    public GameObject _spellPrefab { get => spellPrefab; private set => spellPrefab = value; }
    public GameObject _spellNextLevelPrefab { get => spellNextLevelPrefab; private set => spellNextLevelPrefab = value; }
    public SpellScriptableObject _spellNextLevelData { get => spellNextLevelData; private set => spellNextLevelData = value; }
    public Sprite _spellSpriteIcon { get => spellSpriteIcon; private set => spellSpriteIcon = value; }
    public float _spellDamage { get => spellDamage; private set => spellDamage = value; }
    public float _spellExplosionDamage { get => spellExplosionDamage; private set => spellExplosionDamage = value; }
    public float _spellSpeed { get => spellSpeed; private set => spellSpeed = value; }
    public int _spellPierce { get => spellPierce; private set => spellPierce = value; }
    public float _spellCooldownDur { get => spellCooldownDur; private set => spellCooldownDur = value; }
    public float _spellExplosionRadius { get => spellExplosionRadius; private set => spellExplosionRadius = value;}
    public int _spellLevel { get => spellLevel; private set => spellLevel = value; }
}
