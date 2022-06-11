using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerComponent
{
    [Header("Speed stats")]
    public float walkSpeed = 6;
    public float runSpeed = 12;
    public float backwardsSpeed = 6;
    public float acceleration = 7;
    public float turnspeed = 15;

    [Header("Physics")]
    public float gravity = -9.81f;
    public LayerMask layerMask;




    private bool isJumping = false;
    private Vector3 lastHitPoint;
    private Vector3 slideMovement;



    private float movementSpeed = 0;
    private float ySpeed;
    private Vector3 movement;
    private Vector3 lastMovement;

    private CharacterController cc;
    private Animator anim;
    private Camera mainCamera;





    // Start is called before the first frame update
    void Start()
    {
        locked = false;
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (locked)
            return;
        //HandleJump();
        CalculateGravity();
        Movement();
        Rotation();
        Animations();
    }

    private void LateUpdate()
    {
        if (locked)
            return;
        if(!(ySpeed - cc.velocity.y > -10))
        {
            slideMovement = (transform.position - lastHitPoint).normalized * 0.1f;
        }
        else
        {
            slideMovement = Vector3.zero;
        }
    }

    private void Animations()
    {
        anim.SetFloat("speed", movementSpeed);
        anim.SetFloat("ySpeed", ySpeed / 12);
    }

    private void Rotation()
    {
        if (cc.velocity.magnitude > 0)
        {
            float rotation = Mathf.Atan2(lastMovement.x, lastMovement.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotation, 0), turnspeed * Time.deltaTime);
        }
    }

    private void Movement()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float targetSpeed = runSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetSpeed = walkSpeed;
        }

        // If character is landing, he cant move
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Landing"))
        {
            movement = Vector3.zero;
        }
        else
        {
            movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        }


        Vector3 cameraDependingMovement = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * movement;
        movementSpeed = Mathf.MoveTowards(movementSpeed, movement.magnitude * targetSpeed, acceleration * Time.deltaTime);

        if (verticalInput != 0 || horizontalInput != 0)
        {
            lastMovement = cameraDependingMovement;
        }


        cc.Move(lastMovement * movementSpeed * Time.deltaTime 
            + new Vector3(0, ySpeed, 0) * Time.deltaTime
            + slideMovement);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping && Helper.CheckBeneath(transform.position, cc, layerMask, 0.1f, 1.3f) && anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            print("test");
            isJumping = true;
            ySpeed += 10;
            anim.SetTrigger("jump");
        }
        if (Helper.CheckBeneath(transform.position, cc, layerMask, 1f, 0.5f) && isJumping && ySpeed <= 0)
        {
            isJumping = false;
            anim.SetTrigger("land");
            movementSpeed = 0;
        }
    }

    private void CalculateGravity()
    {
        
        if (!Helper.CheckBeneath(transform.position, cc, layerMask, 0.1f, 1.8f))
        {
            ySpeed += gravity * Time.deltaTime;
        }
        else
        {
            if (ySpeed < 0)
            {
                ySpeed = 0;
            }
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        lastHitPoint = hit.point;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position + new Vector3(0, (cc.radius + cc.skinWidth) * 1 + 0.05f, 0), (cc.radius + cc.skinWidth) * 1);
    }

}
