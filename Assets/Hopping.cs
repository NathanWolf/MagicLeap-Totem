using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This behavior makes a body randomly hop around
public class Hopping : MonoBehaviour {

	// Config constants
	public float speed = 2.0f;
	public float jumpStrength = 10.0f;
	public float jumpCooldown = 0.5f;

	// Components
	Rigidbody body;
	Directional direction;
	Grounded ground;

	// State
	float lastJump;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		direction = GetComponent<Directional>();
		ground = GetComponent<Grounded>();
	}

	void FixedUpdate() {
		if (Random.Range(0, 10) <= 1) {
			if (ground.IsGrounded() && ground.IsUpright()) {
				if (direction.CheckDirection()) {
					Jump();	
				} else {
					Debug.Log("Change direction");
					direction.RandomDirection();
				}
			}
		}
	}

	void Jump() {
		//body.velocity = Vector3.up * 2;
		if (Time.time < lastJump + jumpCooldown) return;
		lastJump = Time.time;
		Vector3 jump = Vector3.up * jumpStrength + direction.getDirection() * speed;
		body.AddForce(jump.x, jump.y, jump.z, ForceMode.Impulse);
	}
}
