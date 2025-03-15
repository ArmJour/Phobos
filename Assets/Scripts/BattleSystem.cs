using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public enum BattleState { PlayerTurn, EnemyTurn }
    public BattleState state;

    public HealthSystem playerHealth;
    public HealthSystem enemyHealth;
    public SkillCheckSystem skillCheckSystem;

    public float missChance = 0.2f; // 20% kemungkinan miss (bisa diubah di Inspector)

    void Start()
    {
        state = BattleState.PlayerTurn; // Mulai dengan giliran pemain
    }

    public void ApplyDamageToEnemy(int damage)
    {
        enemyHealth.TakeDamage(damage);
        if (enemyHealth.IsDefeated())
        {
            Debug.Log("Musuh dikalahkan!");
        }
        else
        {
            state = BattleState.EnemyTurn;
            EnemyAttack();
        }
    }

    void EnemyAttack()
    {
        // Cek apakah musuh miss
        if (Random.value < missChance)
        {
            Debug.Log("Musuh miss!");
            state = BattleState.PlayerTurn; // Lanjutkan ke giliran pemain
            return;
        }

        // Musuh menyerang secara acak
        int attackIndex = Random.Range(0, 3); // 0, 1, atau 2
        int damage = 0;

        switch (attackIndex)
        {
            case 0:
                damage = 10;
                Debug.Log("Musuh menggunakan serangan lemah! Damage: " + damage);
                break;
            case 1:
                damage = 20;
                Debug.Log("Musuh menggunakan serangan sedang! Damage: " + damage);
                break;
            case 2:
                damage = 30;
                Debug.Log("Musuh menggunakan serangan kuat! Damage: " + damage);
                break;
        }

        // Berikan damage ke pemain
        playerHealth.TakeDamage(damage);

        // Kembalikan giliran ke pemain
        state = BattleState.PlayerTurn;
        Debug.Log("Giliran pemain!");
    }

    public void EndPlayerTurn()
    {
        state = BattleState.EnemyTurn;
        EnemyAttack();
    }
}