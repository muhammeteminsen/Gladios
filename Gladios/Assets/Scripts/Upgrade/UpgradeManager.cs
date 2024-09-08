using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponSkills
{
   public List<BaseAbility> weaponAbilities;
}

public class UpgradeManager : MonoBehaviour
{
    // Genel Yetenk Listesi
    [SerializeField] List<GeneralUpgradeSO> generalUpgrades;
    // Silah yetenek Listesi ayr� ayr� Class kulanarak
    [SerializeField] List<WeaponSkills> weaponsSkils;
    // Silah  listesi
    // se�ilmi� 3 yetenek listesi
    
    // oyuncu bir g��lendirme se�tikten sonra onun uygulanaca�� fonksiyon
}
