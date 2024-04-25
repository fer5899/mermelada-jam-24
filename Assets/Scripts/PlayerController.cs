using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Singleton<PlayerController>
{
    public GameManagerSO gameManager;
    public IntVariableSO playerHealth;
    public IntVariableSO playerMana;
    public IntVariableSO playerBlock;

    // Statuses
    public IntVariableSO playerFuryStatusCounter;
    public IntVariableSO playerThornsStatusCounter;
    public IntVariableSO playerWeakStatusCounter;
    public IntVariableSO playerGainManaStatusCounter;
    public IntVariableSO playerLoseManaStatusCounter;
    public SpriteRenderer playerImg;
    public Animator      playerAnimator;


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
        playerFuryStatusCounter.ResetValue();
        playerThornsStatusCounter.ResetValue();
        playerWeakStatusCounter.ResetValue();
        playerGainManaStatusCounter.ResetValue();
        playerLoseManaStatusCounter.ResetValue();
    }

    public void TakeDamage(int damage)
    {
        gameManager.PlayerGetDamage();
        playerHealth.AddAmount(-damage);
        if (playerHealth.Value <= 0)
        {
            playerAnimator.SetBool("DeadBool", true);
            gameManager.PlayerDies();
            StartCoroutine(WaitAfterDeath());
        }
        StartCoroutine(FeedbackDamaged());
    }

    public IEnumerator WaitAfterDeath()
    {
        yield return new WaitForSeconds(4);
        gameManager.EndCombat();
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
        StartCoroutine(CureRutine());
    }

    public void GainBlock(int block)
    {
        Debug.Log("Se ejectua el escudo");
        gameManager.PlayerGainBlock();
        playerBlock.AddAmount(block);
    }

    public void GainFury(int fury)
    {
        playerFuryStatusCounter.AddAmount(fury);
        Debug.Log("Se ejecuta la furia");
        gameManager.PlayerGainFury();
    }

    public void GainThorns(int thorns)
    {
        gameManager.PlayerGainFury();
        playerThornsStatusCounter.AddAmount(thorns);
    }

    public void GainWeak(int weak)
    {
        playerWeakStatusCounter.AddAmount(weak);
    }

    public void GainLoseManaStatus(int amount)
    {
        playerLoseManaStatusCounter.AddAmount(amount);
    }

    public void OnStartTurn(int turn)
    {
        Debug.Log("Player turn started, mana reset");
        playerMana.ResetValue();
        playerBlock.ResetValue();
        ExecuteStatuses();
    }

    public void ExecuteStatuses()
    {
        if (playerWeakStatusCounter.Value > 0)
        {
            playerWeakStatusCounter.AddAmount(-1);
        }
        if (playerFuryStatusCounter.Value > 0)
        {
            playerFuryStatusCounter.AddAmount(-1);
        }
        if (playerThornsStatusCounter.Value > 0)
        {
            playerThornsStatusCounter.AddAmount(-1);
        }
        if (playerGainManaStatusCounter.Value > 0)
        {
            playerGainManaStatusCounter.AddAmount(-1);
            GainMana(1);
        }
        if (playerLoseManaStatusCounter.Value > 0)
        {
            playerLoseManaStatusCounter.AddAmount(-1);
            LoseMana(1);
        }
    }
    
    private IEnumerator FeedbackDamaged()
    {
        yield return new WaitForSeconds(0.1f);
        TurnRed();
        yield return new WaitForSeconds(0.2f);
        TurnWhite();
    }

    private IEnumerator CureRutine()
    {
        yield return new WaitForSeconds(0.1f);
        TurnGreen();
        yield return new WaitForSeconds(0.2f);
        TurnWhite();
    }

    private void TurnWhite()
    {
        playerImg.color = Color.white;
    }

    private void TurnRed()
    {
        playerImg.color = Color.red;
    }
        private void TurnGreen()
    {
        playerImg.color = Color.green;
    }
}
