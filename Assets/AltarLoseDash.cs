using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltarLoseDash : MonoBehaviour {

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
		if (Vector3.Distance(player.transform.position, transform.position) < 8f && !used && ps.hasDashAbility && ps.hasJumpAbility) {
			used = true;
			ps.hasDashAbility = false;
			Debug.Log("Activated");
			canvas.GetComponent<Text>().text = "Sacrificed death!";
			canvas.GetComponent<TextFadeOut>().FadeOut();
		}
		if (used)
		GetComponent<ParticleSystem>().Stop();
	}
}
