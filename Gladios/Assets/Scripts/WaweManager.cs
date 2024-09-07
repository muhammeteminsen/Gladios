using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class Wave
{
    public List<Enemy> enemies; // D��man listesi
    public float spawnInterval;     // D��manlar�n spawn olma aral��� (iste�e ba�l�)
}
public class WaweManager : MonoBehaviour
{

    public List<Wave> waves;  // Wave listesi
    public Transform spawnPoint;

    private int currentWave = 0;
    public bool waweCanStart;
    int currentWawe = 0;
    private void Start()
    {
        currentWawe = 0;
        waweCanStart = true;
    }
    private void Update()
    {
        if (waweCanStart) 
        {
            waweCanStart=false;
            StartCoroutine(SpawnWawe(currentWawe)); 
        }

    }
    IEnumerator SpawnWawe(int waveIndex)
    {
        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.enemies.Count; i++)
        {
            Instantiate(wave.enemies[i].gameObject, spawnPoint.position, Quaternion.identity); // D��man� spawn et
            spawnPoint.position += new Vector3(2f, 0, 0); // Spawn pozisyonunu sa�a kayd�r
            yield return new WaitForSeconds(wave.spawnInterval); // Spawn aral���n� bekle
        }

        currentWave++;
        
    }
}
