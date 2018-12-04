using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltarGainGravity : MonoBehaviour {

	private bool used;
	public Canvas canvas;
	public GameObject player;

	// Use this for initialization
	void Start () {
		used = false;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerScript ps = player.GetComponent<PlayerScript>();
		if (Vector3.Distance(player.transform.position, transform.position) < 8f && !used && ps.hasTeleportAbility && !ps.hasJumpAbility) {
			used = true;
			ps.hasGravityAbility = true;
			Debug.Log("Activated");
            canvas.GetComponentInChildren<Text>().color = Color.cyan;
            canvas.GetComponentInChildren<Text>().text = "Granted gravity switching!";
            canvas.GetComponentInChildren<Text>().GetComponent<TextFadeOut>().FadeOut();

        }
		if (used)
		GetComponent<ParticleSystem>().Stop();
	}
}
