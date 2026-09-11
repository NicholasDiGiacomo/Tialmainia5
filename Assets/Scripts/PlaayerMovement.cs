using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlaayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    [SerializeField] float baseGravity = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
     [SerializeField] Vector2 deathKick = new Vector2(10f, 20f);
    Vector2 moveInput;
    Rigidbody2D myRidgidbody;
    Animator myAnimator;
    BoxCollider2D myFeetColider;

    CapsuleCollider2D myBodyColider;
    bool isAlive = true;


    void Start()
    {
        myRidgidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyColider = GetComponent<CapsuleCollider2D>();
        myFeetColider = GetComponent<BoxCollider2D>();

        baseGravity = myRidgidbody.gravityScale;
        
    }

        void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        
    }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
        
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        if (!myFeetColider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
       
            if(value.isPressed)
        {
            myRidgidbody.linearVelocity += new Vector2 (0f, jumpSpeed);
        }
        
    }

    void ClimbLadder()
    {
         if (!myFeetColider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRidgidbody.gravityScale = baseGravity;
            return;
            myAnimator.SetBool("isClimbing", false);
        }
       Vector2 climbVelocity = new Vector2 ( myRidgidbody.linearVelocity.x, moveInput.y * climbSpeed);
        myRidgidbody.linearVelocity = climbVelocity;
        myRidgidbody.gravityScale = 0f;

        bool hasVertacleSpeed = Mathf.Abs(myRidgidbody.linearVelocity.y) > Mathf.Epsilon;

       
            myAnimator.SetBool("isClimbing", hasVertacleSpeed);
    }

    void OnAttack(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        Instantiate(bullet, gun.position, transform.rotation);
    }
    

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * moveSpeed , myRidgidbody.linearVelocity.y);
        myRidgidbody.linearVelocity = playerVelocity;

bool hasHorozontalSpeed = Mathf.Abs(myRidgidbody.linearVelocity.x) > Mathf.Epsilon;

       
            myAnimator.SetBool("isRunning", hasHorozontalSpeed);
        
        
    }

    void FlipSprite()
    {
        bool hasHorozontalSpeed = Mathf.Abs(myRidgidbody.linearVelocity.x) > Mathf.Epsilon;

        if(hasHorozontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRidgidbody.linearVelocity.x), 1f);
        }
        
    }

    void Die()
    {
        if(myBodyColider.IsTouchingLayers(LayerMask.GetMask("enimy", "hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRidgidbody.linearVelocity = deathKick;
            FindAnyObjectByType<GameSession>().ProcessPlayerDeath(); 
           
        }
    }
}
