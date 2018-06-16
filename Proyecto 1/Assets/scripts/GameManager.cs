//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    bool JuegoIniciado=false;
    public PlayerMovement Mov;
    public Renderer r;
    public float tiempoRespawn;
    public static GameManager instance;
    //Tapas
    [HideInInspector] public GameObject[][] Tapas = new GameObject[2][];
    public GameObject TapaRespawn;
    public GameObject[] Tapas0;
    public GameObject[] Tapas1;
    //bool pistola = false;
    bool tarjeta = false;
    bool tarjetaesp = false;
    public float vida = 7,
	vidaRegenera,							//Cantidad de vida que regenera
	tiempoInicioRegen,						//Tiempo que tarda en iniciarse la regeneración de la vida del jugador
	tiempoRegen;							//Cada cuanto tiempo se regenera la vida del jugador
    public float energia = 10;
    public GameObject[] respawns;
    public GameObject lleno, menos1, menos2, menos3, menos4, menos5, menos6, vacio;
    public GameObject player;
    [HideInInspector]
    public int NumRespawn = 0;
    [HideInInspector]
    public int PisoActual = 0;


    Controles ctrl;

    // ----------UI
    public GameObject panel;
    public Text texto;
    [HideInInspector]
    public float VidaInicial, NrgInicial;
    public Scrollbar BarraEnergia;
    // ------Mapa
    public Image[][] Mapas = new Image[2][];
    public Image[] Piso1, Piso2;
    [HideInInspector] public bool MapActivo = false;
    public Image backdrop;
    //[HideInInspector]
    public int HabActual = 0;
    bool MenuActivo = false;
    public AudioClip[] SonidoAActivar;
    private AudioSource source;
    public float Volume = .5f;
    //nivel máximo de radiacion
    public float nivelMaxRadiacion = 0.6f;
    float radiacion = 0;
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        Mapas[0] = Piso1;
        Mapas[1] = Piso2;

        Color c;

        //Mapa
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < Mapas[i].Length; j++)
            {
                c = Mapas[i][j].color;
                c.a = 0;
                Mapas[i][j].color = c;
            }
        }
        c = backdrop.color;
        c.a = 0f;
        backdrop.color = c;
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        Tapas[0] = Tapas0;
        Tapas[1] = Tapas1;
        HabActual = 0;
        //nivel de radiacion
        r.gameObject.SetActive(true);
        InvokeRepeating("AumentoRadiacion", 300, 300);
        Color c = r.material.color;
        c.a = 0f;
        r.material.color = c;

        //UI
        VidaInicial = vida;
        NrgInicial = energia;
        lleno.SetActive(true);
        menos1.SetActive(false);
        menos2.SetActive(false);
        menos3.SetActive(false);
        menos4.SetActive(false);
        menos5.SetActive(false);
        menos6.SetActive(false);
        vacio.SetActive(false);
        GameManager.instance.panel.SetActive(false);
        UpdateUI(vida, energia);
        PlayMusic(0);

    }

    public void Respawn()
    {

        Debug.Log("Respawning");
        player.transform.position = respawns[PisoActual].transform.position;
        player.GetComponent<SpriteRenderer>().enabled = true;
        //animacion
        //player.SendMessage("TeHasMuerto", false,, SendMessageOptions.DontRequireReceiver);
        player.GetComponent<PlayerMovement>().TeHasMuerto(false, ' ');     
        vida = VidaInicial;
        energia = NrgInicial;
        UpdateUI(vida, energia);
        //TapaRespawn.SetActive(false);

        for (int i = 0; i < Tapas[PisoActual].Length; i++)
        {
            Tapas[PisoActual][i].SetActive(true);
        }
        Tapas[PisoActual][0].SetActive(false);//Tapa 0 es la tapa del respawn

        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerMovement>().puedeMorir = true;
        DontDestroyOnLoad(player);
    }
    public void takeDamage(float dmg) {
        //Debug.Log("Hola kk");
        vida = vida - dmg;
		CancelInvoke ();
		//InvokeRepeating ("Regenera", tiempoInicioRegen, tiempoRegen);
        UpdateUI(vida, energia);
        //TxtVida.text = (Vda + "/100"); necesario para el HUD cuando esté listo
        if (vida <= 0) {
            Invoke("Respawn", tiempoRespawn);
            Debug.Log("RLLY?");
            player.GetComponent<PlayerMovement>().enabled = false;
            
        }
    }
	//Regeneración de la vida del jugador con el tiempo
	void Regenera () {
		//Debug.Log ("Tiempo: " + Time.time);
		if (vida < VidaInicial) {
			vida += vidaRegenera * Time.deltaTime;
			//Debug.Log ("Vida jugador regenerada. Vida actual: " + vida);
		}
        else if(vida > VidaInicial)
        {
            vida = VidaInicial;
        }

		Debug.Log ("Tiempo: " + Time.time);

	}
    void CogerPistola()
    {

        //player.GetComponent<Disparar>().enabled = true;

    }
    void CogerTarjeta()
    {
        tarjeta = true;
    }
    void CogerTarjetaEsp()
    {
        tarjetaesp = true;
    }

    public void CambioPiso(string SiguienteNivel)
    {
        //Application.LoadLevel(SiguienteNivel); OBSOLETO?
        SceneManager.LoadScene(SiguienteNivel);
    }

    //pruebas del HUD, puede modificarse
    public void VariacionEnergia(int nivel)
    {
        energia += nivel;

        if(energia > NrgInicial)
        {
            energia = NrgInicial;
        }
        else if(energia < 0)
        {
            energia = 0;
        }

        UpdateUI(vida, energia);
    }
    public void UpdateUI(float vida, float energia)
    {
        //comprobacion de la vida del jugador
        if (vida >= VidaInicial)
        {
            lleno.SetActive(true);
            menos1.SetActive(false);
            menos2.SetActive(false);
            menos3.SetActive(false);
            menos4.SetActive(false);
            menos5.SetActive(false);
            menos6.SetActive(false);
            vacio.SetActive(false);
        }
        else if (vida >= VidaInicial - VidaInicial / 7)
        {
            menos1.SetActive(true);
            lleno.SetActive(false);
            menos2.SetActive(false);
            menos3.SetActive(false);
            menos4.SetActive(false);
            menos5.SetActive(false);
            menos6.SetActive(false);
            vacio.SetActive(false);
        }
        else if (vida >= VidaInicial - 2 * VidaInicial / 7)
        {
            menos2.SetActive(true);
            menos1.SetActive(false);
            lleno.SetActive(false);
            menos3.SetActive(false);
            menos4.SetActive(false);
            menos5.SetActive(false);
            menos6.SetActive(false);
            vacio.SetActive(false);
        }
        else if (vida >= VidaInicial - 3 * VidaInicial / 7)
        {
            menos3.SetActive(true);
            menos1.SetActive(false);
            menos2.SetActive(false);
            lleno.SetActive(false);
            menos4.SetActive(false);
            menos5.SetActive(false);
            menos6.SetActive(false);
            vacio.SetActive(false);
        }
        else if (vida >= VidaInicial - 4 * VidaInicial / 7)
        {
            menos4.SetActive(true);
            menos1.SetActive(false);
            menos2.SetActive(false);
            menos3.SetActive(false);
            lleno.SetActive(false);
            menos5.SetActive(false);
            menos6.SetActive(false);
            vacio.SetActive(false);
        }
        else if (vida >= VidaInicial - 5 * VidaInicial / 7)
        {
            menos5.SetActive(true);
            menos1.SetActive(false);
            menos2.SetActive(false);
            menos3.SetActive(false);
            menos4.SetActive(false);
            lleno.SetActive(false);
            menos6.SetActive(false);
            vacio.SetActive(false);
        }
        else if (vida >= VidaInicial - 6 * VidaInicial / 7)
        {
            menos6.SetActive(true);
            menos1.SetActive(false);
            menos2.SetActive(false);
            menos3.SetActive(false);
            menos4.SetActive(false);
            menos5.SetActive(false);
            lleno.SetActive(false);
            vacio.SetActive(false);
        }
        else
        {
            vacio.SetActive(true);
            menos1.SetActive(false);
            menos2.SetActive(false);
            menos3.SetActive(false);
            menos4.SetActive(false);
            menos5.SetActive(false);
            menos6.SetActive(false);
            lleno.SetActive(false);
        }

        //energia
        BarraEnergia.size = energia / 10f;
    }

    void AumentoRadiacion() 
    {
        if (radiacion >= nivelMaxRadiacion) 
        {
            Debug.Log("GameOver");
        }
        radiacion += 0.1f;
        Debug.Log(radiacion);
        Color c = r.material.color;
        c.a = radiacion;
        r.material.color = c;
        //Camera.main.backgroundColor = new Color(0, radiacion, 0);
    
    }

   public void Mapa() //Hay que llamarlo en algun sitio
    {       
        Color c;
            Debug.Log("M");
            MapActivo = !MapActivo;
        print(PisoActual);
            if (MapActivo &&  PisoActual >=0  && PisoActual < 2)
            {
            print("pisoactual");
                for(int i = 0; i < Mapas[PisoActual].Length; i++)
                {
                    if (i != HabActual)//Asumo que empezaremos a numerar las hab desde 1 y no desde 0
                    {
                        c = Mapas[PisoActual][i].color;
                        c.a = .66f;
                        Mapas[PisoActual][i].color = c;
                    }
                    else
                    {
                        c = Mapas[PisoActual][i].color;
                        c.a = 1f;
                        Mapas[PisoActual][i].color = c;
                    }
                }
                c = backdrop.color;
                c.a = 1f;
                backdrop.color = c;
             }
             else
                 {
            for (int i = 0; i < Mapas[PisoActual].Length; i++)
            {
                c = Mapas[PisoActual][i].color;
                c.a = 0;
                Mapas[PisoActual][i].color = c;

            }
            c = backdrop.color;
            c.a = 0f;
            backdrop.color = c;
        }

    }

     public void Menu() 
      {
        Color c;
          if (Input.GetKeyDown(KeyCode.Escape) && 0 >= PisoActual && PisoActual < 2)
          {
            ctrl = FindObjectOfType<Controles>();
            if (ctrl != null && ctrl.gameObject.GetComponent<Image>().color.a != 0f) 
            {
                print("Null");
                c = ctrl.gameObject.GetComponent<Image>().color;
                c.a = 0;
                ctrl.gameObject.GetComponent<Image>().color = c;
                //ctrl.gameObject.layer = 0;
            }
            else
            {
                MenuActivo = !MenuActivo;
                if (MenuActivo)
                {
                    Debug.Log("MenuActivo");
                    //Mov.enabled = false;
                    SceneManager.LoadScene("Menu", LoadSceneMode.Additive);

                }
                else
                {
                    Debug.Log("Desactivando el menu");
                    SceneManager.UnloadSceneAsync("Menu");
                    //Mov.enabled = true;
                }
            }
        }      
    }

    public void Cargar()
    {
        SceneManager.UnloadSceneAsync("Menu");
        MenuActivo = false;
    }

    public void Salir()
    {
        Application.Quit();
    }
    
    public void Reiniciar()
    {
        if (PisoActual == 1)
        {
            SceneManager.LoadScene("Piso1", LoadSceneMode.Single);
            MenuActivo = false;
        }
        else
        {
            SceneManager.LoadScene("Piso0", LoadSceneMode.Single);
            MenuActivo = false;
        }

            
    }

    public void PlayMusic(int i)
    {
        source.loop = true;
        source.clip = SonidoAActivar[i];
        source.Play();

    }
    public void StopMusic()
    {

        source.Stop();

    }


}
