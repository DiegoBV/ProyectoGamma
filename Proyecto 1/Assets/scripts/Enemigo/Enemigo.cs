using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour {
    //Declaración de variables
    public float vidaMaxEnemigo,			//Vida máxima del enemigo
	vidaActualEnemigo,						//Vida actual del enemigo
	tiempoAtaca,							//Cada cuanto tiempo el enemigo hace daño al jugador
	dañoJugador;                            //Cantidad de vida que quita al jugador cuando le ataca
    public GameObject player, cadaver, rataCadaver;             //Gameobject jugador

    //Métodos
    void Start() {
        ajusteVida();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }
    //Ajuste de la vida del enemigo
    void ajusteVida() {
        if (vidaActualEnemigo > vidaMaxEnemigo)
            vidaActualEnemigo = vidaMaxEnemigo;

        else if (vidaMaxEnemigo < 1)
            vidaMaxEnemigo = 1;
    }
    //Restar vida al enemigo al recibir un disparo
    void Damage(int daño) {
        vidaActualEnemigo -= daño;
        Debug.Log("Daño enemigo. Vida enemigo: " + vidaActualEnemigo);
        if (vidaActualEnemigo <= 0) //la rata ya deberia morir
        {
            Instantiate(rataCadaver,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
    //Al morir el personaje, se recupera la vida del enemigo al "comerse" su cadáver
    public void recuperaVida() {
        vidaActualEnemigo = vidaMaxEnemigo;
    }

    //Detectar al jugador
    void OnTriggerEnter2D(Collider2D hit) {
        if (hit == player.GetComponent<Collider2D>()) {
            Debug.Log("Dañando al jugador.");
            InvokeRepeating("damagePlayer",1f,tiempoAtaca);
        }
        else
            Debug.Log("No hay objetivos que atacar.");
    }
    //Cuando no se detecta al jugador: este ha muerto o ha huído
    void OnTriggerExit2D(Collider2D hit) {
        if (hit == player.GetComponent<Collider2D>()) {
            CancelInvoke();
        }
        else
            Debug.Log("No hay objetivos.");
    }
    //Atacar al jugador
    void damagePlayer() {
        //Debug.Log("Dañado " + dañoJugador);
        GameManager.instance.takeDamage(dañoJugador);

        if (GameManager.instance.vida <= 0) {
            player.GetComponent<SpriteRenderer>().enabled = false;
            GameObject target = Instantiate(cadaver,player.transform.position,Quaternion.identity);
            GetComponent<RatIA>().CambiaTarget(target);
        }
    }
}