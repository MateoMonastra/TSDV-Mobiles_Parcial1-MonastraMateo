using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using EventSystems.EventSceneManager;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private static GameManager _instancia;
    
    [SerializeField] private FinalScore finalScore;
    [SerializeField] private string nextScene = "PtsFinal";
    [SerializeField]private EventChannelSceneManager eventChannelSceneManager;

    public float TiempoDeJuego = 60;

    public GameSettings gameSettings;

    public enum EstadoJuego
    {
        Calibrando,
        Jugando,
        Finalizado
    }

    public EstadoJuego EstAct = EstadoJuego.Calibrando;

    public Player Player1;
    public Player Player2;

    bool ConteoRedresivo = true;
    public Rect ConteoPosEsc;
    public float ConteoParaInicion = 3;
    public Text ConteoInicio;
    public Text TiempoDeJuegoText;

    public float TiempEspMuestraPts = 3;

    //posiciones de los camiones dependientes del lado que les toco en la pantalla
    //la pos 0 es para la izquierda y la 1 para la derecha
    public Vector3[] PosCamionesCarrera = new Vector3[2];

    //posiciones de los camiones para el tutorial
    public Vector3 PosCamion1Tuto = Vector3.zero;
    public Vector3 PosCamion2Tuto = Vector3.zero;

    //listas de GO que activa y desactiva por sub-escena
    //escena de tutorial
    public GameObject[] ObjsCalibracion1;

    public GameObject[] ObjsCalibracion2;

    //la pista de carreras
    public GameObject[] ObjsCarrera;

    public Vector3 currentLastPlace;

    

    //--------------------------------------------------------//

    void Awake()
    {
        if (_instancia == null)
        {
            GameManager._instancia = this;
        }
        else if (_instancia != this)
        {
            // Si ya existe una instancia y no es esta, se destruye el objeto duplicado
            Destroy(gameObject);
        }
    }

    IEnumerator Start()
    {
        yield return null;
        IniciarTutorial();
    }

    private void OnDestroy()
    {
        GameManager._instancia = null;
    }

    public static GameManager GetInstance()
    {
        if (GameManager._instancia == null)
        {
            GameManager._instancia = FindObjectOfType<GameManager>();
        }

        return GameManager._instancia;
    }

    void Update()
    {
        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (EstAct)
        {
            case EstadoJuego.Calibrando:

                if (InputManager.GetInstance().GetAxis("Vertical1") > 0)
                {
                    Player1.Seleccionado = true;
                }

                if (InputManager.GetInstance().GetAxis("Vertical2") > 0)
                {
                    Player2.Seleccionado = true;
                }

                break;


            case EstadoJuego.Jugando:

                //SKIP LA CARRERA
                if (Input.GetKey(KeyCode.Alpha9))
                {
                    TiempoDeJuego = 0;
                }

                if (TiempoDeJuego <= 0)
                {
                    FinalizarCarrera();
                }

                if (ConteoRedresivo)
                {
                    ConteoParaInicion -= T.GetDT();
                    if (ConteoParaInicion < 0)
                    {
                        EmpezarCarrera();
                        ConteoRedresivo = false;
                    }
                }
                else
                {
                    //baja el tiempo del juego
                    TiempoDeJuego -= T.GetDT();
                }

                if (ConteoRedresivo)
                {
                    if (ConteoParaInicion > 1)
                    {
                        ConteoInicio.text = ConteoParaInicion.ToString("0");
                    }
                    else
                    {
                        ConteoInicio.text = "GO";
                    }
                }

                ConteoInicio.gameObject.SetActive(ConteoRedresivo);

                TiempoDeJuegoText.text = TiempoDeJuego.ToString("00");

                if (gameSettings.PlayerCount == 2)
                {
                    CheckLastPlace();
                }
                else
                {
                    currentLastPlace = currentLastPlace = Player1.transform.position;
                }

                break;

            case EstadoJuego.Finalizado:

                //muestra el puntaje

                TiempEspMuestraPts -= Time.deltaTime;
                if (TiempEspMuestraPts <= 0)
                {
                    eventChannelSceneManager.RemoveScene(gameObject.scene.name);
                    eventChannelSceneManager.AddScene(nextScene);
                    
                }

                break;
        }

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(EstAct == EstadoJuego.Jugando && !ConteoRedresivo);
    }

    //----------------------------------------------------------//

    public void IniciarTutorial()
    {
        if (gameSettings.PlayerCount == 2)
        {
            for (int i = 0; i < ObjsCalibracion1.Length; i++)
            {
                ObjsCalibracion1[i].SetActive(true);
                ObjsCalibracion2[i].SetActive(true);
            }


            Player1.CambiarATutorial();
            Player2.CambiarATutorial();
        }
        else
        {
            for (int i = 0; i < ObjsCalibracion1.Length; i++)
            {
                ObjsCalibracion1[i].SetActive(true);
            }

            Player1.CambiarATutorial();
        }

        for (int i = 0; i < ObjsCarrera.Length; i++)
        {
            ObjsCarrera[i].SetActive(false);
        }

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        ConteoInicio.gameObject.SetActive(false);
    }

    void EmpezarCarrera()
    {
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = true;

        Player2.GetComponent<Frenado>().RestaurarVel();
        Player2.GetComponent<ControlDireccion>().Habilitado = true;
    }

    void FinalizarCarrera()
    {
        EstAct = GameManager.EstadoJuego.Finalizado;

        TiempoDeJuego = 0;

        finalScore.SetPlayer1Score(Player1.Dinero);
        finalScore.SetPlayer2Score(Player2.Dinero);

        Player1.GetComponent<Frenado>().Frenar();
        Player2.GetComponent<Frenado>().Frenar();

        Player1.ContrDesc.FinDelJuego();
        Player2.ContrDesc.FinDelJuego();
    }
    
    //cambia a modo de carrera
    void CambiarACarrera()
    {
        EstAct = GameManager.EstadoJuego.Jugando;

        for (int i = 0; i < ObjsCarrera.Length; i++)
        {
            ObjsCarrera[i].SetActive(true);
        }

        if (gameSettings.PlayerCount == 2)
        {
            //desactivacion de la calibracion
            Player1.FinCalibrado = true;

            for (int i = 0; i < ObjsCalibracion1.Length; i++)
            {
                ObjsCalibracion1[i].SetActive(false);
            }

            Player2.FinCalibrado = true;

            for (int i = 0; i < ObjsCalibracion2.Length; i++)
            {
                ObjsCalibracion2[i].SetActive(false);
            }


            //posiciona los camiones dependiendo de que lado de la pantalla esten
            if (Player1.LadoActual == Visualizacion.Lado.Izq)
            {
                Player1.gameObject.transform.position = PosCamionesCarrera[0];
                Player2.gameObject.transform.position = PosCamionesCarrera[1];
            }
            else
            {
                Player1.gameObject.transform.position = PosCamionesCarrera[1];
                Player2.gameObject.transform.position = PosCamionesCarrera[0];
            }

            Player1.transform.forward = Vector3.forward;
            Player1.GetComponent<Frenado>().Frenar();
            Player1.CambiarAConduccion();

            Player2.transform.forward = Vector3.forward;
            Player2.GetComponent<Frenado>().Frenar();
            Player2.CambiarAConduccion();

            //los deja andando
            Player1.GetComponent<Frenado>().RestaurarVel();
            Player2.GetComponent<Frenado>().RestaurarVel();
            //cancela la direccion
            Player1.GetComponent<ControlDireccion>().Habilitado = false;
            Player2.GetComponent<ControlDireccion>().Habilitado = false;
            //les de direccion
            Player1.transform.forward = Vector3.forward;
            Player2.transform.forward = Vector3.forward;
        }
        else
        {
            //desactivacion de la calibracion
            Player1.FinCalibrado = true;

            for (int i = 0; i < ObjsCalibracion1.Length; i++)
            {
                ObjsCalibracion1[i].SetActive(false);
            }

            //posiciona los camiones dependiendo de que lado de la pantalla esten
            if (Player1.LadoActual == Visualizacion.Lado.Izq)
            {
                Player1.gameObject.transform.position = PosCamionesCarrera[0];
            }
            else
            {
                Player1.gameObject.transform.position = PosCamionesCarrera[1];
            }

            Player1.transform.forward = Vector3.forward;
            Player1.GetComponent<Frenado>().Frenar();
            Player1.CambiarAConduccion();

            //los deja andando
            Player1.GetComponent<Frenado>().RestaurarVel();
            //cancela la direccion
            Player1.GetComponent<ControlDireccion>().Habilitado = false;
            //les de direccion
            Player1.transform.forward = Vector3.forward;
        }

        if (gameSettings.Difficulty == GameSettings.Difficult.NapTime)
        {
            ObjsCarrera[0].SetActive(false);
        }

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        ConteoInicio.gameObject.SetActive(false);
    }

    public void FinCalibracion(int playerID)
    {
        if (gameSettings.PlayerCount == 2)
        {
            if (playerID == 0)
            {
                Player1.FinTuto = true;

            }
            if (playerID == 1)
            {
                Player2.FinTuto = true;
            }

            if (Player1.FinTuto && Player2.FinTuto)
                CambiarACarrera();

        }
        else
        {
            if (playerID == 0)
            {
                CambiarACarrera();

            }
        }
    }

    private void CheckLastPlace()
    {
        if (gameSettings.PlayerCount == 2)
        {
            currentLastPlace = Player1.transform.position;
        }
        else
        {
            currentLastPlace = Player1.transform.position.z > Player2.transform.position.z ? Player2.transform.position : Player1.transform.position;
        }
        
    }
}