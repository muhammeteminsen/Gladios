using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemySO enemyInfo;
    int health;
    int takenDamage;
    public int damage;
    Animator animator;
    Rigidbody rb;
    public List<GameObject> weaponList;
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
        
        if (health <= 0)
        {
            for (int i = 0; i < animator.parameterCount; i++)
            {
                AnimatorControllerParameter parameter = animator.GetParameter(i);
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    animator.SetBool(parameter.name, false);
                }
            }
            Destroy(gameObject,5f);
            animator.Play("Death");
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon") && Combat.attack)
        {
            health -= takenDamage;
            GetComponent<CapsuleCollider>().isTrigger = true;
            rb.isKinematic = true;

        }
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
