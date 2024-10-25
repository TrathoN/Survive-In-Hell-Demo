using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image characterUISlot;
    public SpellController spellSlot;
    public int spellLevel;
    public Image spellUISlot;
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlot = new List<Image>(6);
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public int[] passiveItemLevels = new int[6];
    public List<Image> passiveItemUISlot = new List<Image>(6);
    public bool isSpellUpgradeDefined;

    [System.Serializable]
    public class SpellUpgrade
    {
        public GameObject initialSpell;
        public SpellScriptableObject spellData;
    }

    [System.Serializable]
    public class WeaponUpgrade
    {
        public GameObject initialWeapon;
        public WeaponScriptableObject weaponData;
    }

    [System.Serializable]
    public class PassiveItemUpgrade
    {
        public GameObject initialPassiveItem;
        public PassiveItemScriptableObject passiveItemData;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public TextMeshProUGUI upgradeNameDisplay;
        public TextMeshProUGUI upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    public SpellUpgrade spellUpgradeOption;
    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<PassiveItemUpgrade> passiveItemUpgradeOptions = new List<PassiveItemUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();


    private PlayerStats player;

    private void Start()
    {
        player = GetComponent<PlayerStats>();
    }

    public void SetCharacterIcon(Sprite icon)
    {
        characterUISlot.enabled = true;
        characterUISlot.sprite = icon;
    }

    public void AddSpell(SpellController spell)
    {
        spellSlot = spell;
        spellLevel = spell.spellData._spellLevel;
        spellUISlot.enabled = true;
        spellUISlot.sprite = spell.spellData._spellSpriteIcon;

        if (GameManager.instance != null && GameManager.instance.isLevelUpState)
        {
            GameManager.instance.EndLevelUp();
        }
    }
    public void AddWeapon(int index, WeaponController weapon)
    {
        weaponSlots[index] = weapon;
        weaponLevels[index] = weapon.weaponData._weaponLevel;
        weaponUISlot[index].enabled = true;
        weaponUISlot[index].sprite = weapon.weaponData._weaponSpriteIcon;

        if (GameManager.instance != null && GameManager.instance.isLevelUpState)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void AddPassiveItem(int index, PassiveItem passiveItem)
    {
        passiveItemSlots[index] = passiveItem;
        passiveItemLevels[index] = passiveItem.passiveItemData._passiveItemLevel;
        passiveItemUISlot[index].enabled = true;
        passiveItemUISlot[index].sprite = passiveItem.passiveItemData._passiveItemSpriteIcon;

        if (GameManager.instance != null && GameManager.instance.isLevelUpState)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void LevelUpSpell(SpellUpgrade chosenUpgrade)
    {
        if (spellSlot != null)
        {
            SpellController spell = spellSlot;
            if (!spell.spellData._spellNextLevelPrefab)
            {
                Debug.LogError("No next level for" + spell.name);
                return;
            }
            GameObject upgradedSpell = Instantiate(spell.spellData._spellNextLevelPrefab, transform.position, Quaternion.identity);
            upgradedSpell.transform.SetParent(transform);
            AddSpell(upgradedSpell.GetComponent<SpellController>());
            Destroy(spell.gameObject);
            spellLevel = upgradedSpell.GetComponent<SpellController>().spellData._spellLevel;

            spellUpgradeOption.initialSpell = chosenUpgrade.spellData._spellNextLevelPrefab;
            spellUpgradeOption.spellData = chosenUpgrade.spellData._spellNextLevelData;

            if (GameManager.instance != null && GameManager.instance.isLevelUpState)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    public void LevelUpWeapon(int index, WeaponUpgrade chosenUpgrade, int chosenUpgradeIndex)
    {
        if (weaponSlots.Count > index)
        {
            WeaponController weapon = weaponSlots[index];
            if (!weapon.weaponData._weaponNextLevelPrefab)
            {
                Debug.LogError("No next level for" + weapon.name);
                return;
            }
            GameObject upgradedWeapon = Instantiate(weapon.weaponData._weaponNextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);
            AddWeapon(index, upgradedWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[index] = upgradedWeapon.GetComponent<WeaponController>().weaponData._weaponLevel;

            weaponUpgradeOptions[chosenUpgradeIndex].initialWeapon = chosenUpgrade.weaponData._weaponNextLevelPrefab;
            weaponUpgradeOptions[chosenUpgradeIndex].weaponData = chosenUpgrade.weaponData._weaponNextLevelData;

            if (GameManager.instance != null && GameManager.instance.isLevelUpState)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    public void LevelUpPassiveItem(int index, PassiveItemUpgrade chosenUpgrade, int chosenUpgradeIndex)
    {
        if (passiveItemSlots.Count > index)
        {
            PassiveItem passiveItem = passiveItemSlots[index];
            if (!passiveItem.passiveItemData._passiveItemNextLevelPrefab)
            {
                Debug.LogError("No next level for" + passiveItem.name);
                return;
            }
            GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData._passiveItemNextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform);
            AddPassiveItem(index, upgradedPassiveItem.GetComponent<PassiveItem>());
            Destroy(passiveItem.gameObject);
            passiveItemLevels[index] = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData._passiveItemLevel;

            passiveItemUpgradeOptions[chosenUpgradeIndex].initialPassiveItem = chosenUpgrade.passiveItemData._passiveItemNextLevelPrefab;
            passiveItemUpgradeOptions[chosenUpgradeIndex].passiveItemData = chosenUpgrade.passiveItemData._passiveItemNextLevelData;

            if (GameManager.instance != null && GameManager.instance.isLevelUpState)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    private void ApplyUpgradeOptions()
    {
        isSpellUpgradeDefined = false;

        SpellUpgrade availableSpellUpgrade = spellUpgradeOption;
        List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(weaponUpgradeOptions);
        List<PassiveItemUpgrade> availablePassiveItemUpgrade = new List<PassiveItemUpgrade>(passiveItemUpgradeOptions);

        foreach (var upgradeOption in upgradeUIOptions)
        {
            if(availableWeaponUpgrades.Count == 0 && availablePassiveItemUpgrade.Count == 0 && availableSpellUpgrade == null)
            {
                return;
            }

            int upgradeType;

            if(availableSpellUpgrade == null)
            {
                upgradeType = Random.Range(2, 4);
            }
            else if (availableWeaponUpgrades.Count == 0)
            {
                upgradeType = Random.Range(1, 4);
                if(upgradeType == 2)
                {
                    upgradeType = 3;
                }
            }
            else if(availablePassiveItemUpgrade.Count == 0)
            {
                upgradeType = Random.Range(1, 3);
            }
            else if(availableWeaponUpgrades.Count == 0 && availablePassiveItemUpgrade.Count == 0)
            {
                upgradeType = 1;
            }
            else if(availableSpellUpgrade == null && availablePassiveItemUpgrade.Count == 0)
            {
                upgradeType = 2;
            }
            else if(availableSpellUpgrade == null && availableWeaponUpgrades.Count == 0)
            {
                upgradeType = 3;
            }
            else
            {
                upgradeType = Random.Range(1, 4);
            }
           

            if (upgradeType == 1)
            {
                isSpellUpgradeDefined = true; //set the bool false somewhere
                SpellUpgrade chosenSpellUpgrade = availableSpellUpgrade;

                availableSpellUpgrade = null;

                if (chosenSpellUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);

                    if (spellSlot != null && spellSlot.spellData == chosenSpellUpgrade.spellData)
                    {
                        if (!chosenSpellUpgrade.spellData._spellNextLevelPrefab)
                        {
                            DisableUpgradeUI(upgradeOption);
                        }

                        upgradeOption.upgradeNameDisplay.text = chosenSpellUpgrade.spellData._spellNextLevelPrefab.GetComponent<SpellController>().spellData._spellName;
                        upgradeOption.upgradeDescriptionDisplay.text = chosenSpellUpgrade.spellData._spellNextLevelPrefab.GetComponent<SpellController>().spellData._spellDescription;
                        upgradeOption.upgradeIcon.sprite = chosenSpellUpgrade.spellData._spellSpriteIcon;
                        upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpSpell(chosenSpellUpgrade));
                    }
                }
            }
            else if (upgradeType == 2)
            {
                int randWeapon = Random.Range(0, availableWeaponUpgrades.Count);
                WeaponUpgrade chosenWeaponUpgrade = availableWeaponUpgrades[randWeapon];

                availableWeaponUpgrades.Remove(chosenWeaponUpgrade);

                if (chosenWeaponUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);
                    bool newWeapon = false;
                    for (int i = 0; i < weaponSlots.Count; i++)
                    {
                        if (weaponSlots[i] != null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
                        {
                            newWeapon = false;
                            if (!newWeapon)
                            {
                                if (!chosenWeaponUpgrade.weaponData._weaponNextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                }

                                upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData._weaponNextLevelPrefab.GetComponent<WeaponController>().weaponData._weaponName;
                                upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData._weaponNextLevelPrefab.GetComponent<WeaponController>().weaponData._weaponDesciption;
                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i, weaponUpgradeOptions[randWeapon], randWeapon));
                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    }

                    if (newWeapon)
                    {
                        upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData._weaponName;
                        upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData._weaponDesciption;
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenWeaponUpgrade.initialWeapon));
                    }

                    upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData._weaponSpriteIcon;
                }
            }
            else if (upgradeType == 3)
            {
                int randPassiveItem = Random.Range(0, availablePassiveItemUpgrade.Count);
                PassiveItemUpgrade chosenPassiveItemUpgrade = availablePassiveItemUpgrade[randPassiveItem];

                availablePassiveItemUpgrade.Remove(chosenPassiveItemUpgrade);

                if (chosenPassiveItemUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);
                    bool newPassiveItem = false;
                    for (int i = 0; i < passiveItemSlots.Count; i++)
                    {
                        if (passiveItemSlots[i] != null && passiveItemSlots[i].passiveItemData == chosenPassiveItemUpgrade.passiveItemData)
                        {
                            newPassiveItem = false;
                            if (!newPassiveItem)
                            {
                                if(!chosenPassiveItemUpgrade.passiveItemData._passiveItemNextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                }
                                upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData._passiveItemNextLevelPrefab.GetComponent<PassiveItem>().passiveItemData._passiveItemName;
                                upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData._passiveItemNextLevelPrefab.GetComponent<PassiveItem>().passiveItemData._passiveItemDesciption;
                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpPassiveItem(i, passiveItemUpgradeOptions[randPassiveItem], randPassiveItem));
                            }
                            break;
                        }
                        else
                        {
                            newPassiveItem = true;
                        } 
                    }

                    if (newPassiveItem)
                    {
                        upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData._passiveItemName;
                        upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData._passiveItemDesciption;
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(chosenPassiveItemUpgrade.initialPassiveItem));
                    }

                    upgradeOption.upgradeIcon.sprite = chosenPassiveItemUpgrade.passiveItemData._passiveItemSpriteIcon;
                }
            }
        }
    }

    private void RemoveUpgradeOptions()
    {
        foreach(var upgradeOptions in upgradeUIOptions)
        {
            upgradeOptions.upgradeButton.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgradeOptions);
        }
    }

    public void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }

    private void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(false);
    }
    private void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(true);
    }
}
