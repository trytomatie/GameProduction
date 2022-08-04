using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RacerController : State
{
    [Header("Speed stats")]
    public const float walkSpeed = 2;
    public const float  runSpeed = 6;
    public float backwardsSpeed = 6;
    public float acceleration = 7;
    public float turnspeed = 15;
    public const float sneakSpeed = 2f;

    [Header("Physics")]
    public float gravity = -9.81f;
    public bool grounded = false;

    [Header("GroundCheck")]
    public float castDistance = 0.09f;
    public float castScaleFactor = 1;
    public LayerMask layerMask;


    [Header("Sneaking")]
    public bool isSneaking = false;
    public int chaseIndex=0;
    public float noise = 0;

    private bool isJumping = false;
    
    public float jumpStrength = 5;
    private Vector3 lastHitPoint;
    private Vector3 slideMovement;



    private float movementSpeed = 0;
    private float ySpeed;
    private Vector3 movement;
    private Vector3 lastMovement;

    private bool isTransitioning = false;

    private CharacterController cc;
    private Animator anim;
    private Camera mainCamera;
    private Volume chaseVolume;





    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        chaseVolume = GameObject.Find("ChaseVolume").GetComponent<Volume>();
    }

    // Update is called once per frame
    public override void UpdateState(GameObject source)
    {
        if(isTransitioning)
        {
            isTransitioning = false;
            return;
        }
        HandleJump();
        CalculateGravity();
        HandleSneaking();
        Movement();
        Rotation();
        Animations();

    }


    private void LateUpdate()
    {
        if(!(ySpeed - cc.velocity.y > -10))
        {
            slideMovement = (transform.position - lastHitPoint).normalized * 0.1f;
        }
        else
        {
            slideMovement = Vector3.zero;
        }

        if (chaseIndex > 0)
        {
            chaseVolume.weight = 1;
            
        }
        else
        {
            chaseVolume.weight = 0;
        }

        HandleNoise();
    }

    private void Animations()
    {
        anim.SetFloat("speed", movementSpeed);
        anim.SetFloat("ySpeed", ySpeed / 12);
        anim.SetBool("isSneaking", isSneaking);
    }

    private void HandleNoise()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            switch (movementSpeed)
            {
                case runSpeed:

                    noise = 3;

                break;
                case walkSpeed:

                    noise = 1;

                break;
                case 0:

                    noise = 0;

                break;
            }
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sneaking"))
        {
            noise = 0;
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Sneaking") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            noise = 0;
        }
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
        if(isSneaking)
        {
            targetSpeed = sneakSpeed;
        }

        // If character is landing, he cant move
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jumping"))
        {
            movement = new Vector3(horizontalInput * 0.1f, 0, verticalInput*0.1f).normalized;
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

    private void HandleSneaking()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            isSneaking = !isSneaking;
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping && grounded 
            && anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            print("test");
            isJumping = true;
            ySpeed += jumpStrength;
            anim.SetTrigger("jump");
        }
        if (grounded && isJumping && ySpeed <= 0)
        {
            isJumping = false;
            anim.SetTrigger("land");
            movementSpeed = 0;
        }
    }

    private void CalculateGravity()
    {
        
        if (!Helper.CheckBeneath(transform.position, cc, layerMask, castDistance, castScaleFactor))
        {
            ySpeed += gravity * Time.deltaTime;
            grounded = false;
        }
        else
        {
            grounded = true;
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


    public override void EnterState(GameObject source)
    {

    }

    public override StateName Transition(GameObject source)
    {
        if(Input.GetKeyDown(KeyCode.E) && gameObject.GetComponent<InteractionHandler>().canInteract)
        {
            isSneaking = false;
            movementSpeed = 0;
            Animations();
            isTransitioning = true;
            return StateName.Interacting;
        }
        if(Input.GetKeyDown(KeyCode.R) && GetComponent<Inventory>().cheese > 0)
        {
            movementSpeed = 0;
            Animations();
            isTransitioning = true;
            return StateName.Throwing;
        }
        return stateName;
    }

    public override void ExitState(GameObject source)
    {

    }


    void OnDrawGizmosSelected()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        for(int i = 0; i < 10;i++)
        {
            Gizmos.DrawSphere(transform.position + new Vector3(0, castDistance / i,0),(characterController.radius + characterController.skinWidth) * castScaleFactor);
        }

    }

}
