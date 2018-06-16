using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonEmpezar : MonoBehaviour {

    public Button ThisButton;

	// Use this for initialization
	void Start () {
        Button btn = ThisButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void TaskOnClick () {
        Debug.Log("Boton Pulsado");
        GameManager.instance.Cargar();
		
	}
}
