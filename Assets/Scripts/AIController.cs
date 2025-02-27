using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent navMesh;
    public GameObject direction;
    Animator animator;
    [SerializeField] float speed;
    float defaultSpeed;
    [SerializeField] float stoppingDistance = 1;
    [SerializeField] float suspicionRadius;
    bool isHit;
    AnimationController controller;
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        defaultSpeed = speed;
    }

    private void OnEnable()
    {
        direction = GameObject.FindGameObjectWithTag("MainCha");
    }
    void Update()
    {
        Debug.Log(navMesh.isStopped);
        if (GetComponent<Enemy>().health>0)
        {
            
            navMesh.stoppingDistance = stoppingDistance;
            if (Vector3.Distance(transform.position, direction.transform.position) <= suspicionRadius)
            {
                Collider[] suspicionArea = Physics.OverlapSphere(transform.position, suspicionRadius);
                foreach (Collider suspicion in suspicionArea)
                {
                    if (suspicion.gameObject.CompareTag("MainCha") && !isHit)
                    {
                        navMesh.destination = direction.transform.position;
                        navMesh.speed = defaultSpeed;
                        animator.SetBool("Walk", true);
                        animator.SetBool("Run", false);
                    }

                }
            }
            else
            {
                navMesh.destination = direction.transform.position;
                navMesh.speed = speed * 2;
                animator.SetBool("Run", true);
                animator.SetBool("Walk", false);

            }
        }
       
       

        if (Vector3.Distance(transform.position, direction.transform.position) <= stoppingDistance)
        {
            switch (GetComponent<Enemy>().enemyInfo.characterType)
            {
                case ChaType.Classic:
                    animator.SetBool("Attack", true);
                    break;
                case ChaType.Heavy:
                    animator.SetBool("Attack2", true);
                    break;
                case ChaType.Fast:
                    animator.SetBool("Attack3", true);
                    break;
            }
            animator.SetBool("Walk", false);
            
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                transform.LookAt(direction.transform.position);
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
            switch (GetComponent<Enemy>().enemyInfo.characterType)
            {
                case ChaType.Classic:
                    animator.SetBool("Attack", false);
                    break;
                case ChaType.Heavy:
                    animator.SetBool("Attack2", false);
                    break;
                case ChaType.Fast:
                    animator.SetBool("Attack3", false);
                    break;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon") && Combat.attack)
        {
            controller = other.GetComponentInParent<AnimationController>();
            isHit = true;
            navMesh.isStopped = true;
        }
    }

    public void StoppedDelayFNC()
    {
        isHit = false;
        navMesh.isStopped = false;
        animator.SetBool("Stunned", false);
        animator.SetBool("Hit", false);
    }

    public void isStoppedFalseFNC()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        if (!controller.weapons[0].activeSelf)
        {
            isHit = false;
            navMesh.isStopped = false;
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, suspicionRadius);

    }
}
