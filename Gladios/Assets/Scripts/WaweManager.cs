using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


[Serializable]
public class Wave
{
    public List<Enemy> enemies; // Düþman listesi
    public float spawnInterval;     // Düþmanlarýn spawn olma aralýðý (isteðe baðlý)
}
public class WaweManager : MonoBehaviour
{
    [SerializeField] UpgradeManager upgradeManager;
    public List<Wave> waves;  // Wave listesi
    public Transform spawnPoint;

    
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
        else if (!waweCanStart) 
        {
           upgradeManager.AssignCardSkills();
        }

    }
    IEnumerator SpawnWawe(int waveIndex)
    {
        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.enemies.Count; i++)
        {
            Instantiate(wave.enemies[i].gameObject, spawnPoint.position, Quaternion.identity); // Düþmaný spawn et
            spawnPoint.position += new Vector3(2f, 0, 0); // Spawn pozisyonunu saða kaydýr
            yield return new WaitForSeconds(wave.spawnInterval); // Spawn aralýðýný bekle
        }

        currentWawe++;
        
    }
}
