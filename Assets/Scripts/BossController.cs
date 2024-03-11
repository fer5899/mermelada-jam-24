using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Singleton<BossController>
{

    public GameManagerSO gameManager;
    public IntVariableSO bossHealth;
    public IntVariableSO bossBleedStatusCounter;
    public IntVariableSO bossWeakStatusCounter;
    public IntVariableSO bossPoisonStatusCounter;

    public void OnEnable()
    {
        gameManager.OnTurnStart.AddListener(OnStartTurn);
    }

    public void OnDisable()
    {
        gameManager.OnTurnStart.RemoveListener(OnStartTurn);
    }

    public void Start()
    {
        bossHealth.ResetValue();
        bossBleedStatusCounter.ResetValue();
        bossWeakStatusCounter.ResetValue();
        bossPoisonStatusCounter.ResetValue();
    }

    public void TakeDamage(int damage)
    {
        bossHealth.AddAmount(-damage);
        if (bossHealth.Value <= 0)
        {
            // gameManager.EndGame();
        }
    }


    public void Heal(int amount)
    {
        bossHealth.AddAmount(amount);
        if (bossHealth.Value > bossHealth.DefaultValue)
        {
            bossHealth.SetValue(bossHealth.DefaultValue);
        }
    }

    public void GainBleed(int bleed)
    {
        bossBleedStatusCounter.AddAmount(bleed);
    }

    public void GainWeak(int weak)
    {
        bossWeakStatusCounter.AddAmount(weak);
    }

    public void GainPoison(int poison)
    {
        bossPoisonStatusCounter.AddAmount(poison);
    }

    public void StartBossCombat()
    {
        
    }

    public void OnStartTurn(int turn)
    {
        ExecuteStatuses();
    }

    public void ExecuteStatuses()
    {
        if (bossBleedStatusCounter.Value > 0)
        {
            bossBleedStatusCounter.AddAmount(-1);
        }
        if (bossWeakStatusCounter.Value > 0)
        {
            bossWeakStatusCounter.AddAmount(-1);
        }
        if (bossPoisonStatusCounter.Value > 0)
        {
            TakeDamage(bossPoisonStatusCounter.Value);
            bossPoisonStatusCounter.AddAmount(-1);
        }
    }
}
