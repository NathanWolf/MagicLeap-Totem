using System.Collections;
using UnityEngine;

// This behavior disables gravity initially and then re-enables it after some delay.
public class DelayGravity : MonoBehaviour {

	// Config constants
	public float Delay = 5;

	// Use this for initialization
	private void Start () {
		StartCoroutine(WaitAndEnable());
	}

	private IEnumerator WaitAndEnable() {
		var body = GetComponent<Rigidbody>();
		body.useGravity = false;
		yield return new WaitForSeconds(Delay);
		body.useGravity = true;
	}
}
