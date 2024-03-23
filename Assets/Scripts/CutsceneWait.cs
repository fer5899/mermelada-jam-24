using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneWait : MonoBehaviour
{
    public GameManagerSO gameManager;
    public string sceneToLoad;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(gameManager.WaitAndLoadScene(sceneToLoad, waitTime));
    }
}
