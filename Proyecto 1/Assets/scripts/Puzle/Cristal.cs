using UnityEngine;

public class Cristal : MonoBehaviour {

    public bool codigo;
    public int ordenCodigo;
    public GameObject ControladorCodigo, activado, desactivado;

    public GameObject[] ObjetosAfectados; // Array de objetos que van a ser afectados por el cristal  
    PlaySound sonido;
    void Start()
    {
        if (!codigo)
        {
            ordenCodigo = 0;
            ControladorCodigo = this.gameObject;
            sonido = this.GetComponent<PlaySound>();
        }
    }

    void Activar() // Metodo que se activa al recibir un mensaje de la bala en colision
    {
        activado.SetActive(true);
        desactivado.SetActive(false);

        if (!codigo)
        {
            for (int i = 0; i < ObjetosAfectados.Length; i++) // Se recorre el array de objetos que van a ser afectados por el cristal
            {
                ObjetosAfectados[i].SendMessage("Activate"); //Se le envia el mensaje a cada objeto
            }
        }
        else
        {
            ControladorCodigo.SendMessage("cristalActivado", ordenCodigo,SendMessageOptions.DontRequireReceiver);
        }
        if (sonido != null) {
            sonido.Play();
        }
    }
    void DesactivarCristal()
    {
        activado.SetActive(false);
        desactivado.SetActive(true);
        print("desactivar");
    }
}
