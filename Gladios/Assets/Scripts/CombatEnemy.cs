using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnemy : MonoBehaviour
{
    [SerializeField] float hammerDamageRadius;
    [SerializeField] float delayTime;
    Enemy enemy;
    Animator animator;
    public bool isHit;
    
    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        animator = GetComponentInParent<Animator>();
    }
    private void Update()
    {
       
        if (enemy.weaponList[2].activeSelf)
        {
            hammerDamageRadius = 5;
            delayTime = .4f;
            Collider[] hammerDamage = Physics.OverlapSphere(transform.position, hammerDamageRadius);
            foreach (Collider hitCollider in hammerDamage)
            {
                if (hitCollider.gameObject.CompareTag("MainCha"))
                {
                    Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                    Animator animatorHit = hitCollider.GetComponent<Animator>();
                    if (animator!=null)
                    {
                        if (!animatorHit.GetCurrentAnimatorStateInfo(0).IsName("Death")
                        && animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack1"))
                        {
                            if (!isHit)
                            {
                                StartCoroutine(AttackDelay(rb, animatorHit, hitCollider));

                            }
                        }
                    }
                    
                }


            }
        }
    }
    IEnumerator AttackDelay(Rigidbody rb, Animator animatorHit, Collider hitCollider)
    {
        float randomKnockBack = Random.Range(10, 16) * 75;
        isHit = true;
        yield return new WaitForSecondsRealtime(delayTime);
        hitCollider.GetComponentInChildren<Combat>().health -= GetComponentInParent<Enemy>().damage;
        bool areAllAttacksFalse = !animatorHit.GetBool("Attack1") && !animatorHit.GetBool("Attack2") && !animatorHit.GetBool("Attack3");
        if (areAllAttacksFalse)
        {
            animatorHit.SetBool("Hit", true);
            rb.AddExplosionForce(randomKnockBack, transform.position, hammerDamageRadius);
            yield return new WaitForSeconds(.1f);
            animatorHit.SetBool("Hit", false);
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCha") && (enemy.weaponList[1].activeSelf || enemy.weaponList[0].activeSelf))
        {
            Animator animator = other.GetComponent<Animator>();
            if (animator.GetBool("Block"))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                {
                    other.GetComponentInChildren<Combat>().health -= GetComponentInParent<Enemy>().damage / 3;
                }
            }
            else
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                {
                    other.GetComponentInChildren<Combat>().health -= GetComponentInParent<Enemy>().damage;
                }
               
            }

            bool areAllAttacksFalse = !animator.GetBool("Attack1") && !animator.GetBool("Attack2") && !animator.GetBool("Attack3");
            
            if (areAllAttacksFalse)
            {
                animator.SetBool("Hit", true);
                if (animator.GetCurrentAnimatorStateInfo(2).IsName("HitF2"))
                {
                    animator.SetBool("Hit", true);
                    animator.Play("HitF2");
                }
            }
            


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MainCha") && (enemy.weaponList[1].activeSelf || enemy.weaponList[0].activeSelf))
        {
            other.GetComponent<Animator>().SetBool("Hit", false);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hammerDamageRadius);

    }
}
