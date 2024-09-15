using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : ScriptableObject
{
    public string abilityName;  
    public Sprite abilitySprite;
    public int cooldownTime;
    public float manaCost;
    public bool isOnCooldown = false;
    public KeyCode activateKey;
    public abstract void Activate(GameObject user);
    public IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }
}
public abstract class MeleeAbility : BaseAbility
{
    public int damage;
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