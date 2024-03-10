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
        bossHealth.AddAmount(-damage);
    }

    public void Heal(int amount)
    {
        bossHealth.AddAmount(amount);
    }

    public void GainBleed(int bleed)
    {
        bossBleed.AddAmount(bleed);
    }

    public void GainWeak(int weak)
    {
        bossWeak.AddAmount(weak);
    }

    public void GainPoison(int poison)
    {
        bossPoison.AddAmount(poison);
    }
}
