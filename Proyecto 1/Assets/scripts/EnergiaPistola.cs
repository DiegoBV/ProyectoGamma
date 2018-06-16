using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergiaPistola : MonoBehaviour {
	//Declaración de variables
	public float energiaMax,				//Energía máxima de la pistola
	energiaActual,							//Energía actual de la pistola
	energiaPerdida,							//Cantidad de energía que se pierde al realizar un disparo
	energiaRegeneracion,					//Cantidad de energía que se regenera
	tiempoInicioRegen,						//Tiempo que debe transcurrir para que se inicie la recarga de energía
	tiempoRegen;							//Cada cuánto tiempo se regenera la energía
	
    /*
	GameManager obj;

	//Métodos
	void Start () {
		obj = GameManager.instance;
		energiaActual = obj.energia;
        InvokeRepeating("recuperaEnergia",tiempoInicioRegen,tiempoRegen);
    }

    /*
	void Update () {
		//ajusteEnergia ();
	}
	//Ajuste de la energía de la pistola para que no tome valores fuera del rango 0-energiaMax
	void ajusteEnergia () {
		if (energiaActual < 0)
			energiaActual = 0;
		
		else if (energiaActual > energiaMax)
			energiaActual = energiaMax;
		
		else if (energiaMax < 1)
			energiaMax = 1;
	}
	//Pérdida de energía cuando se dispara la pistola
	void RecuEnergy () {
		energiaActual -= energiaPerdida;
		Debug.Log ("Pérdida. Energía actual: " + energiaActual);
		Debug.Log ("Tiempo: " + Time.time);
		//se "invoca" al método de recuperación de energía tras un tiempo determinado
		CancelInvoke ();
		InvokeRepeating ("recuperaEnergia", tiempoInicioRegen, tiempoRegen);
	}
    */

	//Recuperación de energía con el tiempo
	void recuperaEnergia () {
        //Debug.Log ("Tiempo: " + Time.time);
        if (GameManager.instance.energia < energiaMax) {
            GameManager.instance.VariacionEnergia((int)energiaRegeneracion);
            //Debug.Log ("Recuperación. Energía actual: " + energiaActual);
        }
        else CancelInvoke();
	}

    public void RecuperaInc() {
        InvokeRepeating("recuperaEnergia",tiempoInicioRegen,tiempoRegen);
    }

    public void Shoot() {
        CancelInvoke(); //si disparas se cancela la recarga
    }

}