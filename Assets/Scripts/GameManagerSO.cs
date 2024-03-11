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
        OnEndGame ??= new UnityEvent();
    }

    public void StartCycle()
    {
        OnCycleStart.Invoke(cycle);
    }

    public void EndCycle()
    {
        OnCycleEnd.Invoke(cycle);
        cycle++;
        loadScene("Combat");
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
        UnloadScene("Pablo")
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
        Debug.Log("Fin del juego");
    }

    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    public void UnloadScene(string scene)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void startGame()
    {
        cycle = 0;
        turn = 0;
        loadScene("Combat");
    }
}
