using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCientifico : MonoBehaviour {
	public float velocidad;
	[HideInInspector]
	public bool moverse = false;

	//andar
	void Update () {
		if (moverse) {
			transform.Translate (new Vector2 (0f, velocidad));
		}
	}

	//parar
	void OnTriggerEnter2D () {
		this.GetComponent <MovCientifico> ().velocidad = 0;
	}
}
