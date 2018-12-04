using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarTemplate : MonoBehaviour {

	private bool used;
	public GameObject player;

	// Use this for initialization
	void Start () {
		used = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(player.transform.position, transform.position) < 30f && !used) {
			used = true;
			// CODE TO DO SOMETHING GOES HERE
		}
	}
}
