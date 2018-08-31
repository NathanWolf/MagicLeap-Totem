using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This behavior disables gravity initially and then re-enables it after some delay.
public class DelayGravity : MonoBehaviour {

	// Config constants
	public float delay = 5;

	// Use this for initialization
	void Start () {
		StartCoroutine(WaitAndEnable());
	}

	IEnumerator WaitAndEnable() {
		Rigidbody body = GetComponent<Rigidbody>();
		body.useGravity = false;
		yield return new WaitForSeconds(delay);
		body.useGravity = true;
	}
}
