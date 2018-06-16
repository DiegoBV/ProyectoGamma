using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DeathZone))]
[RequireComponent(typeof(BoxCollider2D))]

//puente activable o deathzone
public class Puente : MonoBehaviour {

    public bool activado; //puente activado default
    SpriteRenderer sprite;
    Collider2D hitbox;
    DeathZone dz;

    // Use this for initialization
    void Start() {

        //config inicial
        hitbox = GetComponent<Collider2D>();
        hitbox.isTrigger = true;
        sprite = GetComponent<SpriteRenderer>();

        hitbox.enabled = !activado;
        sprite.enabled = activado;
    }

    void Activate() { //metodos externos
        Debug.Log("Activado");
        hitbox.enabled = activado;
        sprite.enabled = !activado;
    }

    void Desactivate() {

        hitbox.enabled = !activado;
        sprite.enabled = activado;
    }
}
