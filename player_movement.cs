using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_movement : MonoBehaviour
{
    private Rigidbody rb;


    private float CurrentSpeed = 0;
    public float MaxSpeed;
    public float boostSpeed;
    private float RealSpeed;

    //[header("Tires")]
    //1
    //2
    //3
    //4

    //drift and steering
    private float steerDirection;
    private float driftTime;

    bool driftLeft = false;
    bool driftRight = false;
    float outwardsDriftForce = 5000;

    public bool issliding = false;

    private bool touchingGround;


    //[Header("Particles Drift sparks")]
    //1
    //2
    //3
    //4
    //5

    //[HideInInspector]
    public float BoostTime = 0;

    public Transform boostFire;
    public Transform boostExplosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
        //tireSteer
        steer();
        //groundNormalRotation
        //drift
        //Boost
    }

    private void move()
    {
        RealSpeed = transform.InverseTransformDirection(rb.velocity).z;

        if (Input.GetKey(KeyCode.W))
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, MaxSpeed, Time.deltaTime * 0.5f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, -MaxSpeed / 1.75f, Time.deltaTime);
        }
        else
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, Time.deltaTime * 1.5f);
        }

        Vector3 vel = transform.forward * CurrentSpeed;
        vel.y = rb.velocity.y;
        rb.velocity = vel;
    }
    private void steer()
    {
        steerDirection = Input.GetAxisRaw("Horizontal");
        Vector3 steerDirvect;

        float steerAmount;

        if (driftLeft && driftRight)
        {
            steerDirection = Input.GetAxis("Horizontal") < 0 ? -1.5f : -0.5f;
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, -20f, 0), 8f * Time.deltaTime);

            if (issliding && touchingGround)
                rb.AddForce(transform.right * outwardsDriftForce * Time.deltaTime, ForceMode.Acceleration);
        }
        else if (driftRight && driftLeft)
        {
            steerDirection = Input.GetAxis("Horizontal") < 0 ? 1.5f : 0.5f;
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, 20f, 0), 8f * Time.deltaTime);

            if (issliding && touchingGround)
                rb.AddForce(transform.right * -outwardsDriftForce * Time.deltaTime, ForceMode.Acceleration);
        }
        else
        {
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, 0f, 0), 8f * Time.deltaTime);
        }

        steerAmount = RealSpeed > 30 ? RealSpeed / 4 * steerDirection : steerAmount = RealSpeed / 1.5f * steerDirection;

        steerDirvect = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);

        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirvect, 3 * Time.deltaTime);
    }
    private void groundNormalRotation()
    {
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 6.0f))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up * 2, hit.normal) * transform.rotation, 7.5f * Time.deltaTime);
                touchingGround = true;
            }
            else
            {
                touchingGround = false;
            }
        }
    }
}
