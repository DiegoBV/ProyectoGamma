using UnityEngine;
using System.Collections;

public class PlacasPresion : MonoBehaviour {

    public GameObject [] targets; //aquello que active/desactive la placa
    Empujar caja;
    PlaySound sonido;

    void Start() {
        sonido = this.GetComponent<PlaySound>();
    }
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Placa kk");
        caja = other.gameObject.GetComponent<Empujar>();
        if (caja != null) { //ya que se activa poniendo algo empujable, no el jugador, etc
            if (sonido != null) {
                sonido.Play();
            }
            Debug.Log("Placa presionada");
            for (int i = 0; i < targets.Length; i++) //recorre el array
            {
                targets[i].SendMessage("Activate", SendMessageOptions.DontRequireReceiver); //envia mensajes de activación
                if (targets[i].GetComponent<CambioSala>())
                {
                    targets[i].SendMessage("Desbloquear", SendMessageOptions.DontRequireReceiver);
                }
            }
                
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        caja = other.gameObject.GetComponent<Empujar>();
        if (caja != null) { //ya que se desactiva saliendo algo empujable
            if (sonido != null) {
                sonido.Play();
            }
            Debug.Log("Placa despresionada");
            for (int i = 0; i < targets.Length; i++)
                targets[i].SendMessage("Desactivate", SendMessageOptions.DontRequireReceiver); //envia mensajes de desactivación
        }
    }
}
