using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : ScriptableObject
{
    public string abilityName;
    public float cooldownTime;
    public float manaCost;

    public abstract void Activate(GameObject user);

}
public abstract class MeleeAbility : BaseAbility
{
    public float damage;
    public float attackRange;

    public override void Activate(GameObject user)
    {
        // Yakýn dövüþ yeteneðinin uygulanacaðý kod
        Debug.Log($"{abilityName} activated by {user.name}");
        PerformMeleeAttack(user);
    }

    protected abstract void PerformMeleeAttack(GameObject user);
}
public abstract class AreaMeleeAbility : MeleeAbility
{
    public float areaRadius;

    protected override void PerformMeleeAttack(GameObject user)
    {
        // Alan hasarý uygulanacaðý kod
        Debug.Log($"{abilityName} (Area) activated by {user.name}");
        PerformAreaAttack(user);
    }

    protected abstract void PerformAreaAttack(GameObject user);
}