using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//configura todos sus hijos para ser deathzones
public class DeathFather : MonoBehaviour {

    public bool pinchos;

    private void Awake() {

        foreach (Transform child in transform) {

            child.gameObject.AddComponent<DeathZone>();
            child.gameObject.GetComponent<Collider2D>().isTrigger = true;
            if (pinchos) child.gameObject.AddComponent<DeathZone>().pinchos = true;

        }
    }

}
