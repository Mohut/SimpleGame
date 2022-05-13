using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pill;
    [SerializeField] private float spawnTime;
    private List<GameObject> pills;
    private bool active;
    private Vector2 spawnPosition;

    private void Start()
    {
        EventManager.GameStartEvent += SetActive;
        EventManager.GameOverEvent += SetNotActive;
        EventManager.GameOverEvent += DeleteAllPills;

        pills = new List<GameObject>();
        InvokeRepeating(nameof(SpawnPill), spawnTime, spawnTime);
    }

    private void OnDestroy()
    {
        EventManager.GameStartEvent -= SetActive;
        EventManager.GameOverEvent -= SetNotActive;
        EventManager.GameOverEvent -= DeleteAllPills;
    }

    private void SpawnPill()
    {
        if (active == false)
            return;
        
        spawnPosition.x = Random.Range(-7f, 7f);
        spawnPosition.y = Random.Range(-3f, 3f);
        
        GameObject newPill = Instantiate(pill, spawnPosition, Quaternion.identity);
        pills.Add(newPill);
    }

    private void DeleteAllPills()
    {
        for (int i = 0; i < pills.Count; i++)
            Destroy(pills[i]);
    }

    private void SetActive()
    {
        active = true;
    }

    private void SetNotActive()
    {
        active = false;
    }
}
