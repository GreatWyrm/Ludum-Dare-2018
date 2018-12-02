using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private float horizontalSpeed = 0.12f;


    private float playerFallingSpeed;
    private bool gravityPaused;
    private bool isGravityInverted;

    private bool isDashing;
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
    private KeyCode dashKeyCode = KeyCode.RightShift;
    private float playerMoveX = 0;

    void Awake() {
        isDashing = false;
        jumpsLeft = 2;
        playerFallingSpeed = 0f;
        gravityPaused = false;
        isGravityInverted = false;
    }

    
    private void playerDash()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
        numIntervalsDashed = 0;
        isDashing = true;
    }
    private void playerJump()
    {
        if (jumpsLeft > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
            jumpsLeft--;
        }
    }
    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(playerMoveX, playerFallingSpeed, 0.0f);
        transform.Translate(moveVector);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(jumpKeyCode) || Input.GetKeyDown(altJump))
        {
            playerJump();
        }
        if (Input.GetKeyDown(moveRightKeyCode))
        {
            playerMoveX = horizontalSpeed;
        }
        if (Input.GetKeyDown(moveLeftKeyCode))
        {
            playerMoveX = -horizontalSpeed;
        }
        if (Input.GetKeyDown(moveLeftKeyCode) && Input.GetKeyDown(moveRightKeyCode))
        {
            playerMoveX = 0;
        }
        if (Input.GetKeyDown(fallDownKeyCode))
        {

        }
        if (Input.GetKeyDown(dashKeyCode))
        {
            playerDash();
        }
        if(Input.GetKeyUp(moveRightKeyCode) && playerMoveX == horizontalSpeed)
        {
            playerMoveX = 0;
        }
        if (Input.GetKeyUp(moveLeftKeyCode) && playerMoveX == -horizontalSpeed)
        {
            playerMoveX = 0;
        }
        if(isDashing)
        {
            if(numIntervalsDashed < numDashIntervals)
            {
                Vector2 moveVector = new Vector2(transform.position.x + dashIntervalLength, transform.position.y);
                GetComponent<Rigidbody2D>().MovePosition(moveVector);
                numIntervalsDashed++;
            } else
            {
                GetComponent<Rigidbody2D>().gravityScale = 1;
                isDashing = false;
            }

        }
    }
}
