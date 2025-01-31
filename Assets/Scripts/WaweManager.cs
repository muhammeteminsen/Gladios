using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    public List<Enemy> enemies; // D��man listesi
    public float spawnInterval;     // D��manlar�n spawn olma aral��� (iste�e ba�l�)
}
public class WaweManager : MonoBehaviour
{
    public static WaweManager instance;
    [SerializeField] UpgradeManager upgradeManager;
    public List<Wave> waves;  // Wave listesi
    public Transform spawnPoint;

    public bool waweCanStart;
    int currentWawe = 0;
    private void Awake()
    {
        instance = this;    
    }
    private void Start()
    {
        currentWawe = 0;
        waweCanStart = true;
    }

    private void Update()
    {
        if (waweCanStart)
        {
            waweCanStart = false;
            StartCoroutine(SpawnWawe(currentWawe));
        }
        else if (!waweCanStart && AreEnemiesCleared()) // E�er t�m d��manlar yok olduysa
        {
            upgradeManager.AssignCardSkills();
            upgradeManager.card.upgradeUI.SetActive(true);// Y�kseltmeleri devreye sok
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

        currentWawe++;
    }

    // Bu fonksiyon sahnedeki t�m d��manlar�n yok olup olmad���n� kontrol eder
    private bool AreEnemiesCleared()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); // Sahnede var olan t�m Enemy objelerini bul
        return enemies.Length == 0; // E�er sahnede hi� d��man kalmad�ysa true d�ner

    }
}

