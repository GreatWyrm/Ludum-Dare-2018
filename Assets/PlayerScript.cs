using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private float horizontalSpeed = 0.12f;


    private bool isGravityInverted;

    private bool isFacingRight;
    private bool isDashing;
    private bool canDash;
    private bool canDashCooldown;
    private bool hasDashAbility;
    private bool hasJumpAbility;
    private bool isLanded;
    private int jumpsLeft;
    private float dashIntervalLength = 0.8f;
    private const int numDashIntervals = 15;
    private int numIntervalsDashed;
    private KeyCode jumpKeyCode = KeyCode.Space;
    private KeyCode altJump = KeyCode.W;
    private KeyCode moveRightKeyCode = KeyCode.D;
    private KeyCode moveLeftKeyCode = KeyCode.A;
    private KeyCode fallDownKeyCode = KeyCode.S;
    private KeyCode dashKeyCode = KeyCode.LeftShift;
    private float playerMoveX = 0;
    private float timeNoCollideWithGlass;


    void Awake() {
        isDashing = false;
        jumpsLeft = 2;
        isGravityInverted = false;
        canDash = true;
        canDashCooldown = true;
        hasDashAbility = true;
        hasJumpAbility = true;
        isFacingRight = true;
    }

    
    private void playerDash()
    {
        if(canDash && canDashCooldown && hasDashAbility)
        {
            if(isFacingRight)
            {
                dashIntervalLength = 0.8f;
            } else
            {
                dashIntervalLength = -0.8f;
            }
            GetComponent<Rigidbody2D>().gravityScale = 0;
            numIntervalsDashed = 0;
            isDashing = true;
            canDash = false;
            canDashCooldown = false;
            StartCoroutine("dashCooldown");
        }
    }
    private void playerJump()
    {
        if (jumpsLeft > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 13f), ForceMode2D.Impulse);
            jumpsLeft--;
        }
    }
    private void invertGravity()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y * -1);
        if (isGravityInverted)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        } else
        {
            GetComponent<Rigidbody2D>().gravityScale = -1.5f;
        }
        isGravityInverted = !isGravityInverted;
    }
    private void FixedUpdate()
    {
        if(!isDashing)
        {
            Vector3 moveVector = new Vector3(playerMoveX, 0, 0);
            transform.Translate(moveVector);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // We will have to do an if/else for gravity
        Vector2 bottomOfPlayer = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().bounds.extents.y);
        RaycastHit2D detectGround = Physics2D.Raycast(bottomOfPlayer, Vector2.down, 0.4f);
        if(detectGround.collider != null)
        {
            jumpsLeft = 2;
            canDash = true;
        }

    }
    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(jumpKeyCode) || Input.GetKeyDown(altJump))
        {
            if(hasJumpAbility)
            {
                Vector2 bottomOfPlayer = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().bounds.extents.y);
                RaycastHit2D detectGround = Physics2D.Raycast(bottomOfPlayer, Vector2.down, 0.4f);
                if (detectGround.collider != null)
                {
                    jumpsLeft = 2;
                }
                playerJump();
            } else
            {
                // GRAVITY INVERT
            }
        }
        if (Input.GetKey(moveRightKeyCode))
        {
            playerMoveX = horizontalSpeed;
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            isFacingRight = true;
        }
        if (Input.GetKey(moveLeftKeyCode))
        {
            playerMoveX = -horizontalSpeed;
            GetComponentInChildren<SpriteRenderer>().flipX = false;
            isFacingRight = false;
        }
        if (Input.GetKey(moveLeftKeyCode) && Input.GetKey(moveRightKeyCode))
        {
            playerMoveX = 0;
        }
        if (Input.GetKey(fallDownKeyCode))
        {
            Vector2 bottomOfPlayer = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().bounds.extents.y);
            RaycastHit2D detectGlassPlatform = Physics2D.Raycast(bottomOfPlayer, Vector2.down, 0.4f, LayerMask.GetMask("Glass Platform"));
            if(detectGlassPlatform.collider != null)
            {
                Physics2D.IgnoreLayerCollision(2, 8, true);
                timeNoCollideWithGlass = 30;
            }
        }
        if (Input.GetKeyDown(dashKeyCode))
        {
            if(hasDashAbility)
            {
                playerDash();
            } else
            {
                // TELEPORT
            }

        }
        if(Input.GetKeyUp(moveRightKeyCode) && playerMoveX == horizontalSpeed)
        {
            playerMoveX = 0;
        }
        if (Input.GetKeyUp(moveLeftKeyCode) && playerMoveX == -horizontalSpeed)
        {
            playerMoveX = 0;
        }
        if (isDashing)
        {
            if (numIntervalsDashed < numDashIntervals)
            {
                Vector2 moveVector = new Vector2(transform.position.x + dashIntervalLength, transform.position.y);
                GetComponent<Rigidbody2D>().MovePosition(moveVector);
                numIntervalsDashed++;
            }
            else
            {
                GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                isDashing = false;
            }
        }
        if(timeNoCollideWithGlass > 0)
        {
            timeNoCollideWithGlass--;
        } else if(timeNoCollideWithGlass == 0)
        {
            Physics2D.IgnoreLayerCollision(2, 8, false);
        }
    }
    IEnumerator dashCooldown()
    {
        yield return new WaitForSeconds(.66f);
        canDashCooldown = true;
    }
}
