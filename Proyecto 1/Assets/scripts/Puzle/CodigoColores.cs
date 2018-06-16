using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodigoColores : MonoBehaviour {

    //Al disparar un cristal, devuelve un mensaje con su orden en la activacion del codigo
    //Se comprueba si todos los cristales con un orden de activacion menor estan activados

    //Si estan activados se pasa a activar el siguiente, y cuando todos esten activados se envia un mensaje
    //Si no estan activados se desactivan (Enviar msj desactivar para cambiar sprite)(Al disparar un cristal se cambia el sprite independiente del codigo) ç

    public GameObject premio;
    public GameObject[] Cristales;
    bool[] codigo;

	void Start () {
        codigo = new bool[Cristales.Length];
        for (int z = 0; z < Cristales.Length; z++) codigo[z] = false;
	}	
    void cristalActivado(int orden) // Lo llama el cristal
    {
        codigo[orden-1] = true;
        patron(orden);
    }    
    void patron(int orden)
    {
        bool flag=true;
        int i=0;
        while(i <orden && flag) //Comprobamos que estan activados todos los cristales anteriores
                                //Al ser la i menor estricta siempre va detras de orden y hace la relaccion 1=0(en el array)
        {
            print("Orden"+orden+ " i="+i);
            if (codigo[i])  // Si esta activado pasamos al siguiente
            {
                i++;
            } 
            else
            {
                
                for (int z = 0; z < codigo.Length; z++) codigo[z] = false; // Si no esta activado ponemos todos a falso
                DesactivarCristales();   // Desactivamos todos los cristales
                flag = false;  //Salimos del bucle
            }
        }
        if (orden == Cristales.Length/*-1*/&& flag) //Si estamos en el ultimo y no ha saltado el flag
        {
            CodigoActivado();   //El codigo ha sido resuelto
        }
    }
    void DesactivarCristales()
    {
        for (int i = 0; i < Cristales.Length; i++)
        {                                                 
            Cristales[i].SendMessage("DesactivarCristal"/*,SendMessageOptions.DontRequireReceiver*/);  //Puede haber una forma mas elegante accediendo a los cristales[i].Algo
        }
    }
    void CodigoActivado()
    {
        print("Codigo Resuelto");
        premio.gameObject.SetActive(true);
    }

    //Intentar que los cristales cambien despues de haber activado 4, y no al fallar el 1º

}
