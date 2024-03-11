using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TextMeshProUGUI bossAttackText;

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
        bossAttackText.text = "El Profundo usó Garra Umbría\n(16 de daño)";
        DealDamage(16);
    }

    public void Embestida()
    {
        bossAttackText.text = "El Profundo usó Embestida\n(10 de daño, 1 de debilidad)";
        DealDamage(10);
        PlayerController.Instance.GainWeak(2);
    }

    public void Reposo()
    {
        bossAttackText.text = "El Profundo te observa amenazante";
    }

    public void Rencor()
    {
        bossAttackText.text = "El Profundo usó Rencor\n(6 de daño, 1 de maná perdido)";
        DealDamage(6);
        PlayerController.Instance.GainLoseManaStatus(1);
    }

    public void Carga()
    {
        bossAttackText.text = "El Profundo se prepara para atacar";
        DealDamage(20);
    }

    public void RayoOscuro()
    {
        bossAttackText.text = "El Profundo usó Rayo Oscuro\n(25 de daño)";
        DealDamage(25);
    }

    public void GritoAterrador()
    {
        bossAttackText.text = "El Profundo usó Grito Aterrador\n(3 de debilidad)";
        PlayerController.Instance.GainWeak(4);
    }

    public void Curacion()
    {
        bossAttackText.text = "El Profundo usó Curación\n(15 de curación)";
        Heal(15);
    }

    public void Requiem()
    {
        bossAttackText.text = "El Profundo usó Requiem\n(10000 de daño)";
        DealDamage(10000);
    }


}
