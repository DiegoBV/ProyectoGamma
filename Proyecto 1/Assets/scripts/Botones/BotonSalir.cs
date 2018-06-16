using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BotonSalir : MonoBehaviour {

    public Button ThisButton;
    /// Use this for initialization
    void Start()
    {
        Button btn = ThisButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        Debug.Log("Saliendo");
        GameManager.instance.Salir();

    }
}
