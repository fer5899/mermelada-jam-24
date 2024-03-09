using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        Debug.Log("El boton " + button.name + " ha sido clicado");
    }

    public void DeactivateObject()
    {
        button.gameObject.SetActive(false);
    }
}
