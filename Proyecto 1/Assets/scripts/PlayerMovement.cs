using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int i;

    //public GameObject OtraCaja;
    [HideInInspector] public char dir;
    [HideInInspector]
    public bool puedeMoverse = true, puedeMorir = true; //Podria usar el killable que ya esta creado?
    char animacion;
    public float velocidad;
    RaycastHit2D[] hit;
    //RaycastHit2D hit2, hit3;
    public Animator anim;
    float altura ;
    float ancho;
    public string TagObjeto;
    bool disparo = false;
    public GameObject DisparoDer,DisparoIzq,DisparoArr, DisparoAb;
    public float radio = 0.5f;
    public float distancia = 0.1f;
    public bool killable = true, killableAux = true;
    public GameObject gun, cadaver;
    float cont = 0;


    float radioInicial;
    bool hacks = false;
    PlayerMovement[] jugadores;
    void Start()
    {
        radioInicial = radio;
        DisparoAb.SetActive(false);
        DisparoArr.SetActive(false);
        DisparoDer.SetActive(false);
        DisparoIzq.SetActive(false);
        altura = this.gameObject.transform.localScale.y;
        ancho = this.gameObject.transform.localScale.x;

        jugadores = FindObjectsOfType<PlayerMovement>();

        for (int i = 0; i < jugadores.Length; i++)
        {
            if (jugadores[i].gameObject != this.gameObject) Destroy(jugadores[i].gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        cont += Time.deltaTime;
        if(cont >= 1)
        {
            DesactivarSprites();
        }
        //Debug.Log(cont);
        Movimiento();
	    GameManager.instance.Menu();
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.instance.Mapa();
        }

        //Hacks
        if (Input.GetKeyDown(KeyCode.H))
        {
            hacks = !hacks;
            if (hacks)
            {
                this.gameObject.GetComponent<Collider2D>().enabled = false;
                radio = 0;
            }

            else
            {
                this.gameObject.GetComponent<Collider2D>().enabled = true;
                radio = radioInicial;
            }
             
        }
    }

    void Movimiento()
    {
        bool flag = true;
        if (puedeMoverse)
        {
            if (Input.GetKey(KeyCode.W))
            {

                dir = 'w';
                animacion = 'w';

                hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, transform.up, distancia);

                //print(hit.Length);

                for (int i = 0; i < hit.Length; i++)
                    {
                        //print(hit[i].collider.gameObject.name);
                        if (hit[i].collider.CompareTag(TagObjeto))
                            Empujar(dir, hit[i]);
                        else if (!hit[i].collider.isTrigger) flag = false;
                    }

                if (flag)
                {
                    hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, transform.up, distancia);
                    for (int i = 0; i < hit.Length; i++)
                    {
                        //print("2º flag");
                        if (!hit[i].collider.isTrigger || (hit[i].collider.CompareTag(TagObjeto))) flag = false;
                    }
                }
                if (flag) transform.Translate(new Vector2(0f, velocidad));

                if (Input.GetKey(KeyCode.J))
                {
                    disparo = true;
                }
                else
                    disparo = false;
                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
                {
                    animacion = 'a';
                }
                else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                {
                    animacion = 'd';
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                dir = 'd';
                animacion = 'd';
                if (Input.GetKey(KeyCode.J))
                {
                    disparo = true;
                }
                else
                    disparo = false;


                hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, transform.right, distancia);

                if (hit != null)
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if (hit[i].collider.CompareTag(TagObjeto))
                            Empujar(dir, hit[i]);    
                        else if (!hit[i].collider.isTrigger) flag = false;
                    }
                if (flag)
                {
                    hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, transform.right, distancia);
                    for (int i = 0; i < hit.Length; i++)
                    {
                        //print("2º flag");
                        if (!hit[i].collider.isTrigger || (hit[i].collider.CompareTag(TagObjeto))) flag = false;
                    }
                }
             
                if (flag)
                    transform.Translate(new Vector2(velocidad, 0f));

            }

            if (Input.GetKey(KeyCode.A))
            {
                
                dir = 'a';
                animacion = 'a';
                if (Input.GetKey(KeyCode.J))
                {
                    disparo = true;
                }
                else
                    disparo = false;


                hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, -transform.right, distancia);

                
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if (hit[i].collider.CompareTag(TagObjeto))
                            Empujar(dir, hit[i]);  
                        else if (!hit[i].collider.isTrigger) flag = false;
                    }

                if (flag)
                {
                    hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, -transform.right, distancia);
                    for (int i = 0; i < hit.Length; i++)
                        {
                            //print("2º flag");
                            if (!hit[i].collider.isTrigger || (hit[i].collider.CompareTag(TagObjeto))) flag = false;
                        }
                }
                if (flag) transform.Translate(new Vector2(-velocidad, 0f));

            }

            if (Input.GetKey(KeyCode.S))
            {
                dir = 's';
                animacion = 's';

                hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, -transform.up, distancia);
                if (hit != null)
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if (hit[i].collider.CompareTag(TagObjeto))
                            Empujar(dir, hit[i]);   
                        else if (!hit[i].collider.isTrigger) flag = false;
                    }

                if (flag)
                {
                    hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, -transform.up, distancia);
                    for (int i = 0; i < hit.Length; i++)
                    {
                        //print("2º flag");
                        if (!hit[i].collider.isTrigger || (hit[i].collider.CompareTag(TagObjeto))) flag = false;
                    }
                }
                
                if (flag)
                    transform.Translate(new Vector2(0f, -velocidad));

                if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
                {
                    animacion = 'a';
                }
                else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
                {
                    animacion = 'd';
                }

                if (Input.GetKey(KeyCode.J))
                {
                    disparo = true;
                    Debug.Log(disparo);
                    Debug.Log(animacion);
                }
                else
                    disparo = false;

            }


            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
            {
                animacion = 'q';
                dir = 'q';
                if (Input.GetKeyDown(KeyCode.J))
                {
                    
                    dir = 's';
                    disparo = true;
                }

            }
            if (gun.GetComponent<DispararPistola>().enabled == false)
            {
                disparo = false;
            }
            Animaciones(animacion, disparo);

            if (Input.GetKey(KeyCode.K)) {
                this.GetComponent<SpriteRenderer>().enabled = false;
                GameObject target = Instantiate(cadaver,transform.position,Quaternion.identity);
                GameManager.instance.takeDamage(GameManager.instance.VidaInicial);
            }
        }
    }
       

    void Empujar(char dir, RaycastHit2D hit)
    {
        hit.collider.gameObject.SendMessage("Push", dir, SendMessageOptions.DontRequireReceiver);
    }

    void Animaciones(char dir, bool disparo)
    {
        
        if (dir == 'a')
        {
            if (disparo)
            {
                cont = 0;
                DisparoIzq.SetActive(true);
                DisparoAb.SetActive(false);
                DisparoArr.SetActive(false);
                DisparoDer.SetActive(false);
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
            else 
            {
                anim.SetBool("AndaIzq", true);
                anim.SetBool("Quieto", false);
                anim.SetBool("AndaArr", false);
                anim.SetBool("AndaDer", false);
                anim.SetBool("AndaAb", false);    
            }
            
        }
        else if (dir == 'w')
        {
            if (disparo)
            {
                cont = 0;
                DisparoArr.SetActive(true);
                DisparoAb.SetActive(false);
                DisparoDer.SetActive(false);
                DisparoIzq.SetActive(false);
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
            else 
            {
                anim.SetBool("AndaArr", true);
                anim.SetBool("Quieto", false);
                anim.SetBool("AndaAb", false);
                anim.SetBool("AndaDer", false);
                anim.SetBool("AndaIzq", false);
            }     

        }
        else if (dir == 'd')
        {
            if (disparo)
            {
                cont = 0;
                DisparoDer.SetActive(true);
                DisparoAb.SetActive(false);
                DisparoArr.SetActive(false);
                DisparoIzq.SetActive(false);
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
            else 
            {
                anim.SetBool("AndaDer", true);
                anim.SetBool("Quieto", false);
                anim.SetBool("AndaArr", false);
                anim.SetBool("AndaAb", false);
                anim.SetBool("AndaIzq", false);
            }   
        }
        else if (dir == 's')
        {
            if (disparo)
            {
                Debug.Log("No pillo porq esta mierda no funcionaaaaaaaaa");
                cont = 0;
                DisparoAb.SetActive(true);
                DisparoArr.SetActive(false);
                DisparoDer.SetActive(false);
                DisparoIzq.SetActive(false);
                this.GetComponent<SpriteRenderer>().enabled = false;    
            }
            else 
            {
                anim.SetBool("AndaDer", false);
                anim.SetBool("Quieto", false);
                anim.SetBool("AndaArr", false);
                anim.SetBool("AndaAb", true);
                anim.SetBool("AndaIzq", false);
            }
        }
        else
        {
            if (disparo)
            {
                cont = 0;
                DisparoAb.SetActive(true);
                DisparoArr.SetActive(false);
                DisparoDer.SetActive(false);
                DisparoIzq.SetActive(false);
                this.GetComponent<SpriteRenderer>().enabled = false; 
            }
            else 
            {
                anim.SetBool("AndaDer", false);
                anim.SetBool("Quieto", true);
                anim.SetBool("AndaArr", false);
                anim.SetBool("AndaAb", false);
                anim.SetBool("AndaIzq", false);
            }
        }

    }

    void DesactivarSprites()
    {
        DisparoAb.SetActive(false);
        DisparoArr.SetActive(false);
        DisparoDer.SetActive(false);
        DisparoIzq.SetActive(false);
        this.GetComponent<SpriteRenderer>().enabled = true;
        //CancelInvoke();
    }
    public void TeHasMuerto(bool deVerdad, char dir)
    {
        if (deVerdad)
        {
            anim.SetBool("MuerteCaida", true);
            if (dir == 's')
            {
                transform.Translate(new Vector2(0f, -velocidad * 0.9f));
            }
            else if(dir == 'w')
            {
                transform.Translate(new Vector2(0f, velocidad* 0.9f));
            }
            else if(dir == 'a')
            {
                transform.Translate(new Vector2(-velocidad* 0.9f, 0f));
            }
            else if(dir == 'd')
            {
                transform.Translate(new Vector2(velocidad* 0.9f, 0f));
            }
        }
        else
        {
            anim.SetBool("MuerteCaida", false);
            anim.SetBool("Quieto", true);
        }
        
    }

    /*void LateUpdate()
    {
        if(hit.collider != null && !hit.collider.isTrigger)
        {
            transform.Translate(new Vector2(0f, -velocidad*10));
        }
    }*/

}