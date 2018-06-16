using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desbloqueo : MonoBehaviour
{
    //Desbloqueo de puerta con tarjeta o placa de presión
    public void Desbloquear()
    {
        Debug.Log("Abro puerta.");
        this.GetComponent<CambioSala>().bloqueada = false;
        Debug.Log("Tarjeta eliminada.");
    }
}
