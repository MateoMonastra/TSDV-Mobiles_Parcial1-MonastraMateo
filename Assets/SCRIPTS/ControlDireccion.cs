using UnityEngine;
using UnityEngine.Serialization;

public class ControlDireccion : MonoBehaviour
{

    [SerializeField] private int playerID = 1;
    public bool Habilitado = true;
    CarController _carController;
    private float _giro = 0;

    //---------------------------------------------------------//

    // Use this for initialization
    void Start()
    {
        _carController = GetComponent<CarController>();
    }
    
    void Update()
    {
        if (Habilitado)
        {
            InputManager.GetInstance().GetAxis($"Horizontal{playerID}");
            Debug.Log("entro");
            Debug.Log(playerID);
        }
        
        _carController.SetGiro(_giro);
    }

    public float GetGiro()
    {
        return _giro;
    }
}