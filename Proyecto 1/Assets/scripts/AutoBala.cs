using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBala : MonoBehaviour {

    public float velocidadBala;		//Velocidad de movimiento de la bala
    public char dirC;

    void Update() {
        Movement();
    }

    //Movimiento de la bala
    void Movement() {
        switch (dirC) {
            //hacia arriba
            case 'w':
                transform.Translate(new Vector2(0.0f,velocidadBala * Time.deltaTime));
                //Debug.Log("Bala moviéndose hacia arriba.");
                break;
            //hacia abajo
            case 's':
            case 'q':
                transform.Translate(new Vector2(0.0f,-velocidadBala * Time.deltaTime));
                //Debug.Log("Bala moviéndose hacia abajo.");
                break;
            //hacia la derecha
            case 'd':
                transform.Translate(new Vector2(velocidadBala * Time.deltaTime,0.0f));
                //Debug.Log("Bala moviéndose hacia la derecha.");
                break;
            //hacia la izquierda
            case 'a':
                transform.Translate(new Vector2(-velocidadBala * Time.deltaTime,0.0f));
                //Debug.Log("Bala moviéndose hacia la izquierda.");
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D hit) {

        switch (hit.tag) {
            //colisión con una pared o con una caja
            case "pared":
            case "caja":
                Debug.Log("Bala destruida.");
                Destroy(gameObject);
                break;
            //colisión con un barril
            case "barril":
                Debug.Log("Bala destruida. Barril debería explotar.");
                hit.gameObject.SendMessage("Explota",SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
                break;
            //colisión con un cristal
            case "cristal":
                Debug.Log("Bala destruida. Cristal debería activarse.");
                hit.gameObject.SendMessage("Activar",SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
                break;
            //colisión con un enemigo
            case "enemigo":
                hit.GetComponent<Enemigo>().vidaActualEnemigo--;
                Destroy(gameObject);
                break;
        }
    }
}
            
