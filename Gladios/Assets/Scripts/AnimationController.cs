using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    public float acceleration = .2f;
    public float deccalariton = .5f;
    public GameObject[] weapons;
    float moveY;
    float moveX;
    [SerializeField] float maxMoveValue = 1.2f;
    float defaultMaxMoveValue = 1.2f;
    [SerializeField] float maxMoveValueRun = 2.4f;
    bool isAttack;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        defaultMaxMoveValue = maxMoveValue;
        rb = GetComponent<Rigidbody>();

    }


    void Update()
    {
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool attack = Input.GetMouseButton(0);
        bool block = Input.GetMouseButton(1);


        if (forwardPressed && moveY < maxMoveValue)
        {
            moveY += Time.deltaTime * acceleration;
            if (runPressed)
            {
                moveY += Time.deltaTime * acceleration;
            }
        }
        else if ((!forwardPressed || !runPressed) && moveY > 0)
        {
            moveY -= Time.deltaTime * deccalariton;
        }

        if (backwardPressed && -moveY < maxMoveValue)
        {
            moveY += Time.deltaTime * -acceleration;
            if (runPressed)
            {
                moveY += Time.deltaTime * -acceleration;
            }
        }
        else if ((!backwardPressed || !runPressed) && -moveY > 0)
        {
            moveY -= Time.deltaTime * -deccalariton;
        }

        if (rightPressed && moveX < maxMoveValue)
        {
            moveX += Time.deltaTime * acceleration;
            if (runPressed)
            {
                moveX += Time.deltaTime * acceleration;
            }
        }
        else if ((!rightPressed || !runPressed) && moveX > 0)
        {
            moveX -= Time.deltaTime * deccalariton;
        }

        if (leftPressed && -moveX < maxMoveValue)
        {
            moveX += Time.deltaTime * -acceleration;
            if (runPressed)
            {
                moveX += Time.deltaTime * -acceleration;
            }
        }
        else if ((!leftPressed || !runPressed) && -moveX > 0)
        {
            moveX -= Time.deltaTime * -deccalariton;
        }

        if (runPressed)
        {
            maxMoveValue = maxMoveValueRun;
        }
        else if (!runPressed)
        {
            maxMoveValue = defaultMaxMoveValue;
        }
        animator.SetFloat("MoveY", moveY);
        animator.SetFloat("MoveX", moveX);


        //Sword
        if (weapons[0].activeSelf)
        {
            if (attack && !block)
            {
                animator.SetBool("Attack1", true);
                rb.isKinematic = true;
                isAttack = true;
            }
        }
       
        //Axe
        if (weapons[1].activeSelf)
        {
            if (attack && !block)
            {
                animator.SetBool("Attack3", true);
                rb.isKinematic = true;
                isAttack = true;
            }
        }
        
        //Hammer
        if (weapons[2].activeSelf)
        {
            if (attack && !block)
            {
                animator.SetBool("Attack2", true);
                rb.isKinematic = true;
                isAttack = true;
            }
        }

        if (block)
        {
            animator.SetBool("Block", true);
          
        }
        else if (!block && !isAttack)
        {
            animator.SetBool("Block", false);
            
        }
      
        
    }

    public void AttackControlFNC()
    {
        //Sword
        if (weapons[0].activeSelf)
        {

            animator.SetBool("Attack1", false);
            rb.isKinematic = false;
            isAttack = false;
        }
        
        //Axe
        if (weapons[1].activeSelf)
        {
            
            animator.SetBool("Attack3", false);
            rb.isKinematic = false;
            isAttack = false;
        }

        //Hammer
        if (weapons[2].activeSelf)
        {

            animator.SetBool("Attack2", false);
            rb.isKinematic = false;
            isAttack = false;
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Weapon"))
    //    {
    //        animator.SetBool("BlockHit",true);
    //    }

    //}



}
