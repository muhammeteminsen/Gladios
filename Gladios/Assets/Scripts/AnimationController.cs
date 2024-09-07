using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    public float acceleration = .2f;
    public float deccalariton = .5f;
    float isBlending;
  

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);



        if (forwardPressed && isBlending <= 1)
        {
            animator.SetBool("isWalking", true);
            if (isBlending < .5f)
            {
                isBlending += Time.deltaTime * acceleration;
            }
            if (runPressed)
            {
                isBlending += Time.deltaTime * acceleration;
            }
            else if (!runPressed && isBlending > .5f)
            {
                isBlending -= Time.deltaTime * deccalariton;
            }



        }
        else if (!forwardPressed && isBlending >= 0)
        {
            isBlending -= Time.deltaTime * deccalariton;
            animator.SetBool("isWalking", false);
        } 

        if (isBlending > 1)
        {
            isBlending = 1;
        }
        if (isBlending < 0)
        {
            isBlending = 0;
        }

        animator.SetFloat("Walking", isBlending);
        
    }


  

  
}
