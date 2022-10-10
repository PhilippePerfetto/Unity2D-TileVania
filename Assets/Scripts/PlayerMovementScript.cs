using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float ladderSpeed = 10f;
    [SerializeField] float jumpSpeed = 50f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    
    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    BoxCollider2D myBoxCollider2D;
    bool playerHasHorizontalSpeed, playerHasVerticalSpeed;
    bool isAlive = true;
    // float myEpsilon = 0.0001f; //Mathf.Epsilon;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > myEpsilon;
        // playerHasVerticalSpeed = Mathf.Abs(myRigidbody2D.velocity.y) > myEpsilon;

        if (isAlive)
        {
            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }
    }

    private void Die()
    {
            var layerMask = LayerMask.GetMask("Enemies", "Hazard");
            var isTouching = myCapsuleCollider2D.IsTouchingLayers(layerMask);

            if (isTouching)
            {
                isAlive = false;
                myAnimator.SetTrigger("isDying");
                myRigidbody2D.velocity = deathKick;
                FindObjectOfType<GameSession>().ProcessPlayerDeath();
            }
    }

    void OnMove(InputValue inputValue)
    {
        if (isAlive)
        {
            moveInput = inputValue.Get<Vector2>();
            //Debug.Log("move " + moveInput + " - " + Mathf.Epsilon);

            playerHasHorizontalSpeed = Mathf.Abs(moveInput.x) == 1f;
            playerHasVerticalSpeed = Mathf.Abs(moveInput.y) == 1f;

            //Debug.Log("Horizontal movement : " + playerHasHorizontalSpeed);
            //Debug.Log("Vertical movement :" + playerHasVerticalSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (value.isPressed && isAlive && gameObject?.activeSelf == true && gun != null && transform != null)
        {
            var gPos = gun.position;
            var tR = transform.rotation;
            var b = bullet;
            var go = GameObject.Instantiate(bullet, gun.position, transform.rotation);
            /*var layerMask = LayerMask.GetMask("Player");
            var isTouching = myBoxCollider2D.IsTouchingLayers(layerMask);

            if (isTouching)
                myRigidbody2D.velocity += new Vector2(0f, jumpSpeed);*/
        }
    }
   
    void OnJump(InputValue value)
    {
        if (value.isPressed && isAlive)
        {
            var layerMask = LayerMask.GetMask("Ground");
            var isTouching = myBoxCollider2D.IsTouchingLayers(layerMask);

            if (isTouching)
                myRigidbody2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void ClimbLadder()
    {
        var layerMask = LayerMask.GetMask("Ladder");
        var isTouching = myBoxCollider2D.IsTouchingLayers(layerMask);

        if (isTouching)
        {
            // move or stop on the ladder
            if (playerHasVerticalSpeed)
                    myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, moveInput.y * ladderSpeed);
                else
                    myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, 0f);
        }

        myAnimator.SetBool("isClimbing", isTouching && playerHasVerticalSpeed);
        myRigidbody2D.gravityScale = isTouching ? 0 : 1;
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody2D.velocity.y); // / Time.deltaTime

        myRigidbody2D.velocity = playerVelocity;

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed); 
    }

    void FlipSprite()
    {
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);

            myAnimator.SetBool("isRunning", true); 
        }
        else
        {
            // No move, keep last direction
        }        
    }
}
