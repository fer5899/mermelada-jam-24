using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseDiscardWindow : MonoBehaviour
{
    public Button button;
    public GameObject panel;

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        panel.SetActive(false);
    }

}
