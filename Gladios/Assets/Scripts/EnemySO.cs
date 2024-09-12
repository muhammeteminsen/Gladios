using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "GameData/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy Settings")]
    public int enemyMaxHealth;
    public int enemyDamage;
    public int enemyTakenDamage;
    public Weapons weapons;
    public ChaType characterType;
}
public enum Weapons
{
    Sword, Axe, Hammer, Dagger
}

public enum ChaType
{
    Classic, Heavy, Fast
}

