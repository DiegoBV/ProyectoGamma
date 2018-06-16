using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCte : MonoBehaviour {

    public float vel;   //velocidad y direccion que configura plataforma
    public bool waving;
    [HideInInspector]
    public Vector2 dir;

    float nivelSuavizado = 1, y;

    // Update is called once per frame
    void Update() {
        if (!waving)
            transform.Translate(dir * vel * Time.deltaTime);
        else {
            y = Mathf.Sin(Time.time) / nivelSuavizado; //funcion senoidal -> onda
            transform.Translate(0.0f,y * Time.deltaTime,0.0f);
        }
    }
}
