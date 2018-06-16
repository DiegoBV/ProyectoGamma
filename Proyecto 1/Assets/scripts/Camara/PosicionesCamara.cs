using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 // Script que envia un mensaje a la camara para que se mueva a la posicion del "dueño" del script.
public class PosicionesCamara : MonoBehaviour {

    public GameObject camara; // Referencia a la camara
    public GameObject camaraSecundaria; // Referencia a la camara secundaria

    Vector3 pos; // Variable que guarda tu posicion

    void CambioSala() //Al recibir un mensaje de la puerta, enviamos un mensaje a la camara para que se mueva a nuestra posicion
    {
        camara.SendMessage("MoverCamara",transform.position);
        camaraSecundaria.SendMessage("MoverCamara", transform.position);
    }
}
