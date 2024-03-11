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
        playerDeck = defaultPlayerDeck;
    }

    public void StartCycle()
    {
        OnCycleStart.Invoke(cycle);
        turn = 0;
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
    }

    public void StartCombat()
    {
        OnCombatStart.Invoke();
    }

    public void EndCombat()
    {
        OnCombatEnd.Invoke();
        loadScene("PabloScene");
    }

    public void StartUpgrade()
    {
        OnUpgradeStart.Invoke();
    }

    public void EndUpgrade()
    {
        OnUpgradeEnd.Invoke();
    }

    public void EndGame()
    {
        OnEndGame.Invoke();
        cycle = 0;
        loadScene("ImagenFinal1");
    }

    public void startGame()
    {
        cycle = 0;
        loadScene("ImagenInicio");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public IEnumerator MyCoroutine(string scene)
    {
        yield return new WaitForSeconds(4f);
        loadScene(scene);
    }

}
