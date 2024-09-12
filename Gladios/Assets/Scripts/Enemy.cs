using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemySO enemyInfo;
    int enemyHealth;
    private void Start()
    {
        enemyHealth = enemyInfo.enemyMaxHealth;
    }
    
}
