using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "GameData/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy Settings")]
    public int enemyMaxHealth;
    public int enemySpeed;

}

