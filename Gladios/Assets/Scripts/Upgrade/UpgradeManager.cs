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
    // Silah yetenek Listesi ayrý ayrý Class kulanarak
    [SerializeField] List<WeaponSkills> weaponsSkils;
    // Silah  listesi
    // seçilmiþ 3 yetenek listesi
    
    // oyuncu bir güçlendirme seçtikten sonra onun uygulanacaðý fonksiyon
}
