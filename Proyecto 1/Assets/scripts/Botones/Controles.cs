using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controles : MonoBehaviour {

    Color c;

        void Start(){
        c = this.gameObject.GetComponent<Image>().color;
        c.a = 0;
        this.gameObject.GetComponent<Image>().color = c;
        }
} 

