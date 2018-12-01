using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private int xPos;
    private int yPos;
    private PlayerPhysicsScript playerPhysics;


    void Start() {
        xPos = 0;
        yPos = 0;
        playerPhysics = new PlayerPhysicsScript(this);
    }
    public void changePlayerY(int deltaY)
    {
        yPos += deltaY;
    }
    private void playerDash()
    {

    }
	// Update is called once per frame
	void Update () {
		
	}
}
