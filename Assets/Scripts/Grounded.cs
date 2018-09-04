using UnityEngine;

// This is a helper class that allows for grounding and upright checks
public class Grounded : MonoBehaviour {

	// Config constants
	public float GroundDistance = 0.1f;
	public float UprightDegrees = 10.0f;

	// Components
	private Rigidbody _body;
	private Collider _collision;
	private Vector3 _collisionExtents;

	// State
	private bool _uprightDirty;
	private bool _upright;
	private bool _groundedDirty;
	private bool _grounded;
	private int _layerMask;

	private void Start () {
		_body = GetComponent<Rigidbody>();
		_collision = GetComponent<Collider>();
		_collisionExtents = _collision.bounds.extents;
		_collisionExtents.y = GroundDistance;
		_layerMask = 1 << gameObject.layer;
		_layerMask = ~_layerMask;
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

		var queryLocation = transform.position;
		queryLocation.y = _collision.bounds.min.y;
		_grounded = Physics.CheckBox(queryLocation, _collisionExtents, Quaternion.identity, _layerMask);
		_groundedDirty = false;
		
		//ExtDebug.DrawBox(_collision.bounds.center, _collision.bounds.extents, Quaternion.identity, Color.green);
		//ExtDebug.DrawBox(queryLocation, _collisionExtents, Quaternion.identity, Color.red);
		
		return _grounded;
	}
}
