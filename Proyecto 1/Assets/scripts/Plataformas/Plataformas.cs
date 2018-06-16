using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovimientoCte))] //ejecutara el MRU
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

//toda la logica del mov de las plataformas se controla aqui
public class Plataformas : MonoBehaviour {
    public bool warpingEndings;
    public bool stoppedDefault;
    public float vel;
    private float constVel = 65; //editar segun tamaño prefab
    public GameObject origen, final;

    public bool automaticDelay = true; //en caso de necesitar un valor especial se desactiva
    public float delay; //timing para que la plataforma haga tp (efecto desaparece)

    public Transform[] flechas; //para invertir las flechitas de direccion

    public bool debugLogMode;

    MovimientoCte Mov; //poder modificarlo desde este cs
    Vector2 dir;

    bool warpeableOrigen = true,
                warpeableFinal = true; //para evitar rewarps

    PlayerMovement player;
    Empujar caja;
    //para comprobar que solo caja y player son movidos

    void Start() {

        //config inicial
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Rigidbody2D>().isKinematic = true;

        dir = (final.transform.position - origen.transform.position).normalized;
        Mov = GetComponent<MovimientoCte>();
        Mov.dir = dir;

        if (debugLogMode) {
            Debug.Log("Vel plat:" + vel);
            Debug.Log("Dir plat:" + dir);
            Debug.Log("Pos origen:" + origen.transform.position);
            Debug.Log("Pos final:" + final.transform.position);
            Debug.Log("mov: " + Mov.gameObject.name);
        }

        //config segun opciones
        if (stoppedDefault) Mov.vel = 0;
        else Mov.vel = vel;

        if (automaticDelay) delay = Mathf.Abs(constVel / vel);

        //config endings
        origen.GetComponent<PlataformaEndings>().esOrigen = true;
        origen.GetComponent<Collider2D>().isTrigger = true;
        final.GetComponent<PlataformaEndings>().esOrigen = false;
        final.GetComponent<Collider2D>().isTrigger = true;


    }

    public void EndingReached(bool esOrigen) {

        if (debugLogMode) Debug.Log("Colision con ending. Origen? " + name);

        if (warpingEndings) { //sencillo uso de invokes para timing

            if (!esOrigen && warpeableOrigen) { //uso de los bool para no sufrir multi warping
                warpeableFinal = false;
                Invoke("WarpNormal",delay);
                if (debugLogMode) Debug.Log("warped orig " + name);
            }
            else if (esOrigen && warpeableFinal) { //final
                warpeableOrigen = false;
                Invoke("WarpInverso",delay);
                if (debugLogMode) Debug.Log("warped fin " + name);
            }

        }
        else {  //modo rebote
            Mov.vel *= -1;
            InvierteFlechas();

        }

    }

    private void WarpNormal() { //choca con el "final"
        transform.position = origen.transform.position;
        Invoke("RecuWarps",delay);
    }

    private void WarpInverso() {
        transform.position = final.transform.position;
        Invoke("RecuWarps",delay);
    }

    private void RecuWarps() {
        warpeableFinal = true; //siempre se usa el mismo delay
        warpeableOrigen = true;
    }


    public void Activate() {

        if (warpeableFinal && warpeableOrigen) ActivateOP();
        else Invoke("ActivateOP",delay * 2);
    }

    public void ActivateOP() { //metodos de activacion externa

        Debug.Log("activate!");

        if (stoppedDefault) Mov.vel = vel;
        else {
            Mov.vel = -vel;
            InvierteFlechas();
        }
    }

    public void Desactivate() {

        if (warpeableFinal && warpeableOrigen) DesactivateOP();
        else Invoke("DesactivateOP",delay * 2);
    }

    public void DesactivateOP() {

        if (stoppedDefault) Mov.vel = 0;
        else {
            Mov.vel = vel;
            InvierteFlechas();
        }
    }


    public void OnTriggerEnter2D(Collider2D other) {

        //obtiene referencias del objeto que entra
        player = other.GetComponent<PlayerMovement>();
        caja = other.GetComponent<Empujar>();

        //si no es null --> es player o caja
        if (player != null) {
            other.transform.parent = transform;
            //hacer que el jugador pueda no morir por la deathzone
            if (player.killable)
                player.killable = false;
            else player.killableAux = false;
        }
        if (caja != null) {
            other.transform.parent = transform;
            other.GetComponent<Recolocar>().relocatable = false;
        }
    }

    public void OnTriggerExit2D(Collider2D other) {

        player = other.GetComponent<PlayerMovement>();
        caja = other.GetComponent<Empujar>();

        if (player != null) {
            //primero comprueba que no te habias chocado con la pared
            if (player.GetComponentInParent<Plataformas>() == null) GameManager.instance.takeDamage(GameManager.instance.VidaInicial); //instaKill
            if (player.killableAux) {
                other.transform.parent = null;
                DontDestroyOnLoad(GameManager.instance.player);
                GameManager.instance.player.transform.position = 
                    new Vector3(GameManager.instance.player.transform.position.x,
                                GameManager.instance.player.transform.position.y, -1.1f);
            }
            //hacer que el jugador pueda morir por la deathzone
            if (player.killableAux)
                player.killable = true;
            else player.killableAux = true;
        }
        if (caja != null) { //reinicia la caja si se sale de la plat habiendo chocado con la pared
            other.GetComponent<Recolocar>().relocatable = true;
            if (caja.GetComponentInParent<Plataformas>() == null) other.gameObject.SendMessage("Relocate",SendMessageOptions.DontRequireReceiver);
            other.transform.parent = null;
        }

    }


    public void InvierteFlechas() {

        foreach (Transform flecha in flechas) {

            flecha.localScale = new Vector3
                (flecha.localScale.x,-flecha.localScale.y,flecha.localScale.z);

        }
    }

}