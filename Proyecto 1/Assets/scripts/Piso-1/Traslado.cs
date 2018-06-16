using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traslado : MonoBehaviour {


    public GameObject lugar;
    
    public void GoTo(GameObject other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            other.gameObject.transform.position = lugar.transform.position;
            other.gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
