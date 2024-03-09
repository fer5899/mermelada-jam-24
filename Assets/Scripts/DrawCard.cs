using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrawCard : MonoBehaviour
{
    public Button b1, b2, b3, b4;
    void Start()
    {
    }

    void Update()
    {
        
    }
    
    bool CheckActive(Button b)
    {
        if (!b.gameObject.activeInHierarchy)
            return true;
        else
            return false;
    }

    public void DrawOneCard()
    {
        if (CheckActive(b1))
        {
            Debug.Log("En DrawCard");
            TextMeshProUGUI text = b1.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "Ataque 5";
            b1.transform.localScale = new Vector3(1, 1, 1);
            b1.gameObject.SetActive(true);
        }
        if (CheckActive(b2))
        {
            Debug.Log("En DrawCard");
            TextMeshProUGUI text = b2.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "Ataque 5";
            b2.gameObject.SetActive(true);
        }
    }

}
