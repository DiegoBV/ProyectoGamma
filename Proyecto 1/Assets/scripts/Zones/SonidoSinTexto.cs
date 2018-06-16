using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoSinTexto : MonoBehaviour {

    int cont = 0;
    PlaySound sonido;

    void Start()
    {
        sonido = this.GetComponent<PlaySound>();
    }
    void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("Tio esto funciona");
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            if(sonido != null && cont == 0)
            {
                sonido.Play();
                cont++;
                
            }
        }
    }
}
