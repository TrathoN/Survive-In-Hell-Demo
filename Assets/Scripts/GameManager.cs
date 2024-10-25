using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp,
    }

    [SerializeField] public GameState currentState;
    public GameState previousState;

    [Header("Screens")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject resultScreen;
    [SerializeField] private GameObject levelUpScreen;

    [Header("Start Displays")]
    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI currentRecoveryText;
    public TextMeshProUGUI currentMoveSpeedText;
    public TextMeshProUGUI currentMightText;
    public TextMeshProUGUI currentProjectileSpeedText;
    public TextMeshProUGUI currentMagnetRadiusText;
    public TextMeshProUGUI currentCooldownText;

    [Header("Result Screen Displays")]
    public TextMeshProUGUI characterName;
    public Image characterImage;
    public TextMeshProUGUI timeSurvived;
    public TextMeshProUGUI levelReached;
    public Image spellUI;
    public List<Image> weaponUI = new List<Image>(6);
    public List<Image> passiveUI = new List<Image>(6);

    [Header("Timer")]
    public float timeLimit;
    private float currentTime;
    public TextMeshProUGUI currentTimeText;

    public bool isGameOver;
    public bool isLevelUpState;

    public GameObject playerObject;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            UnityEngine.Debug.LogWarning("Extra " + this + " deleted");
            Destroy(gameObject);
        }
        DisableScreens();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckGameState();
                UpdateTimer();
                break;
            case GameState.Paused:
                CheckGameState();
                break;
            case GameState.GameOver:
                if(!isGameOver)
                {
                    Time.timeScale = 0f;
                    isGameOver = true;
                    UnityEngine.Debug.Log("Game Over");
                    DisplayResults();
                }
                break;
            case GameState.LevelUp:
                if(!isLevelUpState)
                {
                    Time.timeScale = 0f;
                    isLevelUpState = true;
                    levelUpScreen.SetActive(true);
                }
                break;
            default:
                UnityEngine.Debug.LogWarning("Wrong State");
            break;
        }
    }

    private void ChangeState(GameState newState) 
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        previousState = currentState;
        ChangeState(GameState.Paused);
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        ChangeState(previousState);
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    private void CheckGameState()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Gameplay)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    public void GameOver()
    {
        timeSurvived.text = currentTimeText.text;
        ChangeState(GameState.GameOver);
    }

    private void DisplayResults()
    {
        resultScreen.SetActive(true);
    }

    public void AssignStatsUI(float health, float recovery, float moveSpeed, float might, float projectileSpeed, float magnetRadius, float cooldown)
    {
        currentHealthText.text += health;
        currentRecoveryText.text += recovery;
        currentMoveSpeedText.text += moveSpeed;
        currentMightText.text += might;
        currentProjectileSpeedText.text += projectileSpeed;
        currentMagnetRadiusText.text += magnetRadius;
        currentCooldownText.text += cooldown;
    }

    public void AssignCharacterUI(string charName, Sprite charSprite)
    {
        characterName.text = charName;
        characterImage.sprite = charSprite;
    }

    public void AssignLevelReachedUI(int level)
    {
        levelReached.text = level.ToString();
    }

    public void AssignSpellUI(Image spell)
    {
        if(spell)
        {
            spellUI.enabled = true;
            spellUI.sprite = spell.sprite;
        }
        else
        {
            spellUI.enabled = false;
        }
        
    }

    public void AssignWeaponsandPassiveItemsUI(List<Image> weapons, List<Image> passives)
    {
        if(weapons.Count != weaponUI.Count || passives.Count != passiveUI.Count)
        {
            UnityEngine.Debug.Log("Item lists have different lenghts");
        }

        for(int i = 0; i < weaponUI.Count; i++)
        {
            if (weapons[i].sprite)
            {
                weaponUI[i].enabled = true;
                weaponUI[i].sprite = weapons[i].sprite;
            }
            else
            {
                weaponUI[i].enabled = false;
            }
        }

        for (int i = 0; i < passiveUI.Count; i++)
        {
            if (passives[i].sprite)
            {
                passiveUI[i].enabled = true;
                passiveUI[i].sprite = passives[i].sprite;
            }
            else
            {
                passiveUI[i].enabled = false;
            }
        }
    }

    private void UpdateTimer()
    {
        currentTime += Time.deltaTime;

        UpdateTimerDisplay();

        if ( currentTime >= timeLimit )
        {
            GameOver();
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt( currentTime / 60);
        int seconds = Mathf.FloorToInt( currentTime % 60);

        currentTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp()
    {
        isLevelUpState = false;
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
}
