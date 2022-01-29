using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement properties")]
    [Tooltip("the max horizontal movement speed of the player")]
    public float moveSpeed = 2f;
    [Tooltip("the max horizontal air speed of the player")]
    public float maxAirSpeed = 3f;
    [Tooltip("the horizontal air acceleration of the player")]
    public float airAccel = .5f;
    [Tooltip("the velocity that the player jumps at")]
    public float jumpSpeed = 5f;
    [Tooltip("whether the player is on the ground or not")]
    public bool grounded = true;
    [Tooltip("current velocity")]
    public Vector3 currentVel;

    //The player's rigidbody
    private Rigidbody rb;

    [Tooltip("the player's height")]
    public float height;
    [Tooltip("the player's width")]
    public float width;
    [Tooltip("vertical margin of error for determining if player is grounded")]
    public float heightMargin = 5f;
    [Tooltip("horizontal margin of error for determining if player is grounded")]
    public float widthMargin = 5f;


    // Start is called before the first frame update
    void Start()
    {
        height = GetComponent<Collider>().bounds.extents.y;
        width = GetComponent<Collider>().bounds.extents.x + widthMargin;

        rb = GetComponent<Rigidbody>();

        currentVel = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = CheckGrounded();
        Vector3 vel = rb.velocity;
        vel.x = currentVel.x;
        rb.velocity = vel;
    }

    //When player movement input is detected
    void OnMove(InputValue value)
    {
        float vel = value.Get<float>();
        if (Mathf.Abs(vel) < .1)
        {
            vel = 0;
        }
 
            Vector3 velocity = rb.velocity;
            velocity.x = vel * moveSpeed;
            currentVel = velocity;
    }

    //When the player jump input is detected
    void OnJump()
    {
        Debug.Log("check for jump");
        if (grounded)
        {
            Vector3 velocity = rb.velocity;
            velocity.y = jumpSpeed;
            rb.velocity = velocity;
            Debug.Log("successfully jumped");
        }
    }

    bool CheckGrounded()
    {
        LayerMask mask = LayerMask.GetMask("Player", "Projectile");
        Vector3 center = transform.position;
        Vector3 size = new Vector3(width/2, heightMargin, width/2);
        Debug.Log(center);




        return Physics.BoxCast(center, size, -Vector3.up, Quaternion.identity, height + heightMargin);
        
        //return Physics.CheckBox(center: center, halfExtents: size, orientation: Quaternion.identity, layermask: mask, queryTriggerInteraction: QueryTriggerInteraction.UseGlobal);
    }
}
