using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New SwordSlash Skill",menuName ="Skills/Meel")]
public class SwordSlashAbility : MeleeAbility
{
    protected override void PerformMeleeAttack(GameObject user)
    {
        // Yak�n d�v�� sald�r�s�n�n uygulanaca�� kod
        Debug.Log($"{abilityName} deals {damage} melee damage.");
        // Burada d��manlara hasar verebilirsin
    }
}
