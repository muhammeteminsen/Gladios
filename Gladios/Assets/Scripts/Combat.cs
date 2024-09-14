using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Combat : MonoBehaviour
{
    [SerializeField] float hammerDamageRadius;
    AnimationController controller;
    [SerializeField] float delayTime;
    Animator animator;
    Rigidbody rb;
    public int health = 100;
    bool isHit;
    public static bool attack;
    public bool isDead;


    private void Start()
    {
        controller = GetComponentInParent<AnimationController>();
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody>();

    }
    private void Update()
    {
        Debug.LogWarning(health);
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(MouseDown());
        }

        if (health <= 0)
        {

            isDead = true;

            for (int i = 0; i < animator.parameterCount; i++)
            {
                AnimatorControllerParameter parameter = animator.GetParameter(i);
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    animator.SetBool(parameter.name, false);
                }
            }
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                animator.Play("Death");
            }

        }
        else
        {
            isDead = false;
        }


        //Hammer
        Collider[] hammerDamage = Physics.OverlapSphere(transform.position, hammerDamageRadius);
        foreach (Collider hitCollider in hammerDamage)
        {
            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
            if (controller.weapons[2].activeSelf && Input.GetMouseButtonDown(0) && hitCollider.gameObject.CompareTag("Enemy") && hitCollider.GetComponent<Enemy>().health > 0)
            {
                StartCoroutine(AttackDelay(hitCollider, rb));
            }

        }

    }
    //Hammer
    IEnumerator AttackDelay(Collider hitCollider, Rigidbody rb)
    {
        yield return new WaitForSecondsRealtime(delayTime);
        float randomKnockBack = Random.Range(10, 16) * 75;
        rb.AddExplosionForce(randomKnockBack, transform.position, hammerDamageRadius);
        hitCollider.GetComponent<AIController>().navMesh.isStopped = true;
        hitCollider.GetComponent<Enemy>().health -= hitCollider.GetComponent<Enemy>().takenDamage;
        hitCollider.GetComponent<Animator>().Play("Hit_F_1_InPlace");
        yield return new WaitForSecondsRealtime(.1f);
        hitCollider.GetComponent<Animator>().SetBool("Hit", false);

    }

    IEnumerator MouseDown()
    {
        attack = true;
        yield return new WaitForSecondsRealtime(delayTime);
        attack = false;
    }
    //Axe and Sword
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isHit && attack
            && (controller.weapons[0].activeSelf
            || controller.weapons[1].activeSelf))
        {
            StartCoroutine(OneAttackDelay(other));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && (controller.weapons[0].activeSelf || controller.weapons[1].activeSelf))
        {
            attack = false;
            if (other.GetComponent<Enemy>().health > 0)
            {
                StartCoroutine(HitDelay(other));
            }
        }
    }
    IEnumerator HitDelay(Collider other)
    {
        yield return new WaitForSecondsRealtime(delayTime);

        if (!other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("StunnedLoop"))
        {
            other.GetComponent<Animator>().SetBool("Hit", false);
        }


    }
    IEnumerator OneAttackDelay(Collider other)
    {
        Animator otherAnim = other.GetComponent<Animator>();
        isHit = true;
        if (otherAnim.GetCurrentAnimatorStateInfo(0).IsName("StunnedLoop") ||
            otherAnim.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack1"))
        {
            switch (other.GetComponent<Enemy>().enemyInfo.characterType)
            {
                case ChaType.Classic:
                    otherAnim.SetBool("Attack", false);
                    break;
                case ChaType.Heavy:
                    otherAnim.SetBool("Attack2", false);
                    break;
                case ChaType.Fast:
                    otherAnim.SetBool("Attack3", false);
                    break;
            }
            otherAnim.Play("Hit_F_1_InPlace");
        }
        other.GetComponent<Rigidbody>().velocity += Camera.main.transform.forward * 5;
        other.GetComponent<Animator>().SetBool("Hit", true);
        if (controller.weapons[0].activeSelf)
        {
            otherAnim.SetBool("Stunned", true);
        }
        yield return new WaitForSecondsRealtime(delayTime);
        isHit = false;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hammerDamageRadius);

    }
}
