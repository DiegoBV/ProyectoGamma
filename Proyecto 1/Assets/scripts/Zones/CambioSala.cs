using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioSala : MonoBehaviour {

    public GameObject Sala, puerta;
    Collider2D aux;
    public bool TieneAnimacion, bloqueada;
    public int tapaAQuitar, tapaAPoner;
    public bool Secreta = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (!bloqueada)
        {
            if (!Secreta)
            {
                if (other.GetComponent<PlayerMovement>() && Input.GetKeyDown(KeyCode.E))
                {
                    aux = other;
                    if (TieneAnimacion)
                    {
                        puerta.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
                    }
                    other.GetComponent<PlayerMovement>().enabled = false;
                    Invoke("Traslado", 2f);
                }
            }
            else
            {
                if (other.GetComponent<PlayerMovement>())
                {
                    aux = other;
                    if (TieneAnimacion)
                    {
                        puerta.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
                    }
                    other.GetComponent<PlayerMovement>().enabled = false;
                    Invoke("Traslado", 2f);
                }

            }

        }
        
    }

        void Traslado()
    {
            aux.transform.position = Sala.transform.position;
            //tapaAQuitar.SetActive(false);
            //tapaAPoner.SetActive(true);
            GameManager.instance.Tapas[GameManager.instance.PisoActual][tapaAQuitar].SetActive(false);
            GameManager.instance.Tapas[GameManager.instance.PisoActual][tapaAPoner].SetActive(true);
            GameManager.instance.HabActual = tapaAQuitar;
        if (GameManager.instance.MapActivo) {
            GameManager.instance.MapActivo = false;
            GameManager.instance.Mapa();
        }
            aux.GetComponent<PlayerMovement>().enabled = true;
            CancelInvoke();
        }
    }

