using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTimeEnd : MonoBehaviour
{
    public GameManagerSO gameManager;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(gameManager.MyCoroutine("Menú"));
    }
}
