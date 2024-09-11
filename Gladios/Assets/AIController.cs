using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    NavMeshAgent navMesh;
    public Transform direction;
    Animator animator;
    [SerializeField] float stoppingDistance=1;
    [SerializeField] float suspicionRadius;
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
       
        navMesh.stoppingDistance = stoppingDistance;
        Collider[] suspicionArea = Physics.OverlapSphere(transform.position,suspicionRadius);
        foreach (Collider suspicion in suspicionArea)
        {
            if (suspicion.gameObject.CompareTag("MainCha"))
            {
                navMesh.destination = direction.position;
                animator.SetBool("Walk", true);
            }
            
        }

        if (Vector3.Distance(transform.position,direction.position)<=stoppingDistance)
        {
            animator.SetBool("Attack", true);
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
           navMesh.isStopped = true;
        }
    }

    public void StoppedDelayFNC()
    {
        navMesh.isStopped = false;
        animator.SetBool("Stunned", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, suspicionRadius);

    }
}
