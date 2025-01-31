using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Earthquake Skill", menuName = "Skills/Area/Earthquake")]
public class Earthquake : AreaMeleeAbility
{
    protected override void PerformAreaAttack(GameObject user)
    {
        Debug.Log($"{abilityName} deals {damage} damage in {areaRadius} radius.");
        // Burada alan etkisi uygulanabilir, örneðin etrafýndaki düþmanlara hasar vermek
        Collider[] hitColliders = Physics.OverlapSphere(user.transform.position, areaRadius);
        foreach (var hitCollider in hitColliders)
        {
            // Düþmana hasar uygulama kodu burada olacak
            Debug.Log($"Hit {hitCollider.name} within radius.");
        }
    }
}
