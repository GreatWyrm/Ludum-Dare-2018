using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class FinalAltar : MonoBehaviour {

	private bool used;
	public GameObject player;
	
	public Text text;
	// Use this for initialization
	void Start () {
		used = false;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerScript ps = player.GetComponent<PlayerScript>();
		if (Vector3.Distance(player.transform.position, transform.position) < 8f && !used) {
			used = true;
			
			Debug.Log("Activated");
			text.text = "Sacrificed EVERYTHING!";

		}
		if (used)
		GetComponent<ParticleSystem>().Stop();
	}
}
