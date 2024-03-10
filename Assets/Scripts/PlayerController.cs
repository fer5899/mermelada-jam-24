using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public IntVariableSO playerHealth;
    public IntVariableSO playerMana;
    public IntVariableSO playerBlock;
    public IntVariableSO playerFury;
    public IntVariableSO playerThorns;
    public IntVariableSO playerWeak;
    public IntVariableSO playerFutureMana;


    public void Start()
    {
        playerHealth.ResetValue();
        playerMana.ResetValue();
        playerBlock.ResetValue();
        playerFury.ResetValue();
        playerThorns.ResetValue();
        playerWeak.ResetValue();
        playerFutureMana.ResetValue();
    }

    public void TakeDamage(int damage)
    {
        playerHealth.AddAmount(-damage);
    }

    public void UseMana(int mana)
    {
        playerMana.AddAmount(-mana);
    }

    public void GainMana(int mana)
    {
        playerMana.AddAmount(mana);
    }

    public void Heal(int health)
    {
        playerHealth.AddAmount(health);
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

    public void GainFutureMana(int futureMana)
    {
        playerFutureMana.AddAmount(futureMana);
    }
}
