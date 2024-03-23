using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishTurnButton : MonoBehaviour
{

    public GameManagerSO gameManager;

    public void OnEnable()
    {
        gameManager.OnPlayerDeath.AddListener(DisableInteractivity);
    }

    public void OnDisable()
    {
        gameManager.OnPlayerDeath.RemoveListener(DisableInteractivity);
    }

    public void Start()
    {
        GetComponent<UnityEngine.UI.Button>().interactable = true;
    }
    
    public void DisableInteractivity()
    {
        GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}
