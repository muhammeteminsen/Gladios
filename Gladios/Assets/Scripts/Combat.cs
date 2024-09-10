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
    private void Start()
    {
        controller = GetComponentInParent<AnimationController>();
    }
    private void Update()
    {

        Collider[] hammerDamage = Physics.OverlapSphere(transform.position, hammerDamageRadius);
        foreach (Collider hitCollider in hammerDamage)
        {

            if (controller.weapons[2].activeSelf && Input.GetMouseButtonDown(0) && hitCollider.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(AttackDelay(hitCollider));
            }
        }
        Debug.Log(isHit);
    }
    //Hammer
    IEnumerator AttackDelay(Collider hitCollider)
    {
        yield return new WaitForSecondsRealtime(delayTime);
        float randomKnockBack = Random.Range(5,9);
        hitCollider.GetComponent<Rigidbody>().velocity += Camera.main.transform.forward * randomKnockBack;
        hitCollider.GetComponent<Animator>().SetBool("Hit", true);
        yield return new WaitForSecondsRealtime(.1f);
        hitCollider.GetComponent<Animator>().SetBool("Hit", false);
    }
    //Axe and Sword
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetMouseButton(0))
        {
            isHit = false;
            
        }

        if (other.gameObject.CompareTag("Enemy") && !isHit && (controller.weapons[0].activeSelf || controller.weapons[1].activeSelf))
        {
            isHit = true;
            other.GetComponent<Rigidbody>().velocity += Camera.main.transform.forward * 5;
            other.GetComponent<Animator>().SetBool("Hit", true);

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && (controller.weapons[0].activeSelf || controller.weapons[1].activeSelf))
        {
            other.GetComponent<Animator>().SetBool("Hit", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hammerDamageRadius);

    }
}
