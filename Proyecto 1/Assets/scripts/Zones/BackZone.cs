using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cambia al objeto que entra de layer para que "parezca" por detras
public class BackZone : MonoBehaviour {

    [HideInInspector]
    public int backLayer; //la layer escogida
    [HideInInspector] public int frontLayer;

    void OnTriggerEnter2D(Collider2D other) {

        SpriteRenderer sprite = other.GetComponent<SpriteRenderer>();
        PlayerMovement player = other.GetComponent<PlayerMovement>(); //para separar de capa jugador

        if (other.GetComponent<PlayerMovement>() != null) {
            player.gameObject.GetComponent<SpriteRenderer>().sortingOrder = backLayer;

            //animaciones de disparar van separadas
            player.DisparoAb.GetComponent<SpriteRenderer>().sortingOrder = backLayer;
            player.DisparoDer.GetComponent<SpriteRenderer>().sortingOrder = backLayer;
            player.DisparoArr.GetComponent<SpriteRenderer>().sortingOrder = backLayer;
            player.DisparoIzq.GetComponent<SpriteRenderer>().sortingOrder = backLayer;
        }
        else if (other.GetComponent<BalaController>() != null || other.GetComponent<AutoBala>() != null)
            sprite.sortingOrder = backLayer;
        else if (other.GetComponent<Empujar>() != null) sprite.sortingOrder = backLayer;
        else if (other.GetComponent<Enemigo>() != null) sprite.sortingOrder = backLayer;
    }

    void OnTriggerExit2D(Collider2D other) {
        //Debug.Log("salido de detras");

        SpriteRenderer sprite = other.GetComponent<SpriteRenderer>();
        PlayerMovement player = other.GetComponent<PlayerMovement>(); //para separar de capa jugador

        if (player != null) {
            player.gameObject.GetComponent<SpriteRenderer>().sortingOrder = frontLayer;

            //animaciones de disparar van separadas
            player.DisparoAb.GetComponent<SpriteRenderer>().sortingOrder = frontLayer;
            player.DisparoDer.GetComponent<SpriteRenderer>().sortingOrder = frontLayer;
            player.DisparoArr.GetComponent<SpriteRenderer>().sortingOrder = frontLayer;
            player.DisparoIzq.GetComponent<SpriteRenderer>().sortingOrder = frontLayer;

        }
        else if (other.GetComponent<BalaController>() != null || other.GetComponent<AutoBala>() != null)
            sprite.sortingOrder = frontLayer;
        else if (other.GetComponent<Empujar>() != null) sprite.sortingOrder = frontLayer;
        else if (other.GetComponent<Enemigo>() != null) sprite.sortingOrder = frontLayer;
    }

}

