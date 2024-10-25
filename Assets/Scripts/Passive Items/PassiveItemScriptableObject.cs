using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObjects/PassiveItem")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField] private string passiveItemName;
    [SerializeField] private string passiveItemDescription;
    [SerializeField] private GameObject passiveItemNextLevelPrefab;
    [SerializeField] private PassiveItemScriptableObject passiveItemNextLevelData;
    [SerializeField] private Sprite passiveItemSpriteIcon;
    [SerializeField] private float multiplier;
    [SerializeField] private int passiveItemLevel;

    public string _passiveItemName { get => passiveItemName; private set => passiveItemName = value; }
    public string _passiveItemDesciption { get => passiveItemDescription; private set => passiveItemDescription = value; }
    public GameObject _passiveItemNextLevelPrefab { get => passiveItemNextLevelPrefab; private set => passiveItemNextLevelPrefab = value; }
    public PassiveItemScriptableObject _passiveItemNextLevelData { get => passiveItemNextLevelData; private set => passiveItemNextLevelData = value; }
    public Sprite _passiveItemSpriteIcon { get => passiveItemSpriteIcon; private set => passiveItemSpriteIcon = value; }
    public float _multiplier { get => multiplier; private set => multiplier = value; }
    public int _passiveItemLevel { get => passiveItemLevel; private set => passiveItemLevel = value; }
}
