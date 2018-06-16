using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que detecta la entrada del jugador en una sala y envia un mensaje a un GO que tiene la pos que debe tomar la camara
public class Puertas : MonoBehaviour {

    public string TagJugador; // Tag del jugador
    public GameObject posicion; // GO que tiene la posicion de la camara en la sala
    public KeyCode hola; //PRUEBAS
    //void Update() //PRUEBAS
   // {
       
       // if (Input.GetKeyDown(hola)) // PRUEBAS
        //{
           // posicion.SendMessage("CambioSala"); // PRUEBAS
       // }
       
   // }
    void OnTriggerEnter2D(Collider2D other) //Cuando se entra en una sala se le envia un 
                                            //mensaje a un GO con la posicion que debe asumir la camara
    {
        if (other.CompareTag(TagJugador))
        {
            posicion.SendMessage("CambioSala");
        }
    }
    
}
