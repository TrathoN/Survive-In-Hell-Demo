using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private CharacterScriptableObject characterData;

    private float currentHealth;
    private float currentRecovery;
    private float currentMoveSpeed;
    private float currentMight;
    private float currentProjectileSpeed;
    private float currentMagnetRadius;
    private float currentCooldown;

    #region Current Stat Properties
    public float CurrentHealth
    {
        get { return currentHealth;}
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentHealthText.text += currentHealth;
                }
            }
        }
    }
    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryText.text += currentRecovery;
                }
            }
        }
    }
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedText.text += currentMoveSpeed;
                }
            }
        }
    }
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightText.text += currentMight;
                }
            }
        }
    }
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedText.text += currentProjectileSpeed;
                }
            }
        }
    }
    public float CurrentMagnetRadius
    {
        get { return currentMagnetRadius; }
        set
        {
            if (currentMagnetRadius != value)
            {
                currentMagnetRadius = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetRadiusText.text += currentMagnetRadius;
                }
            }
        }
    }
    public float CurrentCooldown
    {
        get { return currentCooldown; }
        set
        {
            if (currentCooldown != value)
            {
                currentCooldown = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentCooldownText.text += currentCooldown;
                }
            }
        }
    }
    #endregion

    [Header("Experience")]
    [SerializeField] private int level;
    [SerializeField] private int experience;
    [SerializeField] private int experienceCap;

    [Header("I-Frames")]
    [SerializeField] private float invincibilityDuration;
    private float invincibilityTimer;
    private bool isInvincible;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    [SerializeField] private List<LevelRange> levelRanges;

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI levelDisplay;

    public GameObject debug;

    private void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingelton();

        inventory = GetComponent<InventoryManager>();

        CurrentHealth = characterData._maxHealth;
        CurrentRecovery = characterData._recovery;
        CurrentMoveSpeed = characterData._moveSpeed;
        CurrentMight = characterData._might;
        CurrentProjectileSpeed = characterData._projectileSpeed;
        CurrentMagnetRadius = characterData._magnetRadius;
        CurrentCooldown = characterData._cooldown;

        SetPlayerIcon();
        AttachSpell(characterData._startingSkill);
        SpawnWeapon(debug);
    }

    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;

        GameManager.instance.AssignStatsUI(currentHealth, currentRecovery, currentMoveSpeed, currentMight, currentProjectileSpeed, currentMagnetRadius, currentCooldown);
        GameManager.instance.AssignCharacterUI(characterData._characterName, characterData._characterSprite);

        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelDisplay();
    }

    private void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if(isInvincible)
        {
            isInvincible = false;
        }

        Recovery();
        UpdateHealthBar();
    }

    public void AddXp(int xp)
    {
        experience += xp;
        LevelUp();
        UpdateExpBar();
    }

    private void LevelUp()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            int experinceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experinceCapIncrease = range.experienceCapIncrease;
                    break;
                }

            }
            experienceCap += experinceCapIncrease;
            UpdateLevelDisplay();
            GameManager.instance.StartLevelUp();
        }
    }

    private void UpdateExpBar()
    {
        expBar.fillAmount = (float)experience / experienceCap;
    }

    private void UpdateLevelDisplay()
    {
        levelDisplay.text = "Level: " + level.ToString();
    }

    public void TakeDamage(float dmg)
    {
        if(!isInvincible)
        {
            CurrentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (CurrentHealth <= 0)
            {
                Kill();
            }
            UpdateHealthBar();
        }
        
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / characterData._maxHealth;
    }

    private void Kill()
    {
        if(!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignSpellUI(inventory.spellUISlot);
            GameManager.instance.AssignWeaponsandPassiveItemsUI(inventory.weaponUISlot, inventory.passiveItemUISlot);
            GameManager.instance.GameOver();
        }
    }

    public void RestoreHealth(int health)
    {
        if(CurrentHealth < characterData._maxHealth)
        {
            CurrentHealth += health;

            if (CurrentHealth >= characterData._maxHealth)
            {
                CurrentHealth = characterData._maxHealth;
            }
        }
    }

    private void Recovery()
    {
        if(CurrentHealth < characterData._maxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;

            if(CurrentHealth >= characterData._maxHealth)
            {
                CurrentHealth = characterData._maxHealth;
            }
        }
    }

    private void SetPlayerIcon()
    {
        inventory.SetCharacterIcon(characterData._characterIcon);
    }

    public void AttachSpell(GameObject spell)
    {
        GameObject attachedSpell = Instantiate(spell, transform.position, Quaternion.identity);
        attachedSpell.transform.SetParent(transform);
        inventory.AddSpell(attachedSpell.GetComponent<SpellController>());
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if(weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory already full");
            return;
        }
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Inventory already full");
            return;
        }
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
    }
}                 