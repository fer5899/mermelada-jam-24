using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrawCard : MonoBehaviour
{
    public Button b1, b2, b3, b4;
    //public Animator respawnTrig;

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

    public void ChangeCardData(Button b)
    {
            TextMeshProUGUI text = b.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "Ataque 5";
            b.transform.localScale = new Vector3(1, 1, 1);
            //respawnTrig.SetTrigger("Respawn");
            b.gameObject.SetActive(true);
    }
    public void DrawOneCard()
    {
        if (CheckActive(b1))
            ChangeCardData(b1);
        if (CheckActive(b2))
            ChangeCardData(b2);
        if (CheckActive(b3))
            ChangeCardData(b3);
        if (CheckActive(b4))
            ChangeCardData(b4);
    }

/*     IEnumerator ScaleUp()
    {
        float duration = 1.0f;
        float start = 0.0f;
        Vector3 initScale = new Vector3(0, 0, 0);
        Vector3 finalScale = new Vector3(1, 1, 1);

        while (start < duration)
        {
            b1.transform.localScale = Vector3.Lerp(initScale, finalScale, start / duration);
            start += Time.deltaTime / duration;
            yield return null;
        }
        b1.transform.localScale = finalScale;
    } */

}
