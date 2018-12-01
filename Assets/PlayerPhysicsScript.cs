using UnityEngine;

public class PlayerPhysicsScript : MonoBehaviour {

    private const float PLAYER_GRAVITY = 9.81f;
    private const int TERMINAL_VELOCITY = 56;
    private float playerFallingSpeed;
    private bool gravityPaused;
    private bool isGravityInverted;
    private PlayerScript playerReference;

    public PlayerPhysicsScript(PlayerScript player)
    {
        playerFallingSpeed = 0f;
        gravityPaused = true;
        isGravityInverted = false;
        playerReference = player;
    }

    private void FixedUpdate()
    {
        if(!gravityPaused)
        {
            if(playerFallingSpeed < TERMINAL_VELOCITY)
            {
                playerFallingSpeed += PLAYER_GRAVITY;
            }
            if(isGravityInverted)
            {
                playerReference.changePlayerY(Mathf.RoundToInt(playerFallingSpeed));
            } else
            {
                playerReference.changePlayerY(Mathf.RoundToInt(playerFallingSpeed) * -1);
            }

        }
    }
    public void resetPlayerSpeed()
    {
        playerFallingSpeed = 0;
    }
    public void pausePlayerGravity()
    {
        gravityPaused = true;
    }
    public void unpausePlayerGravity()
    {
        gravityPaused = false;
    }
}
