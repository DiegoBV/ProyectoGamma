using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(BoxCollider2D))]

public class TextoHistoria : MonoBehaviour {

	public bool Interactuable = false,	//indica si el texto aparece de forma automática o no
	leerOtraVez = false,				//indica si el texto puede volver a leerse
	final = false;						//indica si se carga la escena créditos

	public bool acabado = false;		//indica si continua el recuento

	public TextAsset archivoTexto;		//archivo a leer
	public string[] lineasDialogo;

	public int i = 0;		//contador
    public bool fiin = false;
	PlayerMovement player;
	public bool puedePasar = false;
    PlaySound sonido;
    int cont = 0;

	void Start () {
		player = FindObjectOfType<PlayerMovement>();
		this.GetComponent<BoxCollider2D>().isTrigger = true;
        sonido = this.GetComponent<PlaySound>();	
	}

	void Update () {
		//separa por líneas y las guarda en el array
		lineasDialogo = archivoTexto.text.Split('\n');

		//objetos interactuables
		if (i <= lineasDialogo.Length && Input.GetKeyDown (KeyCode.Space) && puedePasar) {
			if (i >= (lineasDialogo.Length - 1) && Input.GetKeyDown (KeyCode.Space)) {
				puedePasar = false;
				acabado = true;
				i = 0;
				player.puedeMoverse = true;
				GameManager.instance.panel.SetActive (false);
				//"limpiar" el texto
				for (int cont = (lineasDialogo.Length - 2); cont < lineasDialogo.Length; cont++) {
					lineasDialogo [cont] = "";
					GameManager.instance.texto.text = lineasDialogo [cont];
				}
				//si el texto es automático
				if (leerOtraVez == false)
                {
                    if (fiin && this.GetComponent<Traslado>())
                    {
                        this.GetComponent<Traslado>().GoTo(player.gameObject);
                    }
                    Destroy(this);
                 
                }
					
				else if (gameObject.CompareTag ("end"))
					final = true;
			}
			if (acabado == false) {
				GameManager.instance.texto.text = lineasDialogo [i] + "\n" + lineasDialogo [i + 1]; 
				i = i + 2;
			}
            
        }
		acabado = false;
        
        //cargar creditos
        if (final) {
			gameObject.SendMessage ("cargaCreditos", SendMessageOptions.DontRequireReceiver);
			Debug.Log ("Carga creditos");
		} 
	}

	void OnTriggerStay2D (Collider2D other) {
		//objetos no interactuables
		if (!Interactuable && other.GetComponent<PlayerMovement> ()) {
			if (sonido != null && cont == 0) {
				sonido.Play ();
				cont++;
			}
			player.puedeMoverse = false;
			puedePasar = true;
			GameManager.instance.panel.SetActive (true);
		}
	}

	void Interactuado() {
        if (sonido != null) {
            sonido.Play();
        }
		player.puedeMoverse = false;
		puedePasar = true;
		GameManager.instance.panel.SetActive(true);
      
    }
}