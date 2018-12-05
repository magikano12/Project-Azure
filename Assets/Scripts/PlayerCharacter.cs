using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private string name = "Mario";

    [SerializeField]
    private float jumpHeight = 5;
    
    [SerializeField]
    private float accelerationForce = 5;

    [SerializeField]
    private float maxSpeed = 5;

    [SerializeField]
    private float jumpForce = 10;

    private bool hasKey;

    private bool isOnGround;

    [SerializeField]
    private Rigidbody2D rigidbody2DInstance;

    private float horizontalInput;

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    private Collider2D[] groundHitDetectionResults = new Collider2D[16];

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private Collider2D playerGroundCollider;

    private Checkpoint currentCheckpoint;

    public Animator animator;
    private bool isFacingRight = true;
    private bool doubleJump = false;
    private bool isDead;
    public Text deathText;
    // Use this for initialization
    void Start ()
    {
        rigidbody2DInstance = GetComponent<Rigidbody2D>();
        Debug.Log("Rawr");  //How to print to the console in Unity
        string pizza = "yum";
        Debug.Log(pizza);
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	private void Update ()
    {
        //transform.Translate(0, -.01f, 0); don't use because of physics
        //GetInput();
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
        CheckForRespawn();

    }

    private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        Move();
    }

    private void UpdatePhysicsMaterial()
    {
        if(Mathf.Abs(horizontalInput)>0)
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }

        else
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
    }

    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundHitDetectionResults) > 0;
        //Debug.Log("IsOnGround?: " + isOnGround);
        animator.SetBool("Ground", isOnGround);
    }

    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleJumpInput()
    {
        animator.SetFloat("VerticalSpeed", rigidbody2DInstance.velocity.y);
        if (Input.GetButtonDown("Jump") && (isOnGround||!doubleJump))
        {
            animator.SetBool("Ground", false);
            rigidbody2DInstance.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            if(!doubleJump&&!isOnGround)
            {
                doubleJump = true;
            }

        }
    }

    private void Move()
    {

        rigidbody2DInstance.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVelocity = rigidbody2DInstance.velocity;
        clampedVelocity.x = Mathf.Clamp(rigidbody2DInstance.velocity.x, -maxSpeed, maxSpeed);
        rigidbody2DInstance.velocity = clampedVelocity;
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if(isOnGround)
        {
            doubleJump = false;
        }
        if(horizontalInput>0&&!isFacingRight)
        {
            Flip();
        }
        if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Die()
    {
        isDead = true;
        animator.SetBool("OnHazard", true);
    }
    public void Respawn()
    {
        isDead = false;
        deathText.text = null;
        rigidbody2DInstance.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.SetBool("OnHazard", false);
        if (currentCheckpoint == null)
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                rigidbody2DInstance.velocity = Vector2.zero;
                transform.position = currentCheckpoint.transform.position;
            }
    }

    private void CheckForRespawn()
    {
        if(isDead)
        {
            rigidbody2DInstance.constraints = RigidbodyConstraints2D.FreezeAll;
            deathText.text = "Press R to Respawn!";

            if (Input.GetButtonDown("Respawn"))
            {
                Respawn();
            }
        }
    }
    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if (currentCheckpoint != null)
        {
            currentCheckpoint.SetIsActivated(false);
        }
        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }
}
