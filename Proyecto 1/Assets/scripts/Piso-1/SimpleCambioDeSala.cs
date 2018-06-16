using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCambioDeSala : MonoBehaviour {
    public GameObject Sala;
    Collider2D aux;
    public Animator anim;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() && Input.GetKeyDown(KeyCode.E))
        {
            aux = other;
            anim.SetBool("Abrir", true);
            other.GetComponent<PlayerMovement>().enabled = false;
            Invoke("Traslado", 1.5f);

        }
     }

    void Traslado()
    {
        anim.SetBool("Abrir", false);
        aux.transform.position = Sala.transform.position;
        aux.GetComponent<PlayerMovement>().enabled = true;
        CancelInvoke();
    }
}
