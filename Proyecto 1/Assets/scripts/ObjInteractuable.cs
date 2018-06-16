using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjInteractuable : MonoBehaviour {
	//cuando se puede interactuar con el objeto
	void OnTriggerEnter2D () {
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
	}

	//cuando no se puede interactuar con el objeto porque se está demasiado lejos
	void OnTriggerExit2D () {
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	}
}