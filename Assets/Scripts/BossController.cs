using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Singleton<BossController>
{
    public IntVariableSO bossHealth;
    public IntVariableSO bossBleed;
    public IntVariableSO bossWeak;
    public IntVariableSO bossPoison;

    public void Start()
    {
        bossHealth.ResetValue();
        bossBleed.ResetValue();
        bossWeak.ResetValue();
        bossPoison.ResetValue();
    }

    public void TakeDamage(int damage)
    {
        bossHealth.ChangeValue(-damage);
    }

    public void Heal(int amount)
    {
        bossHealth.ChangeValue(amount);
    }

    public void GainBleed(int bleed)
    {
        bossBleed.ChangeValue(bleed);
    }

    public void GainWeak(int weak)
    {
        bossWeak.ChangeValue(weak);
    }

    public void GainPoison(int poison)
    {
        bossPoison.ChangeValue(poison);
    }
}
