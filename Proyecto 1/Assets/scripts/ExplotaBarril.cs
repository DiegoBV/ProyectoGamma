using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotaBarril : MonoBehaviour {

    public GameObject pared;
    public float time = 4;
    public Animator anim;
    PlaySound sonido;

    void Start() {
        sonido = this.GetComponent<PlaySound>();
    }
    void Explota()
    {
        if (sonido != null) {
            sonido.Play();
        }
        anim.SetBool("Explosion", true);
        Invoke("Destruir",time);
        Invoke("RomperPared", time / 2);
    }

    void Destruir()
    {
        anim.SetBool("Explosion", false);
        Destroy(this.gameObject);

    }
    void RomperPared()
    {
        pared.gameObject.SetActive(true);
    }
}
