using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credito a esta pag web por el codigo
/*http://gamedesigntheory.blogspot.com.es/2010/09/controlling-aspect-ratio-in-unity.html */

public class Resolucion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //AspectRatio deseado, se puede hacer publico si lo queremos cambiar
        float targetaspect = 16.0f / 9.0f;

        //Ratio de actual de la camara
        float windowaspect = (float)Screen.width / (float)Screen.height;

        //Cantidad en la que debe variar la altura
        float scaleheight = windowaspect / targetaspect;

        //Accedemos a la camara para poder modificarla
        Camera camara = GetComponent<Camera>();

        //Si el ratio deseado es menor que la el actual

        if(scaleheight < 1.0f)
        {
            Rect rect = camara.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camara.rect = rect;
        }
        else
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camara.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
        }

	}
	
	
}
