using UnityEngine;
using System.Collections;

public class LoopTextura : MonoBehaviour 
{
	public float Intervalo = 1;
	float Tempo = 0;
	
	public Texture2D[] Imagenes;
	public Texture2D[] ImagenesMobile;
	int Contador = 0;

	// Use this for initialization
	void Start () 
	{
#if UNITY_ANDROID || UNITY_IOS
		if(Imagenes.Length > 0)
			GetComponent<Renderer>().material.mainTexture = ImagenesMobile[0];
		
#elif UNITY_STANDALONE
		if(Imagenes.Length > 0)
			GetComponent<Renderer>().material.mainTexture = Imagenes[0];
#endif
	}
	
	// Update is called once per frame
	void Update () 
	{
		Tempo += Time.deltaTime;
		
		if(Tempo >= Intervalo)
		{
			Tempo = 0;
			Contador++;
			
#if UNITY_ANDROID || UNITY_IOS
			if(Contador >= ImagenesMobile.Length)
			{
				Contador = 0;
			}
			GetComponent<Renderer>().material.mainTexture = ImagenesMobile[Contador];
#elif UNITY_STANDALONE
			if(Contador >= Imagenes.Length)
			{
				Contador = 0;
			}
			GetComponent<Renderer>().material.mainTexture = Imagenes[Contador];
#endif
		}
	}
}
