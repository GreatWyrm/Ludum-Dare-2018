﻿using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float horizontalSpeed = 0.12f;


    private bool isGravityInverted;

    public bool isFacingRight;
    private bool isDashing;
    private bool canDash;
    private bool canDashCooldown;
    public bool hasDashAbility;
    public bool hasTeleportAbility;
    public bool hasJumpAbility;
    public bool hasGravityAbility;
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

    public AudioClip sprintSuccessSound, sprintFailureSound, teleSuccessSound, teleFailureSound;

    void Awake()
    {
        isDashing = false;
        jumpsLeft = 2;
        isGravityInverted = false;
        canDash = true;
        canDashCooldown = true;
        hasDashAbility = true;
        hasTeleportAbility = false;
        hasGravityAbility = false;
        hasJumpAbility = true;
        isFacingRight = true;
    }


    private void playerDash()
    {
        if (canDash && canDashCooldown)
        {
            if (isFacingRight)
            {
                dashIntervalLength = 0.8f;
            }
            else
            {
                dashIntervalLength = -0.8f;
            }
            if (hasDashAbility)
            {
                GetComponent<AudioSource>().PlayOneShot(sprintSuccessSound);
                GetComponent<Rigidbody2D>().gravityScale = 0;
                numIntervalsDashed = 0;
                isDashing = true;
                canDash = false;
                canDashCooldown = false;
                StartCoroutine("dashCooldown");
            }
            else if (hasTeleportAbility)
            {
                dashIntervalLength *= 7;
                Vector2 raycastOrigin = new Vector2(transform.position.x + dashIntervalLength, transform.position.y + GetComponent<BoxCollider2D>().bounds.extents.y);
                RaycastHit2D canTeleport = Physics2D.Raycast(raycastOrigin, Vector2.right, 0.8f);
                if (canTeleport.collider == null)
                {
                    GetComponent<AudioSource>().PlayOneShot(teleSuccessSound);
                    transform.Translate(new Vector3(dashIntervalLength, 0f, 0f));
                    canDash = false;
                    canDashCooldown = false;
                    StartCoroutine("dashCooldown");
                }
                else
                {
                    GetComponent<AudioSource>().PlayOneShot(teleFailureSound);
                }

            }

        }
    }

    private void playerJump()
    {
        if (hasJumpAbility) {
if (jumpsLeft > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 13f), ForceMode2D.Impulse);
            jumpsLeft--;
        }
        } 
        
        
    }
    private void invertGravity()
    {
        isGravityInverted = !isGravityInverted;
        //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y * -1);
        if (isGravityInverted)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = -1.5f;
        }
        
    }
    private void FixedUpdate()
    {
        if (!isDashing)
        {
            Vector3 moveVector = new Vector3(playerMoveX, 0, 0);

            transform.Translate(moveVector);
        }
        if(isGravityInverted)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < 0 || timeNoCollideWithGlass > 0)
            {
                Physics2D.IgnoreLayerCollision(2, 8, true);
            }
            else
            {
                Physics2D.IgnoreLayerCollision(2, 8, false);
            }
        } else
        {
            if (GetComponent<Rigidbody2D>().velocity.y > 0 || timeNoCollideWithGlass > 0)
            {
                Physics2D.IgnoreLayerCollision(2, 8, true);
            }
            else
            {
                Physics2D.IgnoreLayerCollision(2, 8, false);
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // We will have to do an if/else for gravity
        Vector2 bottomOfPlayer = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().bounds.extents.y);
        RaycastHit2D detectGround = Physics2D.Raycast(bottomOfPlayer, Vector2.down, 0.4f);
        if (detectGround.collider != null)
        {
            jumpsLeft = 2;
            canDash = true;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(jumpKeyCode) || Input.GetKeyDown(altJump))
        {
            if (hasJumpAbility)
            {
                float factor = Mathf.Sign(GetComponent<Rigidbody2D>().gravityScale);
                Vector2 bottomOfPlayer = new Vector2(transform.position.x, transform.position.y - GetComponent<BoxCollider2D>().bounds.extents.y * factor);
                RaycastHit2D detectGround = Physics2D.Raycast(bottomOfPlayer, Vector2.down, 0.4f * factor);
                if (detectGround.collider != null)
                {
                    jumpsLeft = 2;
                }
                playerJump();
            }
            else
            {
                if (hasGravityAbility) {
            Debug.Log("reverse gravity guys :/");
            invertGravity();
        }
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
            if (detectGlassPlatform.collider != null)
            {

                timeNoCollideWithGlass = 30;
            }
        }
        if (Input.GetKeyDown(dashKeyCode))
        {
            playerDash();

        }
        if (Input.GetKeyUp(moveRightKeyCode) && playerMoveX == horizontalSpeed)
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
        if (timeNoCollideWithGlass > 0)
        {
            timeNoCollideWithGlass--;
        }
    }
    IEnumerator dashCooldown()
    {
        yield return new WaitForSeconds(.66f);
        canDashCooldown = true;
    }
}