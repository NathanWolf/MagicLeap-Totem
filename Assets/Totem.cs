using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour {

	// Config constants
	public float speed = 0.1f;
	public float jumpStrength = 2.0f;
	public float uprightDegrees = 10.0f;
	public float jumpCooldown = 0.5f;

	// Components
	private Rigidbody body;
	private Collider collision;
	private float distanceToBase;

	// State
	private Vector3 direction;
	private float lastJump;

	// Use this for initialization
	void Start () {
		RandomDirection();
		body = GetComponent<Rigidbody>();
		collision = GetComponent<Collider>();
		distanceToBase = collision.bounds.extents.y;
		Transform centerOfMass = gameObject.transform.Find("CenterOfMass");
		body.centerOfMass = centerOfMass.localPosition;
		StartCoroutine(WaitAndEnable());
	}

	IEnumerator WaitAndEnable() {
		Debug.Log("Starting");
		Debug.Log("Disabling gravity");
		body.useGravity = false;
		yield return new WaitForSeconds(5);
		Debug.Log("Enabling gravity");
		body.useGravity = true;
	}

	void FixedUpdate() {
		if (Random.Range(0, 10) <= 1) {
			if (IsGrounded() && IsUpright()) {
				if (CheckDirection()) {
					Jump();	
				}
			}
		}
		// Move();
	}

	void RandomDirection() {
		direction = new Vector2(Random.value, Random.value);
 		direction.Normalize();
	}

	void Move() {
		Vector3 velocity = direction * speed;
		body.AddForce(velocity.x, velocity.y, velocity.z, ForceMode.Impulse);
	}

	bool IsUpright() {
		// TODO: Need to check x as well.. ?
		return body.rotation.eulerAngles.z < uprightDegrees || body.rotation.eulerAngles.z > 360 - uprightDegrees;
	}

	bool CheckDirection() {
		if (Physics.Raycast(transform.position, direction, distanceToBase + 0.1f)) {
			Debug.Log("Change direction");
			RandomDirection();
			return false;
		}
		// TODO: Orient to face direction
		return true;
	}

	void Jump() {
		//body.velocity = Vector3.up * 2;
		if (Time.time < lastJump + jumpCooldown) return;
		lastJump = Time.time;
		Vector3 jump = Vector3.up * jumpStrength + direction * speed;
		body.AddForce(jump.x, jump.y, jump.z, ForceMode.Impulse);
	}

	bool IsGrounded() {
		// return Physics.CheckBox(transform.position, bounds.extents.x, distanceToBase + 0.1);
		// return Physics.CheckCapsule(collider.bounds.center,new Vector3(collider.bounds.center.x,collider.bounds.min.y-0.1f,collider.bounds.center.z),0.18f));
		return Physics.Raycast(transform.position, -Vector3.up, distanceToBase + 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
