using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    private void Start()
    {
        if (IsServer)
        {
            // Spawn enemies on the server
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            NetworkObject.Spawn(enemy);
        }
    }
}
