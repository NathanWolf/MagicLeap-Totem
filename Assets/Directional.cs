using UnityEngine;

// This is a base behavior that allows for grounding and upright checks
public class Directional : MonoBehaviour {

	// Config constants
	public float RotationVelocity = 0.1f;
	public float LookAhead = 0.1f;

	// Constants
	private float _distanceToSide;

	// State
	private Vector3 _direction;

	private void Start () {
		var collision = GetComponent<Collider>();
		_distanceToSide = (collision.bounds.extents.x + collision.bounds.extents.z) / 2;
		RandomDirection();
	}

	private void FixedUpdate() {
		CheckFacing();
	}

	private void CheckFacing() {
		// TODO
	}

	public void RandomDirection() {
		_direction = new Vector2(Random.value, Random.value);
 		_direction.Normalize();
	}

	public bool IsFacingDirection() {
		// TODO ...
		return true;
	}

	public bool CheckDirection() {
		return !Physics.Raycast(transform.position, _direction, _distanceToSide + LookAhead);
	}

	public Vector3 GetDirection() {
		return _direction;
	}

	public void SetDirection(Vector3 d) {
		_direction = d;
	}
}
