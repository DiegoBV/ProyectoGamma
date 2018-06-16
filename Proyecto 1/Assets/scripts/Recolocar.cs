using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolocar : MonoBehaviour {
    //Declaración de variables
    public Transform posicionActual;        //Posición actual de la caja
    Vector3 posicionInicial;		//Posición inicial de la caja
    [HideInInspector]
    public bool relocatable = true;

    //MÉTODOS
    void Start() {
        //se guarda la posición inicial de la caja
        posicionInicial = posicionActual.position;
        //Debug.Log ("Posición inicial: " + posicionInicial);
    }
    //Volver a colocar la caja en su posición inicial
    void Relocate() {
        if (relocatable) {
            posicionActual.position = posicionInicial;
            transform.parent = null;
            Debug.Log("Posición inicial: " + gameObject.transform.position);
        }
    }

    void RelocateOP() {
        relocatable = true;
        Relocate();
    }
}
