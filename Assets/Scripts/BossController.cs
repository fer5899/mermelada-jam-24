using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Singleton<BossController>
{

    public enum BossAttack {
        garraUmbria,
        embestida,
        reposo,
        rencor,
        carga,
        rayoOscuro,
        gritoAterrador,
        curacion,
        requiem,
    }

    public GameManagerSO gameManager;
    public IntVariableSO bossHealth;
    public IntVariableSO bossBleedStatusCounter;
    public IntVariableSO bossWeakStatusCounter;
    public IntVariableSO bossPoisonStatusCounter;

    public BossAttack[] bossAttacks = new BossAttack[] {
        BossAttack.garraUmbria,
        BossAttack.embestida,
        BossAttack.reposo,
        BossAttack.garraUmbria,
        BossAttack.rencor,
        BossAttack.gritoAterrador,
        BossAttack.garraUmbria,
        BossAttack.rencor,
        BossAttack.reposo,
        BossAttack.curacion,
        BossAttack.garraUmbria,
        BossAttack.embestida,
        BossAttack.gritoAterrador,
        BossAttack.carga,
        BossAttack.rayoOscuro,
        BossAttack.carga,
        BossAttack.requiem,
    };

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
            gameManager.EndGame();
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
        switch (bossAttacks[gameManager.turn])
        {
            case BossAttack.garraUmbria:
                GarraUmbria();
                break;
            case BossAttack.embestida:
                Embestida();
                break;
            case BossAttack.reposo:
                Reposo();
                break;
            case BossAttack.rencor:
                Rencor();
                break;
            case BossAttack.carga:
                Carga();
                break;
            case BossAttack.rayoOscuro:
                RayoOscuro();
                break;
            case BossAttack.gritoAterrador:
                GritoAterrador();
                break;
            case BossAttack.curacion:
                Curacion();
                break;
            case BossAttack.requiem:
                Requiem();
                break;
        }
        gameManager.EndTurn();
    }

    public void DealDamage(int damage)
    {
        float modifiedDamage = damage;
        int bossDamageOutput;
        int finalDamage;

        // Apply weak status multiplier
        if (bossWeakStatusCounter.Value > 0)
        {
            modifiedDamage *= 0.8f;
        }

        bossDamageOutput = (int)Mathf.Ceil(modifiedDamage);

        finalDamage = bossDamageOutput - PlayerController.Instance.playerBlock.Value;

        if (finalDamage > 0)
        {
            PlayerController.Instance.TakeDamage(finalDamage);
        }

        // Take thorns damage if active
        if (PlayerController.Instance.playerThornsStatusCounter.Value > 0)
        {
            TakeDamage(PlayerController.Instance.playerThornsStatusCounter.Value);
        }

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

    public void GarraUmbria()
    {
        Debug.Log("Boss used Garra Umbria");
        DealDamage(16);
    }

    public void Embestida()
    {
        Debug.Log("Boss used Embestida");
        DealDamage(10);
        PlayerController.Instance.GainWeak(1);
    }

    public void Reposo()
    {
        Debug.Log("Boss used Reposo");
    }

    public void Rencor()
    {
        Debug.Log("Boss used Rencor");
        DealDamage(6);
        PlayerController.Instance.GainLoseManaStatus(1);
    }

    public void Carga()
    {
        Debug.Log("Boss used Carga ataque");
    }

    public void RayoOscuro()
    {
        Debug.Log("Boss used Rayo Oscuro");
        DealDamage(25);
    }

    public void GritoAterrador()
    {
        Debug.Log("Boss used Grito Aterrador");
        PlayerController.Instance.GainWeak(3);
    }

    public void Curacion()
    {
        Debug.Log("Boss used Curacion");
        Heal(15);
    }

    public void Requiem()
    {
        Debug.Log("Boss used Requiem");
        DealDamage(10000);
    }


}
