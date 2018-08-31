using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a base behavior that allows for grounding and upright checks
public class Directional : MonoBehaviour {

	// Config constants
	public float rotationVelocity = 0.1f;
	public float lookAhead = 0.1f;

	// Constants
	float distanceToSide;

	// State
	Vector3 direction;

	void Start () {
		Collider collision = GetComponent<Collider>();
		distanceToSide = (collision.bounds.extents.x + collision.bounds.extents.z) / 2;
		RandomDirection();
	}

	void FixedUpdate() {
		checkFacing();
	}

	void checkFacing() {
		// TODO
	}

	public void RandomDirection() {
		direction = new Vector2(Random.value, Random.value);
 		direction.Normalize();
	}

	public bool isFacingDirection() {
		// TODO ...
		return true;
	}

	public bool CheckDirection() {
		return !Physics.Raycast(transform.position, direction, distanceToSide + lookAhead);
	}

	public Vector3 getDirection() {
		return direction;
	}

	public void setDirection(Vector3 d) {
		direction = d;
	}
}
