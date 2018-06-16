using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AnimacionPuerta : MonoBehaviour {

    public AudioClip door;
    private AudioSource source;
    //public float Volume = .5f;
    public Animator anim;
    public bool tieneMusica = false;
    public int i;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Activate()
    {
        anim.SetBool("Abrir", true);
        source.PlayOneShot(door, GameManager.instance.Volume);
        if (tieneMusica)
        {
            GameManager.instance.StopMusic();
            GameManager.instance.PlayMusic(i);
        }
        Invoke("Cerrar", 3f);
    }
    void Cerrar()
    {
        anim.SetBool("Abrir", false);
    }
}
