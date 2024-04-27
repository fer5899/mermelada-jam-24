using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManagerSO : ScriptableObject
{
    public int cycle = 0;
    public int turn = 0;
    public CardSO[] playerDeck;

    public CardSO[] defaultPlayerDeck;

    public IntVariableSO[] resetVariables;

    [System.NonSerialized]
    public UnityEvent<int> OnCycleStart;
    [System.NonSerialized]
    public UnityEvent<int> OnCycleEnd;
    [System.NonSerialized]
    public UnityEvent<int> OnTurnStart;
    [System.NonSerialized]
    public UnityEvent<int> OnTurnEnd;
    [System.NonSerialized]
    public UnityEvent OnCombatStart;
    [System.NonSerialized]
    public UnityEvent OnCombatEnd;
    [System.NonSerialized]
    public UnityEvent OnUpgradeStart;
    [System.NonSerialized]
    public UnityEvent OnUpgradeEnd;
    [System.NonSerialized]
    public UnityEvent OnEndGame;
    [System.NonSerialized]
    public UnityEvent OnPlayerDeath;
    [System.NonSerialized]
    public UnityEvent OnBossDeath;
    [System.NonSerialized]
    public UnityEvent onPlayerAttack;
    [System.NonSerialized]
    public UnityEvent<string> onBossAttack;
    [System.NonSerialized]
    public UnityEvent OnBossPoison;
    [System.NonSerialized]
    public UnityEvent OnBossBleed;
    [System.NonSerialized]
    public UnityEvent OnBossWeak;
    [System.NonSerialized]
    public UnityEvent OnPlayerBlock;
    [System.NonSerialized]
    public UnityEvent onPlayerGetDamage;
    [System.NonSerialized]
    public UnityEvent onPlayerGainFury;
    [System.NonSerialized]
    public UnityEvent onPlayerGainHealth;
    [System.NonSerialized]
    public UnityEvent onPlayerGainWeak;
    [System.NonSerialized]
    public UnityEvent onPlayerGainThorns;
    [System.NonSerialized]
    public UnityEvent onPlayerGainMana;
    [System.NonSerialized]
    public UnityEvent onDiscardCard;
    [System.NonSerialized]
    public UnityEvent onPlayerGainBlock;
    [System.NonSerialized]
    public UnityEvent onSelectCard;
    [System.NonSerialized]
    public UnityEvent onButtonSelect;

    public void OnEnable()
    {
        OnCycleStart ??= new UnityEvent<int>();
        OnCycleEnd ??= new UnityEvent<int>();
        OnTurnStart ??= new UnityEvent<int>();
        OnTurnEnd ??= new UnityEvent<int>();
        OnCombatEnd ??= new UnityEvent();
        OnCombatStart ??= new UnityEvent();
        OnUpgradeStart ??= new UnityEvent();
        OnUpgradeEnd ??= new UnityEvent();
        OnEndGame ??= new UnityEvent();
        OnUpgradeStart ??= new UnityEvent();
        OnUpgradeEnd ??= new UnityEvent();
        OnPlayerDeath ??= new UnityEvent();
        OnBossDeath ??= new UnityEvent();
        onBossAttack ??= new UnityEvent<string>();
        onPlayerAttack ??= new UnityEvent();
        OnPlayerBlock ??= new UnityEvent();
        onPlayerGetDamage ??= new UnityEvent();
        onDiscardCard ??= new UnityEvent();
        onPlayerGainBlock ??= new UnityEvent();
        onPlayerGainFury ??= new UnityEvent();
        onPlayerGainHealth ??= new UnityEvent();
        onPlayerGainThorns ??= new UnityEvent();
        onPlayerGainMana ??= new UnityEvent();
        onPlayerGainWeak ??= new UnityEvent();
        onSelectCard ??= new UnityEvent();
        onButtonSelect ??= new UnityEvent();
        OnBossPoison ??= new UnityEvent();
        OnBossBleed ??= new UnityEvent();
        OnBossWeak ??= new UnityEvent();

        // Copy all the default cards to the player deck
        playerDeck = new CardSO[defaultPlayerDeck.Length];
        for (int i = 0; i < defaultPlayerDeck.Length; i++)
        {
            playerDeck[i] = defaultPlayerDeck[i];
        }
    }

    public void StartCycle()
    {
        turn = 0;
        OnCycleStart.Invoke(cycle);
    }

    public void EndCycle()
    {
        OnCycleEnd.Invoke(cycle);
        cycle++;
        loadScene("ImagenInicio");
    }

    public void StartTurn()
    {
        OnTurnStart.Invoke(turn);
    }

    public void EndTurn()
    {
        OnTurnEnd.Invoke(turn);
        turn++;
        StartTurn();
    }

    public void PlayerGainFury()
    {
        onPlayerGainFury.Invoke();
    }

    public void PlayerHealth()
    {
        onPlayerGainHealth.Invoke();
    }

    public void PlayerGainThorns()
    {
        onPlayerGainThorns.Invoke();
    }

    public void PlayerGainMana()
    {
        onPlayerGainMana.Invoke();
    }

    public void PlayerGainWeak()
    {
        onPlayerGainWeak.Invoke();
    }

    public void BossPoison()
    {
        OnBossPoison.Invoke();
    }

    public void BossGainBleed()
    {
        OnBossBleed.Invoke();
    }

    public void BossGainWeak()
    {
        OnBossWeak.Invoke();
    }

    public void DiscardCard()
    {
        onDiscardCard.Invoke();
    }

    public void StartCombat()
    {
        OnCombatStart.Invoke();
        ResetVariables();
    }

    public void EndCombat()
    {
        OnCombatEnd.Invoke();
        loadScene("ImagenMuertePlayer");
    }

    public void StartUpgrade()
    {
        OnUpgradeStart.Invoke();
    }

    public void PlayerGainBlock()
    {
        onPlayerGainBlock.Invoke();
    }

    public void SelectCard()
    {
        onSelectCard.Invoke();
    }

    public void EndUpgrade()
    {
        OnUpgradeEnd.Invoke();
    }

    public void ButtonSelect()
    {
        onButtonSelect.Invoke();
    }

    public void EndGame()
    {
        ButtonSelect();
        OnEndGame.Invoke();
        cycle = 0;
        loadScene("ImagenFinal1");
    }

    public void StartGame()
    {
        cycle = 0;
        turn = 0;
        ButtonSelect();
        ResetDeck();
        loadScene("ImagenInicio");
    }

    public void OpenCredits()
    {
        ButtonSelect();
        loadScene("Credits");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public IEnumerator WaitAndLoadScene(string scene, float time)
    {
        yield return new WaitForSeconds(time);
        loadScene(scene);
    }

    public void ResetDeck()
    {
        playerDeck = new CardSO[defaultPlayerDeck.Length];
        for (int i = 0; i < defaultPlayerDeck.Length; i++)
        {
            playerDeck[i] = defaultPlayerDeck[i];
        }
    }

    public void ResetVariables()
    {
        for (int i = 0; i < resetVariables.Length; i++)
        {
            resetVariables[i].ResetValue();
        }
    }

    public void PlayerDies()
    {
        OnPlayerDeath.Invoke();
    }

    public void BossDies()
    {
        OnBossDeath.Invoke();
    }

    public void BossAttack(string attackInfo)
    {
        onBossAttack.Invoke(attackInfo);
    }

    public void PlayerGetDamage()
    {
        onPlayerGetDamage.Invoke();
    }

    public void PlayerAttack()
    {
        onPlayerAttack.Invoke();
    }

    public void PlayerBlock()
    {
        OnPlayerBlock.Invoke();
    }

}
