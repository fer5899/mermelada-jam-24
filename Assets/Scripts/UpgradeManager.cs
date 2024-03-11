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
   
    [System.NonSerialized]
    public UnityEvent OnUpgradeStart;
    [System.NonSerialized]
    public UnityEvent OnUpgradeEnd;

    public void Start()
    {
        StartUpgrade();
    }

    public void OnEnable()
    {
        OnUpgradeStart ??= new UnityEvent();
        OnUpgradeEnd ??= new UnityEvent();
    }

    public void StartUpgrade()
    {
        OnUpgradeStart.Invoke();
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
        CardSO[] poolCardsChoosen = takePoolCards();
        int randomCard1 = UnityEngine.Random.Range(0, poolCardsChoosen.Length);
        int randomCard2;
        do
            randomCard2 = UnityEngine.Random.Range(0, poolCardsChoosen.Length);
        while (randomCard2 == randomCard1);
    
        upgradeCardLoaders[0].LoadCard(poolCardsChoosen[randomCard1]);
        upgradeCardLoaders[1].LoadCard(poolCardsChoosen[randomCard2]);
    }

    private CardSO[] takePoolCards()
    {
        int round = startRound - gameManager.cycle + gameManager.turn;
        int poolChoosen = 0;
        int startCard = 0;
        CardSO[] cardsPool;

        //GET THE RANGE WHERE IS THE POOL OF THE REWARD
        for (int j = 0; j < poolRange.Length; j++)
        {
            for (int i = poolRange[j]; i > 0; i--)
            {
                if (round == 0)
                    poolChoosen = j;
               
                round--;
            }
            if (round <= 0)
                break;
        }

        //ADD SIZE TO POOL ARRAY
        cardsPool = new CardSO[poolSize[poolChoosen]];
        Debug.Log("cards Pool " + poolChoosen);
        //GET THE STARTING ARRAY OF THE POOL
        for (int i = 0; i < poolChoosen; i++)
        {
            for (int j = 0; j < poolSize[i]; j++)
            {
                startCard++;
            }
        }

        Debug.Log("start " + startCard);
        //GET THE CARDS OF THE ARRAY OF CARDS TO INTRODUCE IN THE CARD OF THE POOL
        for (int i = 0; i < cardsPool.Length; i++)
        {
            cardsPool[i] = upgradeProgression[i + startCard];
        }

        return cardsPool;
        
    }

    public void EndUpgrade()
    {
        OnUpgradeEnd.Invoke();
    }

}
