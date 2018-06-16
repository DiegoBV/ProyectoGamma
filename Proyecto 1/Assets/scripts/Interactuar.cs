using UnityEngine;
using System.Collections;

public class Interactuar : MonoBehaviour {
	//Variables
	ushort tarjeta = 0,				//Recuento del número de tarjetas que tiene el jugador
	tarjetaS = 0;					//Recuento del número de tarjetas maestras/especiales que tiene el jugador
	public bool pistola = false;			//Indica cuándo se tiene la pistola

	public float radio;				//Radio del "círculo" imaginario (se edita desde el inspector)
	public float desplazamiento;	//Cuánto se desplaza el origen del círculo de las coordenadas del jugador (x,y) (se edita desde el inspector)

	//Tags públicas
	public string tagTarjeta, tagTarjetaS, tagPistola, tagPalanca, tagPuerta, tagPuertaS;

    public GameObject gun;
    static PlayerMovement playermov;
    PlaySound sonido;

    private char dirC;				//Donde se guarda el valor de la variable dir del PlayerMovement
    bool puedeCoger = true;
    void Start() {
        //se necesita tomar el valor de la variable dir del PlayerMovement para saber "hacia dónde está mirando" el jugador y, en función de
        //eso, desplazar el origen de coordenadas del "círculo" imaginario
        //se accede a la variable dir del player
        playermov = GetComponent<PlayerMovement>();
        if (pistola) gun.GetComponent<DispararPistola>().enabled = true;
    }

    //MÉTODOS
    void Update () {
		
		//se guarda el valor de la variable dir en dirC
		dirC = playermov.dir;

		LogicObject ();
	}

	//Lógica objetos
	void LogicObject () {
		if (Input.GetKeyDown ("e") && puedeCoger) {
			Debug.Log ("Pulsada E.");
			Take ();
            puedeCoger = false;
            Invoke("TrueCoger", 3f);
		}
	}
	//Coger un objeto
	void Take () {
		//coordenadas del jugador (x,y) en el instante en que "coge" algo
		float x = transform.position.x, y = transform.position.y;
		//se comprueba el último valor de la variable dir (dirC) y se modifican x, y para desplazar el origen del "círcculo" correctamente
		switch (dirC) {
		//hacia arriba
		case 'w':
			y = y + desplazamiento;
			Debug.DrawRay(new Vector2(x,y), transform.up, Color.green);
			break;
		//hacia abajo
		case 's':
			y = y - desplazamiento;
			Debug.DrawRay(new Vector2(x,y), -transform.up, Color.green);
			break;
		//hacia la izquierda
		case 'a':
			x = x - desplazamiento;
			Debug.DrawRay(new Vector2(x,y), -transform.right, Color.green);
			break;
		//hacia la derecha
		case 'd':
			x = x + desplazamiento;
			Debug.DrawRay(new Vector2(x,y), transform.right, Color.green);
			break;
		}

		//se detecta lo que está dentro del "círculo" imaginario y se crea un array
		Collider2D [] hitCol = Physics2D.OverlapCircleAll (new Vector2(x,y), radio);
		Debug.Log ("Posición x círculo: " + x + ", posición y círculo: " + y);

		//se realiza una búsqueda de los objetos que se pueden coger
		for (int i = 0; i < hitCol.Length; i++) {
			Collider2D hit = hitCol [i];
            //tarjeta
			if (hit.gameObject.CompareTag (tagTarjeta)) {
                sonido = hit.GetComponent<PlaySound>();
                if (sonido != null) {
                    sonido.Play();
                }
				tarjeta++;
				Debug.Log ("Objeto recogido. Número tarjetas: " + tarjeta);
                hit.GetComponent<SpriteRenderer>().enabled = false;
                hit.GetComponent<BoxCollider2D>().enabled = false;
			}
            //tarjeta especial
            else if (hit.gameObject.CompareTag(tagTarjetaS)) {
                sonido = hit.GetComponent<PlaySound>();
                if (sonido != null) {
                    sonido.Play();
                }
                tarjetaS++;
                hit.GetComponent<SpriteRenderer>().enabled = false; 
                hit.GetComponent<BoxCollider2D>().enabled = false;
            }
            //pistola
            else if (hit.gameObject.CompareTag (tagPistola)) {
				pistola = true;
				gun.GetComponent<DispararPistola> ().enabled = true;
				Debug.Log ("Objeto recogido. Pistola: " + pistola);
				Destroy (hit.gameObject);
			}
            //palanca (regenerar caja)
            else if (hit.gameObject.CompareTag (tagPalanca)) {
				Debug.Log ("Palanca activada.");
				hit.gameObject.SendMessage ("ComprobarArray", SendMessageOptions.DontRequireReceiver);
			}
			//puerta (abrir con tarjeta)
			else if (hit.gameObject.CompareTag (tagPuerta)) {
				Debug.Log ("Desbloqueo puerta.");
                if (tarjeta > 0) {
                    hit.gameObject.SendMessage("Desbloquear",SendMessageOptions.DontRequireReceiver);
                    tarjeta--;
                }
			}
			//puerta maestra/especial (abrir con tarjeta)
			else if (hit.gameObject.CompareTag (tagPuertaS)) {
				Debug.Log ("Desbloqueo puerta S.");
                if (tarjetaS > 0) {
                    hit.gameObject.SendMessage("Desbloquear",SendMessageOptions.DontRequireReceiver);
                    tarjetaS--;
                }
			}

            else if (hit.gameObject.GetComponent<NumeroSpawn>())
            {
                GameManager.instance.NumRespawn = hit.gameObject.GetComponent<NumeroSpawn>().NumSpawn;
            }

            else if (hit.gameObject.GetComponent<TextoHistoria>())
            {
                if (hit.gameObject.GetComponent<TextoHistoria>().Interactuable)
                {
                    hit.SendMessage("Interactuado", SendMessageOptions.DontRequireReceiver);
                }

            }
            else if (hit.gameObject.GetComponent<ActivarPanel>())
            {
                if (tarjetaS > 0)
                {
                    hit.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
                }
            }
            else
                Debug.Log("No se detecta ningún gameobject.");
		}
	}

    void TrueCoger()
    {
        puedeCoger = true;
    }

    /*void Destruir(Collider2D hit) {
        Destroy(hit.gameObject)
    }*/
}