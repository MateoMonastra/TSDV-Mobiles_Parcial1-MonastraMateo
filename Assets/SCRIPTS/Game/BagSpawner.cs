using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSpawner : MonoBehaviour
{
    [SerializeField] private List<Bolsa> bags;

    private void Start()
    {
        TurnOffAllBags();
    }

    public void SpawnBag(Vector3 newPosition)
    {
        bool spawned = false;
        
        foreach (var bag in bags)
        {
            if (!bag.isActive && !spawned)
            {
                bag.gameObject.SetActive(true);
                bag.isActive = true;
                bag.gameObject.transform.position = newPosition;
                spawned = true;
            }
        }
    }
    
    private void TurnOffAllBags()
    {
        foreach (var bag in bags)
        {
            bag.transform.position = Vector3.zero;
            bag.isActive = false;
        }
    }
    public void DespawnBag(Transform bagToDespawn)
    {
        foreach (var bag in bags)
        {
            if (Mathf.Approximately(bag.transform.position.z, bagToDespawn.position.z))
            {
                bag.transform.position = Vector3.zero;
                bag.isActive = false;
            }
        }
    }
}
