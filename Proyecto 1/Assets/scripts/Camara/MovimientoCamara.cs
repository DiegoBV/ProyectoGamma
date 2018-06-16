using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//La camara recibe un mensaje con una posicion a la que se mueve
public class MovimientoCamara : MonoBehaviour {

    void MoverCamara(Vector3 pos) 
    {
        transform.position = pos;

    }
}
