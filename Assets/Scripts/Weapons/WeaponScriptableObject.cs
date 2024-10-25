using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private string weaponDescription;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private GameObject weaponNextLevelPrefab;
    [SerializeField] private WeaponScriptableObject weaponNextLevelData;
    [SerializeField] private Sprite weaponSpriteIcon;
    [SerializeField] private float weaponDamage;
    [SerializeField] private float weaponSpeed;
    [SerializeField] private int weaponPierce;
    [SerializeField] private float weaponCooldownDur;
    [SerializeField] private int weaponProjectile;
    [SerializeField] private float weaponProjectileInterval;
    [SerializeField] private float weaponRadius;
    [SerializeField] private int weaponLevel;
    

    public string _weaponName { get => weaponName; private set => weaponName = value; }
    public string  _weaponDesciption { get => weaponDescription; private set => weaponDescription = value; }
    public GameObject _weaponPrefab { get =>  weaponPrefab; private set => weaponPrefab = value;}  
    public GameObject _weaponNextLevelPrefab { get => weaponNextLevelPrefab; private set => weaponNextLevelPrefab = value;}
    public WeaponScriptableObject _weaponNextLevelData { get => weaponNextLevelData; private set => weaponNextLevelData = value;}
    public Sprite _weaponSpriteIcon { get => weaponSpriteIcon; private set => weaponSpriteIcon = value;}
    public float _weaponDamage { get => weaponDamage; private set => weaponDamage = value;}
    public float _weaponSpeed { get => weaponSpeed; private set => weaponSpeed = value;}
    public int _weaponPierce { get => weaponPierce; private set => weaponPierce = value; }
    public float _weaponCooldownDur {  get => weaponCooldownDur; private set => weaponCooldownDur = value; }
    public int _weaponProjectile { get => weaponProjectile; private set => weaponProjectile = value;}
    public float _weaponProjectileInterval { get => weaponProjectileInterval; private set =>weaponProjectileInterval = value;}
    public float _weaponRadius { get => weaponRadius; private set => weaponRadius = value; }
    public int _weaponLevel { get => weaponLevel; private set => weaponLevel = value;}
}
