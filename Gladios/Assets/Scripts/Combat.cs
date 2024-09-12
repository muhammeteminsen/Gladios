using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] float hammerDamageRadius;
    AnimationController controller;
    [SerializeField] float delayTime;
    bool isHit;
    public static bool attack;
    private void Start()
    {
        controller = GetComponentInParent<AnimationController>();
        
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(MouseDown());
        }
        //Hammer

        Collider[] hammerDamage = Physics.OverlapSphere(transform.position, hammerDamageRadius);
        foreach (Collider hitCollider in hammerDamage)
        {
            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
            if (controller.weapons[2].activeSelf && Input.GetMouseButtonDown(0) && hitCollider.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(AttackDelay(hitCollider, rb));
            }
        }

    }
    //Hammer
    IEnumerator AttackDelay(Collider hitCollider, Rigidbody rb)
    {
        yield return new WaitForSecondsRealtime(delayTime);
        float randomKnockBack = Random.Range(10, 16) * 40;
        rb.AddExplosionForce(randomKnockBack, transform.position, hammerDamageRadius);
        hitCollider.GetComponent<Animator>().SetBool("Hit", true);
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
        if (other.gameObject.CompareTag("Enemy") && !isHit && attack && (controller.weapons[0].activeSelf || controller.weapons[1].activeSelf))
        {
            StartCoroutine(OneAttackDelay(other));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && (controller.weapons[0].activeSelf || controller.weapons[1].activeSelf))
        {
            attack = false;
            StartCoroutine(HitDelay(other));
            
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

        isHit = true;
        if (other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("StunnedLoop") ||
            other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("EnemyAttack1"))
        {
            other.GetComponent<Animator>().SetBool("Attack", false);
            other.GetComponent<Animator>().Play("Hit_F_1_InPlace");
        }
        other.GetComponent<Rigidbody>().velocity += Camera.main.transform.forward * 5;
        other.GetComponent<Animator>().SetBool("Hit", true);
        if (controller.weapons[0].activeSelf)
        {
            other.GetComponent<Animator>().SetBool("Stunned", true);
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
