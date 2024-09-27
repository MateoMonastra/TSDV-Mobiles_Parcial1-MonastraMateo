using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPullController : MonoBehaviour
{
    [SerializeField] private List<GameObject> firstQuarterBags;
    
    [SerializeField] private StartQuarterCollision secondQuarter;
    [SerializeField] private List<GameObject> secondQuarterBags;
    
    [SerializeField] private StartQuarterCollision thirdQuarter;
    [SerializeField] private List<GameObject> thirdQuarterBags;
    
    [SerializeField] private BagSpawner bagSpawner;


    private void Start()
    {
        foreach (var bagPos in firstQuarterBags)
        {
            bagSpawner.SpawnBag(bagPos.transform.position);
        }
        
        secondQuarter.OnPlayerCollision += InitSecondQuarter;
        thirdQuarter.OnPlayerCollision += InitThirdQuarter;
    }

    private void CheckBagsInQuarter(List<GameObject> quarterBags)
    {
        foreach (var bagPos in quarterBags)
        {
            if (bagPos.transform.position == Vector3.zero) break;
        
            if (bagPos.transform.position.z < GameManager.GetInstance().currentLastPlace.z)
            {
                Debug.Log("DESPAWN");
                bagSpawner.DespawnBag(bagPos.transform);
            }
            else
            {
                Debug.Log($" {bagPos.gameObject}: {bagPos.transform.position}, current place:{GameManager.GetInstance().currentLastPlace}");
            }
        }
    }

    
    private void Update()
    {
        CheckBagsInQuarter(firstQuarterBags);
        CheckBagsInQuarter(secondQuarterBags);
        CheckBagsInQuarter(thirdQuarterBags);
    }

    private void InitSecondQuarter()
    {
        foreach (var bagPos in secondQuarterBags)
        {
            bagSpawner.SpawnBag(bagPos.transform.position);
        }
    }
    
    private void InitThirdQuarter()
    {
        foreach (var bagPos in thirdQuarterBags)
        {
            bagSpawner.SpawnBag(bagPos.transform.position);
        }
    }
    
}
