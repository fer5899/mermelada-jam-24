using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class UpgradeManager : Singleton<UpgradeManager>
{

    public GameManagerSO gameManager;
    public GameObject organizer;
    public GameObject prefab;
    public int startRound;
    public int[] poolSize;
    public int[] poolRange;
    public CardSO[] upgradeProgression;
    public CardLoader[] upgradeCardLoaders;

    private CardSO selectedUpgradeCard;
    private CardSO selectedDeckCard;

    public GameObject continueButton;


    public void Start()
    {
        gameManager.StartUpgrade();
    }

    public void OnEnable()
    {
        gameManager.OnUpgradeStart.AddListener(StartUpgrade);
        gameManager.OnUpgradeEnd.AddListener(EndUpgrade);
    }

    public void OnDisable()
    {
        gameManager.OnUpgradeStart.RemoveListener(StartUpgrade);
        gameManager.OnUpgradeEnd.RemoveListener(EndUpgrade);
    }

    public void StartUpgrade()
    {
        getRewardCards();
        getHandOfPlayer();
    }

    private void getHandOfPlayer()
    {
        for (int i = 0; i < gameManager.playerDeck.Length; i++)
        {   
            GameObject newChild = Instantiate(prefab, organizer.transform);
            CardLoader cardDeck = newChild.GetComponent<CardLoader>();
            cardDeck.LoadCard(gameManager.playerDeck[i]);
        }
    }

    private void getRewardCards()
    {
        List<CardSO> pool = takePoolCards();
        if (pool.Count <= 0)
            return ;
        int randomCard1 = UnityEngine.Random.Range(0, pool.Count);
        int randomCard2;
        do
            randomCard2 = UnityEngine.Random.Range(0, pool.Count);
        while (randomCard2 == randomCard1);
    
        upgradeCardLoaders[0].LoadCard(pool[randomCard1]);
        upgradeCardLoaders[1].LoadCard(pool[randomCard2]);
    }

    private List<CardSO> takePoolCards()
    {
        int round = startRound - gameManager.cycle + gameManager.turn;
        int poolChoosen = 0;
        int poolBehind = 0;
        int start = 0;
        bool exit = false;
        List<CardSO> pool = new List<CardSO>();

        if (round < 0)
            round = 0;
        
        Debug.Log("Round = " + round);
        //FIND THE POOL
        for (int j = 0; j < poolRange.Length; j++)
        {
            for (int i = start; i <= poolRange[j]; i++)
            {
                if (round == i)
                {
                    poolChoosen = j;
                    if (j != 0)
                        poolBehind = j - 1;
                    exit = true;
                    break;
                }
                start++;
            }
            if (exit)
                break;
        }

        Debug.Log("Pool Choosen = " + poolChoosen);
        Debug.Log("Pool Behind = " + poolBehind);
        if (poolChoosen > poolRange.Length)
            return null;
        //FIND STARTING CARD
        if (poolBehind > 0)
        {
            poolBehind = poolSize[poolBehind];
        }
        poolChoosen = poolSize[poolChoosen];

        //ADD CARDS TO THE POOL
        for (int i = poolBehind + 1; i <= poolChoosen; i++)
        {
            pool.Add(upgradeProgression[i]);
        }

        return pool;
        
    }

    public void EndUpgrade()
    {
        gameManager.EndCycle();    
    }

    public void SetAllDeckAsDeselected()
    {
        for (int i = 0; i < organizer.transform.childCount; i++)
        {
            organizer.transform.GetChild(i).GetComponent<CardLoader>().SetCardAsDeselected();
        }
    }

    public void SetAllUpgradeAsDeselected()
    {
        for (int i = 0; i < upgradeCardLoaders.Length; i++)
        {
            upgradeCardLoaders[i].SetCardAsDeselected();
        }
    }

    public void SetSelectedUpgradeCard(CardSO card)
    {
        selectedUpgradeCard = card;
    }

    public void SetSelectedDeckCard(CardSO card)
    {
        selectedDeckCard = card;
    }

    public void Update()
    {
        if (selectedDeckCard != null && selectedUpgradeCard != null)
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void UpgradeCard()
    {
        gameManager.playerDeck[Array.IndexOf(gameManager.playerDeck, selectedDeckCard)] = selectedUpgradeCard;
        gameManager.EndUpgrade();
    }

}
