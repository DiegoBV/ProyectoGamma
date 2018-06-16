using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour {
    //Declaración de variables
    public GameObject [] Objetos;           //Objetos que están relacionados con la palanca (cajas, plataformas)
    public string tagCaja, tagPlataforma;	//Tags públicas
    public Animator anim;
    public AudioClip sonidoPalanca;
    private AudioSource source;      //sonido y animación
    //public float Volume = .5f;
    //MÉTODOS
    void Awake() {
        source = GetComponent<AudioSource>();
    }
    //Comprobar el "tag" de los objetos del array y mandar un mensaje
    void ComprobarArray() {
        for (int i = 0; i < Objetos.Length; i++) {
            GameObject obj = Objetos [i];
            Debug.Log("Objeto: " + obj);
            Debug.Log("Tag objeto: " + obj.tag);
            //caja
            if (obj.CompareTag(tagCaja)) {
                Debug.Log("Recolocar caja.");
                obj.SendMessage("RelocateOP",SendMessageOptions.DontRequireReceiver);
            }
            //plataforma
            else if (obj.CompareTag(tagPlataforma)) {
                Debug.Log("Activar plataforma.");
                obj.SendMessage("Activate",SendMessageOptions.DontRequireReceiver);
            }
            else
                Debug.Log("El array está vacío o el gameobject relacionado con la palanca no posee el tag correspondiente.");
        }
        source.PlayOneShot(sonidoPalanca, GameManager.instance.Volume);
        anim.SetBool("Activado",true);
        Invoke("ReturnNormal",1.6f);
    }

    void ReturnNormal() {
        source.PlayOneShot(sonidoPalanca, GameManager.instance.Volume);
        anim.SetBool("Activado",false);
    }
}