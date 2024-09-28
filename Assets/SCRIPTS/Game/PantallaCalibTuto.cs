using UnityEngine;
using System.Collections;

public class PantallaCalibTuto : MonoBehaviour
{
    public Texture2D[] ImagenesDelTuto;
    public Texture2D[] ImagenesDelTutoMobile;
    public float Intervalo = 1.2f; //tiempo de cada cuanto cambia de imagen
    float TempoIntTuto = 0;
    int EnCursoTuto = 0;

    public Texture2D[] ImagenesDeCalib;
    public Texture2D[] ImagenesDeCalibMobile;
    int EnCursoCalib = 0;
    float TempoIntCalib = 0;

    public Texture2D ImaReady;

    public ContrCalibracion ContrCalib;

    // Update is called once per frame
    void Update()
    {
        switch (ContrCalib.EstAct)
        {
            case ContrCalibracion.Estados.Calibrando:
                //pongase en posicion para iniciar
                TempoIntCalib += T.GetDT();
                if (TempoIntCalib >= Intervalo)
                {
                    TempoIntCalib = 0;
#if UNITY_ANDROID || UNITY_IOS
if (EnCursoCalib + 1 < ImagenesDeCalibMobile.Length)
                        EnCursoCalib++;
                    else
                        EnCursoCalib = 0;
#elif UNITY_STANDALONE
                    if (EnCursoCalib + 1 < ImagenesDeCalib.Length)
                        EnCursoCalib++;
                    else
                        EnCursoCalib = 0;
#endif
                }
#if UNITY_ANDROID || UNITY_IOS
                GetComponent<Renderer>().material.mainTexture = ImagenesDeCalibMobile[EnCursoCalib];
#elif UNITY_STANDALONE
                GetComponent<Renderer>().material.mainTexture = ImagenesDeCalib[EnCursoCalib];
#endif
                

                break;

            case ContrCalibracion.Estados.Tutorial:
                //tome la bolsa y depositela en el estante
                TempoIntTuto += T.GetDT();
                if (TempoIntTuto >= Intervalo)
                {
                    TempoIntTuto = 0;
#if UNITY_ANDROID || UNITY_IOS
				if(EnCursoTuto + 1 < ImagenesDelTutoMobile.Length)
					EnCursoTuto++;
				else
					EnCursoTuto = 0;
#elif UNITY_STANDALONE
                    if (EnCursoTuto + 1 < ImagenesDelTuto.Length)
                        EnCursoTuto++;
                    else
                        EnCursoTuto = 0;
#endif
                }
#if UNITY_ANDROID || UNITY_IOS
			GetComponent<Renderer>().material.mainTexture = ImagenesDelTutoMobile[EnCursoTuto];
#elif UNITY_STANDALONE
                GetComponent<Renderer>().material.mainTexture = ImagenesDelTuto[EnCursoTuto];
#endif

                break;

            case ContrCalibracion.Estados.Finalizado:
                //esperando al otro jugador		
                GetComponent<Renderer>().material.mainTexture = ImaReady;

                break;
        }
    }
}