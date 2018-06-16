using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioMusica : MonoBehaviour
{

    public bool reciclable = false;
    public int sonidoAlEntrar, sonidoAlSalir;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            GameManager.instance.StopMusic();
            GameManager.instance.PlayMusic(sonidoAlEntrar);
            if (!reciclable)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
