using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public BattleSystem battleSystem;
    public SkillCheckSystem skillCheckSystem;

    public void Attack1()
    {
        // Area lebar, 1x Skill Check, damage 10
        skillCheckSystem.StartSkillCheck(100f, 1, 10, battleSystem.ApplyDamageToEnemy);
    }

    public void Attack2()
    {
        // Area lebih kecil, 1x Skill Check, damage 20
        skillCheckSystem.StartSkillCheck(80f, 1, 20, battleSystem.ApplyDamageToEnemy);
    }

    public void Attack3()
    {
        // Area lebih kecil lagi, 1x Skill Check, damage 30
        skillCheckSystem.StartSkillCheck(60f, 1, 30, battleSystem.ApplyDamageToEnemy);
    }

    public void Attack4()
    {
        // Area lebar, 2x Skill Check, damage 40
        skillCheckSystem.StartSkillCheck(100f, 2, 40, battleSystem.ApplyDamageToEnemy);
    }

    public void Attack5()
    {
        // Area kecil, 2x Skill Check, damage 50
        skillCheckSystem.StartSkillCheck(60f, 2, 50, battleSystem.ApplyDamageToEnemy);
    }

    public void Attack6()
    {
        // Area sangat kecil, 3x Skill Check, damage 60
        skillCheckSystem.StartSkillCheck(40f, 3, 60, battleSystem.ApplyDamageToEnemy);
    }
}