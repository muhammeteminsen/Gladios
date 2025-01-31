using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New SwordSlash Skill",menuName ="Skills/Meel")]
public class SwordSlashAbility : MeleeAbility
{
    protected override void PerformMeleeAttack(GameObject user)
    {
     
        // Apply damage
        Enemy.takenDamage += ((int)damage);
        Debug.Log($"{abilityName} deals {damage} melee damage.");
        // Additional code for dealing damage
    }

}
