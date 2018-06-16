using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioPiso : MonoBehaviour {

    public string SiguienteNivel;
    Collider2D aux;
    public GameObject puerta;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            aux = other;
            other.GetComponent<PlayerMovement>().enabled = false;
            if(puerta != null)
            {
                puerta.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
            }
            Invoke("Traslado", 3f);
          
        }
    }

    void Traslado()
    {
        aux.GetComponent<PlayerMovement>().enabled = true;
        GameManager.instance.CambioPiso(SiguienteNivel);
        GameManager.instance.PisoActual = GameManager.instance.PisoActual + 1;
        //falta poner que la posicion sea igual al spawn del siguiente piso pero eso lo dejo pa luego :P
        aux.gameObject.transform.position = GameManager.instance.respawns[GameManager.instance.PisoActual].transform.position;
        CancelInvoke();
    }

}
