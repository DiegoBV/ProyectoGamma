using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MovimientoCte))]
public class RatIA : MonoBehaviour {


    public float vel = 30, delay, countdown, tiempoCadaver;
    public int salaRata;

    static GameObject target, player;
    static Vector2 dir, dirNewTarget;
    MovimientoCte Mov;
    public Animator anim;
    Vector2 der, izq;
    PlaySound sonido; //cosas de sonido
    int cont = 0;
    public bool ray;

    // Use this for initialization
    void Start() {

        Mov = this.GetComponent<MovimientoCte>();
        player = this.GetComponent<Enemigo>().player;
        target = player;
        der = new Vector2(-transform.localScale.x,transform.localScale.y);
        izq = new Vector2(transform.localScale.x,transform.localScale.y);
        countdown = 0;
        sonido = this.GetComponent<PlaySound>();
        InvokeRepeating("Perseguir",delay,delay);
    }

    void Perseguir() {

        dir = (target.transform.position - this.transform.position);
        Mov.dir = dir.normalized;
        //Debug.Log("Dir sqrt" + dir.sqrMagnitude);

        ray = true; //raycast que comprueba si hay pared entre jugador y rata
        //Debug.DrawLine(transform.position,(transform.position+new Vector3(dir.x/2, dir.y/2,0)));
        RaycastHit2D [] hits = Physics2D.RaycastAll(new Vector2(transform.position.x,transform.position.y),dir,dir.magnitude);

        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].collider.gameObject.CompareTag("pared")) ray = false;
        }

        if (dir.sqrMagnitude < 15000 && GameManager.instance.HabActual == salaRata 
            && ray) {
            Mov.vel = vel;
            anim.SetBool("Andar",true);
            if (dir.x >= 0) {
                this.transform.localScale = der;
            }
            else
                this.transform.localScale = izq;
            if (sonido != null && cont == 0) {
                sonido.Play();
                cont++; //queda tocarlo para que siempre que te vea suene algo
            }
        }
        else {
            Mov.vel = 0;
            anim.SetBool("Andar",false);
        }
    }

    public void CambiaTarget(GameObject newTarget) {
        target = newTarget;
        Mov.vel = 0;
        CancelInvoke();
        InvokeRepeating("CheckTarget",delay,delay);
    }

    void CheckTarget() {

        dir = (player.transform.position - this.transform.position);
        if (dir.sqrMagnitude < 15000 && GameManager.instance.HabActual == salaRata) {
            countdown++;
        }

        if (countdown > tiempoCadaver) {
            Destroy(target);
            GetComponent<Enemigo>().recuperaVida();
            target = player;

            CancelInvoke();
            InvokeRepeating("Perseguir",delay,delay);
        }

    }

}
