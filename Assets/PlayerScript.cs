using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private int xPos;
    private int yPos;
    private PlayerPhysicsScript playerPhysics;

    private bool isDashing;
    private int dashIntervalLength = 1;
    private const int numDashIntervals = 100;
    private int numIntervalsDashed;


    void Start() {
        xPos = 0;
        yPos = 0;
        isDashing = false;
        playerPhysics = new PlayerPhysicsScript(this);
    }
    public void changePlayerY(int deltaY)
    {
        yPos += deltaY;
    }
    private void playerDash()
    {
        playerPhysics.pausePlayerGravity();
        numIntervalsDashed = 0;
        isDashing = true;
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
                playerPhysics.unpausePlayerGravity();
            }
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
