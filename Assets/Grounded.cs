using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a helper class that allows for grounding and upright checks
public class Grounded : MonoBehaviour {

	// Config constants
	public float groundDistance = 0.1f;
	public float uprightDegrees = 10.0f;

	// Components
	Rigidbody body;

	// Constants
	float distanceToBase;

	// State
	bool uprightDirty;
	bool upright;
	bool groundedDirty;
	bool grounded;

	void Start () {
		body = GetComponent<Rigidbody>();
		Collider collision = GetComponent<Collider>();
		distanceToBase = collision.bounds.extents.y;
	}

	void FixedUpdate() {
		uprightDirty = true;
		groundedDirty = true;
	}

	public bool IsUpright() {
		if (uprightDirty) {
			upright = (body.rotation.eulerAngles.z < uprightDegrees || body.rotation.eulerAngles.z > 360 - uprightDegrees) &&
					  (body.rotation.eulerAngles.x < uprightDegrees || body.rotation.eulerAngles.x > 360 - uprightDegrees);
			uprightDirty = false;
		}
		return upright;
	}

	public bool IsGrounded() {
		if (groundedDirty) {
			// return Physics.CheckBox(transform.position, bounds.extents.x, distanceToBase + 0.1);
			// return Physics.CheckCapsule(collider.bounds.center,new Vector3(collider.bounds.center.x,collider.bounds.min.y-0.1f,collider.bounds.center.z),0.18f));
			grounded = Physics.Raycast(transform.position, -Vector3.up, distanceToBase + 0.1f);
			groundedDirty = false;
		}
		return grounded;
	}
}
