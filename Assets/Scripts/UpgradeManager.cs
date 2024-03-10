using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public CardSO[] upgradeProgression;
    public GameManagerSO gameManager;
    public CardLoader[] upgradeCardLoaders;
    public int upgradeCycleStart;


    [System.NonSerialized]
    public UnityEvent OnUpgradeStart;
    [System.NonSerialized]
    public UnityEvent OnUpgradeEnd;


    public void OnEnable()
    {
        OnUpgradeStart ??= new UnityEvent();
        OnUpgradeEnd ??= new UnityEvent();
    }

    public void StartUpgrade()
    {
        OnUpgradeStart.Invoke();

    }

    public void EndUpgrade()
    {
        OnUpgradeEnd.Invoke();
    }
}
