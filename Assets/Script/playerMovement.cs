using System;
using UnityEngine;
public class playerController : MonoBehaviour
{
    [SerializeField] private inputReader inputReader;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private playerAnimationController animController;
    [SerializeField] private cutsceneManager cutsceneManager;
    [SerializeField] private UI ui;

    [SerializeField] public bool canJump; // = true;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float maxJumpTime;
    private float timeToApex;
    private float initialJumpVelocity;

    [SerializeField] public bool canDash = true;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashCooldown;
    private float currentDashCooldown;
    [SerializeField] private float dashStrength;

    [SerializeField] private float speedMultiplyer; // = 6f;
    [SerializeField] private float rotationFactorPerFrame; // = 0.5f;

    [SerializeField] private float groundedGravityConstant; // = -0.05f;
    [SerializeField] private float gravityConstant; // = -2 * maxJumpHeight / Mathf.Pow(timeToApex, 2);
    [SerializeField] private float yVector;

    [SerializeField] private Checkpoint[] checkpointScriptsArr;

    private Vector3 playerInitPos;
    public Vector3 spawnPoint;

    void Start()
    {
        timeToApex = maxJumpTime / 2f;
        initialJumpVelocity = 2 * maxJumpHeight / timeToApex;
        currentDashCooldown = dashCooldown;
        playerInitPos = transform.position;
        spawnPoint = transform.position;
        checkpointScriptsArr = FindObjectsByType<Checkpoint>();
    }

    void Update()
    {
        startTimerWhenStartMoving();
        updatePlayerStates();
        move();
        turn();
        handleGravity();
        jump();
        dash();
        updateAnimation();
        updateDashCooldown();
    }

    private void startTimerWhenStartMoving()
    {
        if(inputReader.moveVector != Vector2.zero)
        {   
            ui.startTimer();
        }
    }

    private void move()
    {
        Vector2 vec = inputReader.moveVector;
        Vector3 directionToMove = new Vector3(vec.x, yVector, vec.y);

        float yaw = cameraTransform.eulerAngles.y;
        Vector3 directionToMoveRelativeToCamera = Quaternion.Euler(0f, yaw, 0f) * directionToMove;  //directionnya di rotate sebanyak yaw di y axis

        characterController.Move(directionToMoveRelativeToCamera * speedMultiplyer * Time.deltaTime);
    }

    private void turn()
    {
        //turn kalo lagi gerak
        if(inputReader.moveVector != Vector2.zero)
        {
            Vector2 vec = inputReader.moveVector;
            Vector3 directionToLook = new Vector3(vec.x, 0, vec.y);

            float yaw = cameraTransform.eulerAngles.y;
            Vector3 directionToLookRelativeToCamera = Quaternion.Euler(0f, yaw, 0f) * directionToLook;  //directionnya di rotate sebanyak yaw di y axis

            Quaternion targetRotation = Quaternion.LookRotation(directionToLookRelativeToCamera);
            Quaternion currentRotation = transform.rotation; 

            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame);
        }
    }

    private void jump()
    {
        if (canJump && inputReader.isJumpKeyPressed)
        {
            animController.setJump(true);
            canJump = false;
            yVector = initialJumpVelocity;
        }
    }

    private void dash()
    {
        if ((canDash || currentDashCooldown <= 0) && inputReader.isDashKeyPressed)
        {
            //reset dash timer
            currentDashCooldown = dashCooldown;
            canDash = false;

            //handle dash logic
            animController.setDash(true);
            
            Vector3 directionToMove = Vector3.forward * dashStrength;   //forward == [0,0,1]

            float yaw = transform.eulerAngles.y;
            Vector3 directionToMoveRelativeToPlayer = Quaternion.Euler(0f, yaw, 0f) * directionToMove;  //directionnya di rotate sebanyak yaw di y axis

            characterController.Move(directionToMoveRelativeToPlayer);
        }
    }

    private void updateAnimation()
    {
        //update speed untuk blend tree
        if(inputReader.moveVector == Vector2.zero)
        {
            animController.setSpeed(0);
        }
        else
        {
            animController.setSpeed(1);
        }

        //update jump true udh di jump()
        if (characterController.isGrounded)
        {
            animController.setJump(false);
        }

    }

    private void handleGravity()
    {
        //notice bedanya = sama +=
        if (characterController.isGrounded)
        {
            animController.setJump(false);
            yVector = groundedGravityConstant;
        }
        else if(inputReader.isJumpKeyPressed)
        {
            yVector += gravityConstant;
        }
        else
        {
            yVector += gravityConstant * 3f;
        }

        yVector = Mathf.Max(yVector, -10);
    }

    private void updatePlayerStates()
    {
        if (characterController.isGrounded)
        {
            canJump = true;
        }
    }

    private void updateDashCooldown()
    {
        //decrement timer
        currentDashCooldown -= Time.deltaTime;
        currentDashCooldown = Math.Max(currentDashCooldown, 0);

        if (currentDashCooldown <= 0)
        {
            canDash = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        //kena collider buat trigger ending cutscene
        if(other.tag == "endingCollider")
        {
            ui.setBestTime();
            ui.resetTimer();
            cutsceneManager.triggerEndingCutscene();
            spawnPoint = playerInitPos;
            resetAllCheckpoints();
            teleportTo(playerInitPos);
        }
    }

    private void teleportTo(Vector3 pos)
    {
        characterController.enabled = false;
        transform.position = pos;
        characterController.enabled = true;
    }

    public void respawn()
    {
        teleportTo(spawnPoint);
    }

    private void resetAllCheckpoints()
    {
        for(int i = 0; i < checkpointScriptsArr.Length; i++)
        {
            checkpointScriptsArr[i].renableParticleSystem();
        }
    }
}
