using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonControles : MonoBehaviour {


    public Image imgControles;
    public Button control;
    Color c;
	// Use this for initialization
    void Start()
    {
        Button btn = control.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
    void TaskOnClick()
    {
        Debug.Log("Control");
        c = imgControles.color;
        c.a = 1f;
        imgControles.color = c;
    }
}
