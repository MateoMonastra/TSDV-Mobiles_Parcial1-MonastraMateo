using System;
using UnityEngine;

public class StartQuarterCollision : MonoBehaviour
{
    public Action OnPlayerCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            OnPlayerCollision?.Invoke();
        }
    }
}
