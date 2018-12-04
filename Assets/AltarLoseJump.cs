using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltarLoseJump : MonoBehaviour {

	private bool used;
	public GameObject player;
	public Canvas canvas;

	// Use this for initialization
	void Start () {
		used = false;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerScript ps = player.GetComponent<PlayerScript>();
		if (Vector3.Distance(player.transform.position, transform.position) < 8f && !used && ps.hasTeleportAbility && ps.hasJumpAbility) {
			used = true;
			ps.hasJumpAbility = false;
			Debug.Log("Activated");
            canvas.GetComponentInChildren<Text>().color = Color.cyan;
            canvas.GetComponentInChildren<Text>().text = "Sacrificed Jump!";
            canvas.GetComponentInChildren<Text>().GetComponent<TextFadeOut>().FadeOut();
        }
		if (used)
		GetComponent<ParticleSystem>().Stop();
	}
}
