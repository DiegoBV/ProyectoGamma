using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player o caja que entre reset
public class DeathZone : MonoBehaviour {

    public bool pinchos;
    void OnTriggerStay2D(Collider2D other) {

        if (/*other.CompareTag("caja")*/other.gameObject.GetComponent<Recolocar>()) {
            Debug.Log("cajiiiita");
            if(other.gameObject.GetComponent<Empujar>().sonido != null )
            {
                other.gameObject.GetComponent<Empujar>().sonido.Play();
            }
            
            other.gameObject.SendMessage("Relocate",SendMessageOptions.DontRequireReceiver); //con delay y cositas
        }
        //para comprobar que el objeto que entre sea player o caja
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            if (!pinchos) {
                if (other.GetComponent<PlayerMovement>().killable) {
                    //other.gameObject.transform.position = this.transform.position;
                    // other.gameObject.SendMessage("TeHasMuerto", true, other.GetComponent<PlayerMovement>().dir, SendMessageOptions.DontRequireReceiver);
                    other.GetComponent<PlayerMovement>().TeHasMuerto(true,other.GetComponent<PlayerMovement>().dir);
                    GameManager.instance.takeDamage(GameManager.instance.VidaInicial); //instaKill
                    Debug.Log(other.GetComponent<PlayerMovement>().dir);
                    GameManager.instance.HabActual = 0;
                    if (GameManager.instance.MapActivo) {
                        GameManager.instance.MapActivo = false;
                        GameManager.instance.Mapa();
                    }
                }
            }
            else if (other.GetComponent<PlayerMovement>().puedeMorir) {
                other.GetComponent<PlayerMovement>().puedeMorir = false;
                this.GetComponent<SpriteRenderer>().enabled = false;
                other.GetComponent<SpriteRenderer>().enabled = false;
                GameManager.instance.takeDamage(GameManager.instance.VidaInicial); //instaKill
                Debug.Log("has muerto || " + this.name);
                Destroy(this);
                GameManager.instance.HabActual = 0;
                if (GameManager.instance.MapActivo) {
                    GameManager.instance.MapActivo = false;
                    GameManager.instance.Mapa();
                }
            }           
        }
        
    }
    void OnTriggerExit2D(Collider2D other) {

        other.SendMessage("CancelDeath",SendMessageOptions.DontRequireReceiver);

    }

  

}

