using UnityEngine;
using System.Collections;

public class Empujar : MonoBehaviour
{
    //float altura;
    //float ancho;
    public float distancia = 1;
    RaycastHit2D[] hit;
    public float radio = 1;
    public float velocidad = 1;
    [HideInInspector]
    public PlaySound sonido;
    int cont = 0;
    void Start()
    {
        sonido = this.GetComponent<PlaySound>();
    }

    void Push(char dir)
    {
        bool flag = true;
        //print("Push " + dir);
        if (dir == 'd')
        {
            hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, transform.right, distancia);


            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.gameObject != this.gameObject && !hit[i].collider.isTrigger)
                {
                    flag = false;
                }
                //print(hit[i].collider.gameObject.name);
            }

            /*if (hit.collider == null || hit.collider.isTrigger)
            {
                transform.Translate(new Vector2(velocidad, 0f));
                //transform.position += new Vector3(distancia, 0f, 0f);
            }*/

        }
        else if (dir == 'a')
        {
            hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, -transform.right, distancia);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.gameObject != this.gameObject && !hit[i].collider.isTrigger)
                {
                    flag = false;
                }
                //print(hit[i].collider.gameObject.name);
            }

            /*if (hit.collider == null || hit.collider.isTrigger)
            {
                transform.Translate(new Vector2(-velocidad, 0f));
                //transform.position += new Vector3(-distancia, 0f, 0f);
            }*/

        }
        else if (dir == 's')
        {
            //print("hola");
            hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, -transform.up, distancia);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.gameObject != this.gameObject && !hit[i].collider.isTrigger)
                {
                    flag = false;
                    print("Falso");
                }
                //print(hit[i].collider.gameObject.name);
            }

        }
        else if (dir == 'w')
        {
            hit = Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y), radio, transform.up, distancia);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.gameObject != this.gameObject && !hit[i].collider.isTrigger)
                {
                    flag = false;
                }
                //print(hit[i].collider.gameObject.name);
            }
           /* if (hit.collider == null || hit.collider.isTrigger)
            {
                print("translate");
                transform.Translate(new Vector2(0f, velocidad));
                //transform.position += new Vector3(0f, distancia, 0f);
            }*/

        }

        if (flag)
        {
            switch (dir)
            {
                case 'w':
                    transform.Translate(new Vector2(0f, velocidad));
                    break;
                case 'a':
                    transform.Translate(new Vector2(-velocidad, 0f));
                    break;
                case 'd':
                    transform.Translate(new Vector2(velocidad, 0f));
                    break;
                case 's':
                    transform.Translate(new Vector2(0f, -velocidad));
                    break;
            }
        }

        /*if(sonido != null && cont == 0)
        {
            sonido.Play();
            cont++;
            Invoke("Sound", 3.2f);
        }*/

    }

   /* void Sound()
    {
        cont = 0;      //no mola mucho
    }*/
}