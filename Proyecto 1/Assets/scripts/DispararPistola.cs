using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispararPistola : MonoBehaviour
{
    //Declaración de variables
    public Transform[] posicionBala;        //Posiciones posibles donde se puede spawnear la bala al disparar
    public GameObject Bala;                 //Gameobject de la bala
    public int energiaConsumida;
    public float shootDelay;                //Tiempo de recarga para que se pueda disparar de nuevo

    private char dirC;                      //Guarda el valor de la variable "dir" del Player Movement
    private bool reloading;					//Indica cuando se ha disparado
    PlayerMovement playermov;
    PlaySound sonido;
    //Métodos
    void Start()
    {
        //se comprueba y se guarda el valor de la variable "dir" en "dirC"
        GameObject player = GameObject.Find("Player 1");
        playermov = player.GetComponent<PlayerMovement>();
        sonido = this.GetComponent<PlaySound>();
    }
    void Update()
    {
        LogicGun();

    }
    //Lógica del funcionamiento de la pistola
    void LogicGun()
    {
        if (GameManager.instance.energia > 0) {
            //uso de la pistola
            if (Input.GetKeyDown(KeyCode.J) && !reloading) {
                dirC = playermov.dir;
                Shoot();
                GetComponent<EnergiaPistola>().Shoot();
                Debug.Log("Has disparado.");
                //pérdida de energía 
                GameManager.instance.VariacionEnergia(-energiaConsumida);
                if (sonido != null) {
                    sonido.Play();
                }
                GetComponent<EnergiaPistola>().RecuperaInc();
            }
        }

    }
    //Disparar
    void Shoot()
    {
        //mientras se está recargando, no se puede "instanciar" una nueva bala
        reloading = true;
        Invoke("isReloaded", shootDelay);
        //se comprueba el último valor de la variable dir (dirC) y se crea la bala en la posición correspondiente
        switch (dirC)
        {
            //derecha
            case 'd':
                Instantiate(Bala, posicionBala[0].position, posicionBala[0].rotation);
                gameObject.SendMessage("Movement", SendMessageOptions.DontRequireReceiver);
                Debug.Log("Posición bala: " + posicionBala[0].position + ", rotación bala: " + posicionBala[0].rotation);
                break;

            //izquierda
            case 'a':
                Instantiate(Bala, posicionBala[1].position, posicionBala[1].rotation);
                gameObject.SendMessage("Movement", SendMessageOptions.DontRequireReceiver);
                Debug.Log("Posición bala: " + posicionBala[1].position + ", rotación bala: " + posicionBala[1].rotation);
                break;

            //arriba
            case 'w':
                Instantiate(Bala, posicionBala[2].position, posicionBala[2].rotation);
                gameObject.SendMessage("Movement", SendMessageOptions.DontRequireReceiver);
                Debug.Log("Posición bala: " + posicionBala[2].position + ", rotación bala: " + posicionBala[2].rotation);
                break;

            //abajo
            case 's':
            case 'q':
                Instantiate(Bala, posicionBala[3].position, posicionBala[3].rotation);
                gameObject.SendMessage("Movement", SendMessageOptions.DontRequireReceiver);
                Debug.Log("Posición bala: " + posicionBala[3].position + ", rotación bala: " + posicionBala[3].rotation);
                break;

       
        }
    
    }
    //Finalización del tiempo de recarga de la pistola
    void isReloaded()
    {
        reloading = false;
    }

}