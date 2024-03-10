using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public Button button;
    public IntVariableSO manaData;

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        if (manaData.Value <= 0)
            button.interactable = false;
        else
            button.interactable = true;
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
