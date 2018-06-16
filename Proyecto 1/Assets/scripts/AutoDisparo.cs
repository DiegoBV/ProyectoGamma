using UnityEngine;

public class AutoDisparo : MonoBehaviour {

    public GameObject proyectil; //el objeto que se dispara
    public float speedProyectil, delay; //delay entre disparos y velocidad
    public bool activadoDefault;
    //la direccion del disparo varia con la rotacion del GM vacio que lo tenga

    // Use this for initialization
    void Start() {
        if (activadoDefault)
            InvokeRepeating("AutoDisparar",delay,delay);
    }

    private void AutoDisparar() {
        Instantiate(proyectil,transform.position,Quaternion.identity); //simplemente instancia el proyectil       
    }

    void Activate() {
        InvokeRepeating("AutoDisparar",delay,delay);
    }

}
