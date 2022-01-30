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


    [Header("Gun objects")]
    [Tooltip("the gun")]
    public GameObject gun;
    [Tooltip("the end of the gun")]
    public GameObject gunEnd;
    [Tooltip("the gun holder (for rotation)")]
    public GameObject gunHolder;

    [Header("Gun stats")]
    [Tooltip("projectile")]
    public GameObject proj;
    [Tooltip("the gun's current color")]
    public Colors.Color gunColor = Colors.Color.Grey;
    [Tooltip("how long it takes until the gun can fire again")]
    public float gunDelay = 1;
    [Tooltip("the gun's cooldown timer")]
    public float gunTimer;
    [Tooltip("the projectiles' speed")]
    public float projSpeed;
    //the gun's mesh renderer
    private MeshRenderer matRenderer;


    // Start is called before the first frame update
    void Start()
    {
        matRenderer = gun.GetComponent<MeshRenderer>();
        if(matRenderer == null)
        {
            Debug.LogWarning("why");
        }
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
        if(gunTimer > 0)
        {
            gunTimer -= Time.fixedDeltaTime;
        }
    }

    //When player movement input is detected
    void OnMove(InputValue value)
    {
        Debug.Log("moved: " + value);
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

    //
    void OnShoot()
    {
        if(gunTimer <= 0)
        {
            GameObject newProj = Instantiate(proj, gunEnd.transform.position, gunHolder.transform.rotation);
            newProj.GetComponent<Rigidbody>().velocity = gunHolder.transform.up.normalized * projSpeed;
            newProj.GetComponent<ColorBullet>().SetColor(gunColor);
            Debug.Log("Shot new bullet with velocity: " + newProj.GetComponent<Rigidbody>().velocity);
            Debug.Log("gundealy: " + gunDelay);
            gunTimer = gunDelay;
            Debug.Log("guntimer: " + gunTimer);
        }
    }

    void OnGrey()
    {
        gunColor = Colors.Color.Grey;
        matRenderer.material = Colors.GetColorMat(Colors.Color.Grey);
    }

    void OnRed()
    {
        gunColor = Colors.Color.Red;
        matRenderer.material = Colors.GetColorMat(Colors.Color.Red);
    }

    void OnGreen()
    {
        gunColor = Colors.Color.Green;
        matRenderer.material = Colors.GetColorMat(Colors.Color.Green);
    }

    void OnBlue()
    {
        gunColor = Colors.Color.Blue;
        matRenderer.material = Colors.GetColorMat(Colors.Color.Blue);
    }

    void OnSwapColor()
    {
        int newColor = ((int)gunColor + 1) % 4;
        gunColor = (Colors.Color)newColor;
        matRenderer.material = Colors.GetColorMat(gunColor);
    }

    bool CheckGrounded()
    {
        LayerMask mask = LayerMask.GetMask("Player", "Projectile");
        Vector3 center = transform.position;
        Vector3 size = new Vector3(width/2, heightMargin, width/2);




        return Physics.BoxCast(center, size, -Vector3.up, Quaternion.identity, height + heightMargin);
        
        //return Physics.CheckBox(center: center, halfExtents: size, orientation: Quaternion.identity, layermask: mask, queryTriggerInteraction: QueryTriggerInteraction.UseGlobal);
    }
}
