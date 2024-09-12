using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    NavMeshAgent navMesh;
    public Transform direction;
    Animator animator;
    [SerializeField] float stoppingDistance = 1;
    [SerializeField] float suspicionRadius;
    bool isHit;
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {

        navMesh.stoppingDistance = stoppingDistance;
        if (Vector3.Distance(transform.position,direction.position)<=suspicionRadius)
        {
            Collider[] suspicionArea = Physics.OverlapSphere(transform.position, suspicionRadius);
            foreach (Collider suspicion in suspicionArea)
            {
                if (suspicion.gameObject.CompareTag("MainCha") && !isHit)
                {
                    navMesh.destination = direction.position;
                    navMesh.isStopped = false;
                    animator.SetBool("Walk", true);
                }

            }
        }
        else
        {
            
            animator.SetBool("Walk", false);
            navMesh.isStopped = true;
        }
       

        if (Vector3.Distance(transform.position, direction.position) <= stoppingDistance)
        {
            animator.SetBool("Attack", true);
            animator.SetBool("Walk", false);

        }
        else
        {
            animator.SetBool("Attack", false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon") && Combat.attack)
        {
            isHit = true;
            navMesh.isStopped = true;
        }
    }

    public void StoppedDelayFNC()
    {
        isHit = false;
        navMesh.isStopped = false;
        animator.SetBool("Stunned", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, suspicionRadius);

    }
}
