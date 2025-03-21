using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // ========== CORE SYSTEMS ==========
    [Header("Base Settings")]
    public bool isBoss1;           // Centang untuk Boss1
    public int maxHP = 125;        // HP maksimal
    public Slider hpSlider;        // Assign UI Slider HP

    [Header("Fear Settings")]
    public int boss1Move1Fear = 1; // Fear per turn (Move1 Boss1)
    public int boss1Move1Turns = 3;// Durasi (Move1 Boss1)
    public int boss1Move2Fear = 2;
    public int boss1Move2Turns = 2;
    public int boss1Move3Fear = 5;
    public int boss2Move1Fear = 3;
    public float boss2Move1MissChance = 30; // % miss chance
    public int boss2Move2Fear = 2;
    public int boss2Move3Fear = 5;
    public int boss2Move3Turns = 2;

    // ========== PRIVATE VARIABLES ==========
    private int currentHP;
    private Coroutine currentRoutine;
    private PlayerCombatActions player;
    private BattleSystem battleSystem;

    void Start()
    {
        InitializeBoss();
        InitializeHealthUI();
    }

    // ========== INITIALIZATION ==========
    void InitializeBoss()
    {
        currentHP = maxHP;
        player = FindFirstObjectByType<PlayerCombatActions>();
        battleSystem = FindFirstObjectByType<BattleSystem>();
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
        // Tambahkan logika kemenangan di sini
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
        return isBoss1 ?
            Random.Range(1, 4) :   // Boss1: 1-3 damage 
            Random.Range(2, 6);     // Boss2: 2-5 damage
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
            case 0: // Fear 1/turn for 3 turns
                currentRoutine = StartCoroutine(FearOverTime(
                    boss1Move1Fear,
                    boss1Move1Turns
                ));
                break;

            case 1: // Fear 2/turn for 2 turns
                currentRoutine = StartCoroutine(FearOverTime(
                    boss1Move2Fear,
                    boss1Move2Turns
                ));
                break;

            case 2: // Instant 5 fear
                player.UpdateFearMeter(boss1Move3Fear);
                break;
        }
    }

    // ========== BOSS2 MOVES ==========
    void HandleBoss2Moves(int moveIndex)
    {
        switch (moveIndex)
        {
            case 0: // Fear 3 + 30% miss
                player.UpdateFearMeter(boss2Move1Fear);
                ApplyMissDebuff(boss2Move1MissChance);
                break;

            case 1: // Instant 2 fear
                player.UpdateFearMeter(boss2Move2Fear);
                break;

            case 2: // Fear 5/turn for 2 turns
                currentRoutine = StartCoroutine(FearOverTime(
                    boss2Move3Fear,
                    boss2Move3Turns
                ));
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