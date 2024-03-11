using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverScript : MonoBehaviour
{
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateObject()
    {
        img.gameObject.SetActive(true);
    }
    public void DeactivateObject()
    {
        img.gameObject.SetActive(false);
    }
}
