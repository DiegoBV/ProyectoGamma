using UnityEngine;

public class PuntoRespawn : MonoBehaviour {

    public int numCamara;
    void Interaccionado()
    {
        GameManager.instance.NumRespawn = numCamara;
    }
}
