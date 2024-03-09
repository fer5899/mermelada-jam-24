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


    public void Start()
    {
        playerHealth.ResetValue();
        playerMana.ResetValue();
        playerBlock.ResetValue();
        playerFury.ResetValue();
        playerThorns.ResetValue();
        playerWeak.ResetValue();
    }

    public void TakeDamage(int damage)
    {
        playerHealth.ChangeValue(-damage);
    }

    public void UseMana(int mana)
    {
        playerMana.ChangeValue(-mana);
    }

    public void GainMana(int mana)
    {
        playerMana.ChangeValue(mana);
    }

    public void Heal(int health)
    {
        playerHealth.ChangeValue(health);
    }

    public void GainBlock(int block)
    {
        playerBlock.ChangeValue(block);
    }

    public void GainFury(int fury)
    {
        playerFury.ChangeValue(fury);
    }

    public void GainThorns(int thorns)
    {
        playerThorns.ChangeValue(thorns);
    }
}
