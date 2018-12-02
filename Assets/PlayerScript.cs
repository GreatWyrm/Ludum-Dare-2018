using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private int xPos;
    private int yPos;


    // Player Phyisics Variables
    public float gravity = 0.01f;
    public float terminalVelocity = 1.0f;
    public float horizontalSpeed = 0.2f;


    private float playerFallingSpeed;
    private bool gravityPaused;
    private bool isGravityInverted;

    public bool isDashing;
    private int dashIntervalLength = 2;
    private const int numDashIntervals = 50;
    private int numIntervalsDashed;
    private KeyCode jumpKeyCode = KeyCode.Space;
    private KeyCode altJump = KeyCode.W;
    private KeyCode moveRightKeyCode = KeyCode.D;
    private KeyCode moveLeftKeyCode = KeyCode.A;
    private KeyCode fallDownKeyCode = KeyCode.S;
    private KeyCode dashKeyCode = KeyCode.LeftShift;
    private float playerMoveX = 0;

    void Awake() {
        xPos = 0;
        yPos = 0;
        isDashing = false;
        playerFallingSpeed = 0f;
        gravityPaused = false;
    }

    
    private void playerDash()
    {
        gravityPaused = true;
        numIntervalsDashed = 0;
        isDashing = true;
    }
    private void playerJump()
    {
        float playerJumpStrength = 0.4f;
        gravityPaused = false;
        if(isGravityInverted)
        {
            playerFallingSpeed = -playerJumpStrength;
        } else
        {
            playerFallingSpeed = playerJumpStrength;
        }
    }
    private void FixedUpdate()
    {
        if(isDashing)
        {
            if(numIntervalsDashed < numDashIntervals)
            {
                xPos += dashIntervalLength;
                numIntervalsDashed++;
            } else
            {
                isDashing = false;
                gravityPaused = false;
            }
        }
        if (!gravityPaused)
        {
                if (Mathf.Abs(playerFallingSpeed) < terminalVelocity)
                {
                    playerFallingSpeed += -gravity;
                }
        }

        Vector2 moveVector = new Vector2(playerMoveX, playerFallingSpeed);
        transform.Translate(moveVector);
        Vector2 start = new Vector2(transform.position.x, transform.position.y - GetComponent<Collider2D>().bounds.extents.y);
        RaycastHit2D hit = Physics2D.Raycast(start, Vector2.down, .05f);
        if (hit.collider != null && playerFallingSpeed != 0)
        {
            gravityPaused = true;
            playerFallingSpeed = 0;
        }
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
    }
}
