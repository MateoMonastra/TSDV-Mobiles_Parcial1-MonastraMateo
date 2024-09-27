using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    private static InputManager _instance;

    public static InputManager GetInstance()
    {
        if (_instance != null)
        {
            return _instance;
        }

        _instance = new InputManager();
        return _instance;
    }

    private Dictionary<string, float> _axisValues = new Dictionary<string, float>();

    public void SetAxis(string axisName, float value)
    {
        _axisValues.TryAdd(axisName, value);

        _axisValues[axisName] = value;
    }

    public float GetAxis(string inputName)
    {
#if UNITY_EDITOR
        return GetOrAddAxis(inputName) + Input.GetAxis(inputName);
#endif
#if UNITY_ANDROID || UNITY_IOS
        return GetOrAddAxis(inputName);
#endif
    }

    public float GetOrAddAxis(string axisName)
    {
        _axisValues.TryAdd(axisName, 0);
        return _axisValues[axisName];
    }
}