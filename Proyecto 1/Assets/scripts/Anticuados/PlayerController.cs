using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	//Declaración de variables
	public float vidaMaxJug,			//Vida máxima que tiene el personaje del jugador
	vidaActualJug,						//Vida actual que tiene el personaje del jugador
	vidaRegenera,						//Cantidad de vida que regenera
	tiempoInicioRegen,					//Tiempo que tarda en iniciarse la regeneración de la vida del jugador
	tiempoRegen;						//Cada cuanto tiempo se regenera la vida del jugador

	//Métodos
	void Update () {
		Vida ();
	}
	//Vida del jugador 
	void Vida () {
		if (vidaActualJug > vidaMaxJug)
			vidaActualJug = vidaMaxJug;
		
		else if (vidaMaxJug < 1)
			vidaMaxJug = 1;
		
		else if (vidaActualJug <= 0) {
			Debug.Log ("Has muerto.");
			gameObject.SendMessage ("Muere", SendMessageOptions.DontRequireReceiver);
		}
	}
	//Pérdida de vida al recibir un ataque de un enemigo
	void pierdeVida (float daño) {
		vidaActualJug -= daño;
		Debug.Log ("Jugador ha perdido vida. Vida actual: " + vidaActualJug);
		CancelInvoke ();
		InvokeRepeating ("Regenera", tiempoInicioRegen, tiempoRegen); 
	}
	//Regeneración de la vida del jugador con el tiempo
	void Regenera () {
		Debug.Log ("Tiempo: " + Time.time);
		if (vidaActualJug < vidaMaxJug) {
			vidaActualJug += vidaRegenera * Time.deltaTime;
			Debug.Log ("Vida jugador regenerada. Vida actual: " + vidaActualJug);
		}
		Debug.Log ("Tiempo: " + Time.time);
	}

}