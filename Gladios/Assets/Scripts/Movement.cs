using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Values")]
    /*[HideInInspector]*/ public float speed = 5f;
    public float inAttackSpeed;
    [SerializeField] float forceSpeedValue = 5f;
    [SerializeField] float jumpValue = 10f;
    [SerializeField] float mouseSensivity = 5f;
    [SerializeField] float speedAccelaretion;
    [SerializeField] float speedDeccalariton;
    [SerializeField] float minClamp;
    [SerializeField] float gravityMultiplier;
    float currentRotationX = 0f;
    [Header("Objects")]
    Rigidbody rb;
    public Camera mainCamera;
    [HideInInspector] public float defaultSpeed;
    float defaultGravityMultiplier;
    float forceSpeed;
    [Header("Bool")]
    bool isGround;

    void Start()
    {
        defaultGravityMultiplier = gravityMultiplier;
        defaultSpeed = speed;
        forceSpeed = speed + forceSpeedValue;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        inAttackSpeed = speed / 2 + 1;
    }

    void Update()
    {
        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            if (!GetComponentInChildren<Combat>().isDead)
            {
                Direction();
                Jump();
                Force();
            }
        }
        Debug.Log(speed);
       
        speed = Mathf.Clamp(speed,1f, 5f);
    }

    void Direction()
    {

        float horizontal = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        //transform.Translate(horizontal, 0, vertical);

        if (horizontal == 0 && vertical == 0)
        {
            rb.velocity = Vector3.Slerp(rb.velocity, Vector3.zero, speedDeccalariton * Time.deltaTime);
        }



        float clampedMagnitude = Mathf.Clamp(rb.velocity.magnitude, minClamp, speed);
        rb.velocity = rb.velocity.normalized * clampedMagnitude;


        Vector3 cameraDirection = mainCamera.transform.forward;
        cameraDirection.y = 0;
        Vector3 moveDirection = (cameraDirection.normalized * vertical) + (mainCamera.transform.right * horizontal);


        rb.velocity += moveDirection * speed;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity;

        currentRotationX -= mouseY;
        currentRotationX = Mathf.Clamp(currentRotationX, -25f, 25f);

        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + mouseX, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(currentRotationX, 0, 0);
    }

    void Jump()
    {

        bool jumpInput = Input.GetKey(KeyCode.Space);
        if (isGround)
        {
            //Physics.gravity = new Vector3(0f, -9.81f, 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 jump = new Vector3(transform.position.x, transform.position.y * jumpValue * jumpValue, transform.position.z);
                rb.AddForce(jump);
            }
        }
        //if (!isGround)
        //{
        //    if (!jumpInput)
        //    {
        //        Physics.gravity = new Vector3(0f, -9.81f*gravityMultiplier, 0);
        //    }
        //}





    }

    void Force()
    {
        bool forceInput = Input.GetKey(KeyCode.LeftShift);
        speed = Mathf.Clamp(speed, defaultSpeed, forceSpeed);
        if (forceInput)
        {
            speed += Time.deltaTime * speedAccelaretion;

        }
        if (!forceInput)
        {
            speed = defaultSpeed;
        }

        if (speed > forceSpeed)
        {
            speed = forceSpeed;
        }

        else if (speed < defaultSpeed)
        {
            speed = defaultSpeed;
        }
    }



    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
            isGround = false;

    }
}