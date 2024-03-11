using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public GameManagerSO gameManager;
    public IntVariableSO playerHealth;
    public IntVariableSO playerMana;
    public IntVariableSO playerBlock;

    // Statuses
    public IntVariableSO playerFury;
    public IntVariableSO playerThorns;
    public IntVariableSO playerWeak;
    public IntVariableSO playerGainMana;
    public IntVariableSO playerLoseMana;

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
        playerHealth.ResetValue();
        playerMana.ResetValue();
        playerBlock.ResetValue();
        playerFury.ResetValue();
        playerThorns.ResetValue();
        playerWeak.ResetValue();
        playerGainMana.ResetValue();
        playerLoseMana.ResetValue();
    }

    public void TakeDamage(int damage)
    {
        playerHealth.AddAmount(-damage);
        if (playerHealth.Value <= 0)
        {
            // gameManager.EndCombat();
        }
    }

    public void GainMana(int mana)
    {
        playerMana.AddAmount(mana);
    }
    public void LoseMana(int mana)
    {
        playerMana.AddAmount(-mana);
        if (playerMana.Value < 0)
        {
            playerMana.SetValue(0);
        }
    }

    public void Heal(int health)
    {
        playerHealth.AddAmount(health);
        if (playerHealth.Value > playerHealth.DefaultValue)
        {
            playerHealth.SetValue(playerHealth.DefaultValue);
        }
    }

    public void GainBlock(int block)
    {
        playerBlock.AddAmount(block);
    }

    public void GainFury(int fury)
    {
        playerFury.AddAmount(fury);
    }

    public void GainThorns(int thorns)
    {
        playerThorns.AddAmount(thorns);
    }

    public void OnStartTurn(int turn)
    {
        playerMana.ResetValue();
        playerBlock.ResetValue();
        ExecuteStatuses();
    }

    public void ExecuteStatuses()
    {
        if (playerWeak.Value > 0)
        {
            playerWeak.AddAmount(-1);
        }
        if (playerFury.Value > 0)
        {
            playerFury.AddAmount(-1);
        }
        if (playerThorns.Value > 0)
        {
            playerThorns.AddAmount(-1);
        }
        if (playerGainMana.Value > 0)
        {
            playerGainMana.AddAmount(-1);
            GainMana(1);
        }
        if (playerLoseMana.Value > 0)
        {
            playerLoseMana.AddAmount(-1);
            LoseMana(1);
        }
    }

}
