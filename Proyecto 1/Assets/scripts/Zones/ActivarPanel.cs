using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPanel : MonoBehaviour {
    public GameObject[] cristales;
    public GameObject controlador;
    PlaySound sonido;

    void Start()
    {
        sonido = this.GetComponent<PlaySound>();
    }
    void Activate()
    {
        for (int i = 0; i < cristales.Length; i++)
        {
            cristales[i].GetComponent<Cristal>().enabled = true;
            cristales[i].tag = "cristal";
        }
        controlador.gameObject.SetActive(true);
        if (sonido != null)
        {
            sonido.Play();
        }
    }
}
