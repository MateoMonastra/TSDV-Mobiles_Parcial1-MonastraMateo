using UnityEngine;
using UnityEngine.Serialization;

public class ControlDireccion : MonoBehaviour
{

    [SerializeField] private int _playerID;
    public bool habilitado = true;
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
        if (habilitado)
        {
            InputManager.GetInstance().GetAxis($"Horizontal{_playerID}");
        }
        
        _carController.SetGiro(_giro);
    }

    public float GetGiro()
    {
        return _giro;
    }
}