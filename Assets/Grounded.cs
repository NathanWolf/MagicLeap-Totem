using UnityEngine;

// This is a helper class that allows for grounding and upright checks
public class Grounded : MonoBehaviour {

	// Config constants
	public float GroundDistance = 0.1f;
	public float UprightDegrees = 10.0f;

	// Components
	private Rigidbody _body;

	// Constants
	private float _distanceToBase;

	// State
	private bool _uprightDirty;
	private bool _upright;
	private bool _groundedDirty;
	private bool _grounded;

	private void Start () {
		_body = GetComponent<Rigidbody>();
		var collision = GetComponent<Collider>();
		_distanceToBase = collision.bounds.extents.y;
	}

	private void FixedUpdate() {
		_uprightDirty = true;
		_groundedDirty = true;
	}

	public bool IsUpright() {
		if (!_uprightDirty) return _upright;
		
		_upright = (_body.rotation.eulerAngles.z < UprightDegrees || _body.rotation.eulerAngles.z > 360 - UprightDegrees) &&
		           (_body.rotation.eulerAngles.x < UprightDegrees || _body.rotation.eulerAngles.x > 360 - UprightDegrees);
		_uprightDirty = false;
		return _upright;
	}

	public bool IsGrounded() {
		if (!_groundedDirty) return _grounded;
		
		// return Physics.CheckBox(transform.position, bounds.extents.x, distanceToBase + 0.1);
		// return Physics.CheckCapsule(collider.bounds.center,new Vector3(collider.bounds.center.x,collider.bounds.min.y-0.1f,collider.bounds.center.z),0.18f));
		_grounded = Physics.Raycast(transform.position, -Vector3.up, _distanceToBase + 0.1f);
		_groundedDirty = false;
		return _grounded;
	}
}
