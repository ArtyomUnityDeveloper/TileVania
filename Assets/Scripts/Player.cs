using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // config parameters
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 40f);
    [SerializeField] AudioClip deathSound;
    [SerializeField] float deathDelay = 1f;

    // state
    bool isAlive = true;

    // cached references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;


    // Message then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) {  return; }

        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
        Die();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        // print(playerVelocity);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }


    private void Jump()
    {
        if(!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground", "Hazards"))) { return; }


        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")) || myFeet.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            myAnimator.SetTrigger("Die");
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            GetComponent<Rigidbody2D>().velocity = deathKick;
            StartCoroutine(DeathLittleDelay());
        }
    }

    IEnumerator DeathLittleDelay()
    {
        yield return new WaitForSecondsRealtime(deathDelay);
        isAlive = false;
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    private void ClimbLadder()
    {
        //Debug.Log("myFeet.IsTouchingLayersLayerMask.GetMask Ladders) " + myFeet.IsTouchingLayers(LayerMask.GetMask("Ladders")));
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ladders"))) 
        {
            myRigidBody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("Climbing", false);
            return; 
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); // value is between -1 to +1
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;
        // print(playerVelocity);

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);

    }


    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        // if the player is moving horizontally
        if(playerHasHorizontalSpeed)
        {
            // reverse the current scaling of x axis
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f); 
            // в общем - если движемся, то передаём в трансформ локал скейл +1 или -1 по оси Х
            // в зависимости от того, в какую сторону движется игрок
            // передача идёт через создание нового Vector2 
        }

    }

}
