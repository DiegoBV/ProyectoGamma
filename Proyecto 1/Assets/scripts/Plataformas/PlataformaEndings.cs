using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class PlataformaEndings: MonoBehaviour {

    [HideInInspector]
    public bool esOrigen; //configurado por plataforma

    Plataformas plat;

    PlayerMovement player;
    Empujar caja;
    //para lograr que choquen contra las paredes


    private void OnTriggerEnter2D(Collider2D other) {

        //obtiene referencias
        plat = other.GetComponent<Plataformas>();
        player = other.GetComponent<PlayerMovement>();
        caja = other.GetComponent<Empujar>();

        if (plat != null) { //si lo que entra es una plataforma

            plat.SendMessage("EndingReached", esOrigen);
            //Debug.Log("other: " + other.name);
        }

        if (player != null || caja != null) { //si choca le quita el padre
            //Debug.Log("señor pared inc");   
            other.transform.parent = null;
            if (player != null) {
                DontDestroyOnLoad(GameManager.instance.player);
                GameManager.instance.player.transform.position =
                    new Vector3(GameManager.instance.player.transform.position.x,
                                GameManager.instance.player.transform.position.y,-1.1f);
            }
        }
    }
}
