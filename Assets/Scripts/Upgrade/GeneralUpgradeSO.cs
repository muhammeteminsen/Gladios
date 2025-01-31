using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName ="GameData/ Upgrade")]
public class GeneralUpgradeSO : ScriptableObject
{
    public int healthBonus;
    public int speedBonus;
    public int damageBonus;

   

    public Sprite selectedSprite;
    
}
