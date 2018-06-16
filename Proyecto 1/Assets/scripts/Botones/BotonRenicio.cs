using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BotonRenicio : MonoBehaviour {

    public Button ThisButton;

    // Use this for initialization
    void Start()
    {
        Button btn = ThisButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        Debug.Log("Reinicio");
        GameManager.instance.Reiniciar();

    }
}
   
