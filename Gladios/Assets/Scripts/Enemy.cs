using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    public EnemySO enemyInfo;
    public int health;
    public static int takenDamage;
    public int damage;
    Animator animator;
    Rigidbody rb;
    public List<GameObject> weaponList;
    [SerializeField] ParticleSystem deathEffect;
    private void Start()
    {
        health = enemyInfo.enemyMaxHealth;
        takenDamage = enemyInfo.enemyTakenDamage;
        damage = enemyInfo.enemyDamage;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        switch (enemyInfo.weapons)
        {
            case Weapons.Sword:
                foreach (var weapon in weaponList)
                {
                    weapon.SetActive(false);
                }
                weaponList[0].SetActive(true);
                break;
            case Weapons.Axe:
                foreach (var weapon in weaponList)
                {
                    weapon.SetActive(false);
                }
                weaponList[1].SetActive(true);
                break;
            case Weapons.Hammer:
                foreach (var weapon in weaponList)
                {
                    weapon.SetActive(false);
                }
                weaponList[2].SetActive(true);
                break;
            case Weapons.Dagger:
                foreach (var weapon in weaponList)
                {
                    weapon.SetActive(false);
                }
                weaponList[3].SetActive(true);
                break;

        }
    }
    private void Update()
    {
        Debug.Log(health);
        if (health <= 0)
        {
            rb.isKinematic = true;
            for (int i = 0; i < animator.parameterCount; i++)
            {
                AnimatorControllerParameter parameter = animator.GetParameter(i);
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    animator.SetBool(parameter.name, false);
                }
            }
            animator.Play("Death");
            
           
            
        }

        
    }
    public void EffectsDelayFNC()
    {
       
        Destroy(gameObject);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon") && Combat.attack)
        {
            health -= takenDamage;
            GetComponent<CapsuleCollider>().isTrigger = true;
            Combat.attack = false;
        }
    }
    public void HitControlFNC()
    {
        GetComponentInChildren<CombatEnemy>().isHit=false;
        Instantiate(deathEffect, transform.position, transform.rotation, transform);
    }
    public void DeathWeaponLeaveFNC()
    {
        foreach (var weapon in weaponList)
        {
            Rigidbody wprb = weapon.GetComponent<Rigidbody>();
            wprb.isKinematic = false;
            wprb.useGravity = true;
            weapon.GetComponent<MeshCollider>().isTrigger = false;
            weapon.transform.SetParent(null);
        }
    }

}
