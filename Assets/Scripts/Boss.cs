using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    // ========== CORE SYSTEMS ==========
    [Header("Base Settings")]
    [SerializeField] private bool isBoss1;
    public int maxHP = 125;
    public Slider hpSlider;
    public GameObject portal1ToDisable;
    public GameObject portal2ToDisable;
    public GameObject winUI; // UI yang muncul saat menang
    private static int defeatedBossCount = 0; // Track jumlah boss yang sudah kalah

    [Header("Fear Settings")]
    private int boss1Move1Fear = 1;
    private int boss1Move1Turns = 3;
    private int boss1Move2Fear = 2;
    private int boss1Move2Turns = 2;
    private int boss1Move3Fear = 5;
    private int boss2Move1Fear = 3;
    private float boss2Move1MissChance = 30;
    private int boss2Move2Fear = 2;
    private int boss2Move3Fear = 5;
    private int boss2Move3Turns = 2;

    // ========== PRIVATE VARIABLES ==========
    private int currentHP;
    private Coroutine currentRoutine;
    private PlayerCombatActions player;
    private BattleSystem battleSystem;
    private LevelLoader levelLoader;

    void Start()
    {
        InitializeBoss();
        InitializeHealthUI();
        CheckBossDefeatedStatus();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBossDefeatStatus();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (winUI != null) winUI.SetActive(false);
        }
    }

    // ========== INITIALIZATION ==========
    void InitializeBoss()
    {
        currentHP = maxHP;
        player = FindFirstObjectByType<PlayerCombatActions>();
        battleSystem = FindFirstObjectByType<BattleSystem>();
        levelLoader = FindFirstObjectByType<LevelLoader>();
    }

    void CheckBossDefeatedStatus()
    {
        if (PlayerPrefs.GetInt("Boss1Defeated", 0) == 1 && portal1ToDisable != null)
        {
            portal1ToDisable.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Boss2Defeated", 0) == 1 && portal2ToDisable != null)
        {
            portal2ToDisable.SetActive(false);
        }
        if (PlayerPrefs.GetInt("BothBossesDefeated", 0) == 1 && winUI != null)
        {
            winUI.SetActive(true);
        }
    }

    void ResetBossDefeatStatus()
    {
        PlayerPrefs.DeleteKey("Boss1Defeated");
        PlayerPrefs.DeleteKey("Boss2Defeated");
        PlayerPrefs.DeleteKey("BothBossesDefeated");
        if (portal1ToDisable != null) portal1ToDisable.SetActive(true);
        if (portal2ToDisable != null) portal2ToDisable.SetActive(true);
        if (winUI != null) winUI.SetActive(false);
        Debug.Log("Boss defeat status reset!");
    }

    // ========== COMBAT SYSTEM ==========
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Max(currentHP - damage, 0);
        UpdateHealthUI();

        if (currentHP <= 0) HandleDefeat();
    }

    void HandleDefeat()
    {
        Debug.Log("BOSS DIKALAHKAN!");
        defeatedBossCount++;

        

        if (defeatedBossCount >= 2)
        {
            PlayerPrefs.SetInt("BothBossesDefeated", 1);
            ShowWinUI();
            SceneManager.LoadScene("Menu Scene");
        }
        else
        {
            SceneManager.LoadScene("Spawn Map");
        }
    }

    void ShowWinUI()
    {
        if (winUI != null)
        {
            winUI.SetActive(true);
        }
    }

    // ========== MISS DEBUFF SYSTEM ==========
    public void ApplyMissDebuff(float missChance)
    {
        player.ApplyMissChance(missChance);
    }

    public bool CheckMiss()
    {
        return player.CheckForMiss();
    }

    // ========== BOSS ATTACK SYSTEM ==========
    public int CalculateBossAttack()
    {
        return isBoss1 ? Random.Range(1, 4) : Random.Range(2, 6);
    }

    public void ExecuteMove(int moveIndex)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        if (isBoss1) HandleBoss1Moves(moveIndex);
        else HandleBoss2Moves(moveIndex);
    }

    // ========== BOSS1 MOVES ==========
    void HandleBoss1Moves(int moveIndex)
    {
        switch (moveIndex)
        {
            case 0:
                currentRoutine = StartCoroutine(FearOverTime(boss1Move1Fear, boss1Move1Turns));
                break;
            case 1:
                currentRoutine = StartCoroutine(FearOverTime(boss1Move2Fear, boss1Move2Turns));
                break;
            case 2:
                player.UpdateFearMeter(boss1Move3Fear);
                break;
        }
    }

    // ========== BOSS2 MOVES ==========
    void HandleBoss2Moves(int moveIndex)
    {
        switch (moveIndex)
        {
            case 0:
                player.UpdateFearMeter(boss2Move1Fear);
                ApplyMissDebuff(boss2Move1MissChance);
                break;
            case 1:
                player.UpdateFearMeter(boss2Move2Fear);
                break;
            case 2:
                currentRoutine = StartCoroutine(FearOverTime(boss2Move3Fear, boss2Move3Turns));
                break;
        }
    }

    // ========== FEAR OVER TIME SYSTEM ==========
    IEnumerator FearOverTime(int fearPerTurn, int turns)
    {
        for (int i = 0; i < turns; i++)
        {
            yield return new WaitUntil(() => battleSystem.isPlayerTurn);
            player.UpdateFearMeter(fearPerTurn);
            yield return new WaitUntil(() => !battleSystem.isPlayerTurn);
        }
    }

    // ========== UI SYSTEM ==========
    void InitializeHealthUI()
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = maxHP;
        }
    }

    void UpdateHealthUI()
    {
        if (hpSlider != null)
            hpSlider.value = currentHP;
    }

    public int CurrentHP
    {
        get { return currentHP; }
    }
}