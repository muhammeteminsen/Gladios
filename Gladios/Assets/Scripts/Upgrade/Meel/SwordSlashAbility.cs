using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New SwordSlash Skill",menuName ="Skills/Meel")]
public class SwordSlashAbility : MeleeAbility
{
    protected override void PerformMeleeAttack(GameObject user)
    {
        // Yakýn dövüþ saldýrýsýnýn uygulanacaðý kod
        Debug.Log($"{abilityName} deals {damage} melee damage.");
        // Burada düþmanlara hasar verebilirsin
    }
}
