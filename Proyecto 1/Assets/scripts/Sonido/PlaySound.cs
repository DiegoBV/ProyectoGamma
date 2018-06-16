using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class PlaySound : MonoBehaviour {

    public AudioClip [] SonidoAActivar;
    private AudioSource source;
    //public float Volume = .5f;
    int i = 2;
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play()
    {
        Debug.Log("YEYYYYYYYYYYYYYYYY");
        source.PlayOneShot(SonidoAActivar[0] ,GameManager.instance.Volume);
    }

    public void Play2()
    {
        source.PlayOneShot(SonidoAActivar[1], GameManager.instance.Volume);
    }

    public void PlayMusica()
    {
        source.PlayOneShot(SonidoAActivar[i], GameManager.instance.Volume);
    }
}
