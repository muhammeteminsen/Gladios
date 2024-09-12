using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    NavMeshAgent navMesh;
    public Transform direction;
    Animator animator;
    [SerializeField] float speed;
    float defaultSpeed;
    [SerializeField] float stoppingDistance = 1;
    [SerializeField] float suspicionRadius;
    bool isHit;
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        defaultSpeed = speed;
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
                    navMesh.speed = defaultSpeed;
                    navMesh.isStopped = false;
                    animator.SetBool("Walk", true);
                    animator.SetBool("Run",false);
                }

            }
        }
        else
        {
            navMesh.destination = direction.position;
            navMesh.speed = speed*2;
            animator.SetBool("Run",true);
            animator.SetBool("Walk", false);
            
        }
       

        if (Vector3.Distance(transform.position, direction.position) <= stoppingDistance)
        {
            animator.SetBool("Attack", true);
            animator.SetBool("Walk", false);
            
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                transform.LookAt(direction.position);
            }
            else
            {
                int ignoreLayer = LayerMask.NameToLayer("NavMesh");
                navMesh.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                Physics.IgnoreLayerCollision(navMesh.gameObject.layer, ignoreLayer);
            }
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
