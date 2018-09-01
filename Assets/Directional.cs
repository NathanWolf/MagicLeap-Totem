using UnityEngine;

// This is a base behavior that allows for grounding and upright checks
public class Directional : MonoBehaviour {

	// Config constants
	public float DirectionTolerance = 5;

	// Components
	private Collider _collision;
	private Rigidbody _body;
	
	// Constants
	private float _collisionOffset;
	private Vector3 _collisionExtents;
	private int _layerMask;

	// State
	private Vector3 _direction;

	private void Start ()
	{
		_body = GetComponent<Rigidbody>();
		_collision = GetComponent<Collider>();
		_collisionExtents = _collision.bounds.extents;
		_collisionOffset = (_collision.bounds.extents.x + _collision.bounds.extents.z);
		_layerMask = 1 << gameObject.layer;
		_layerMask = ~_layerMask;
		RandomDirection();
	}

	private void FixedUpdate() {
		CheckFacing();
	}

	private void CheckFacing() {
		if (!IsFacingDirection())
		{
			_body.MoveRotation(Quaternion.LookRotation(_direction));
		}
	}

	public void RandomDirection()
	{
		_direction = new Vector3(Random.value, 0, Random.value);
		_direction.Normalize();
	}

	public void ChangeDirection(float angle)
	{
		_direction = Quaternion.Euler(0, angle, 0) * _direction;
		_direction.Normalize();
	}

	public bool IsFacingDirection()
	{
		/*
		Debug.Log("Facing: " + Mathf.DeltaAngle(_body.rotation.eulerAngles.y, Quaternion.LookRotation(_direction).eulerAngles.y) + " from " + 
		          _body.rotation.eulerAngles.y + " and " + Quaternion.LookRotation(_direction).eulerAngles.y + " direction: " + _direction);
	    */
		return Mathf.Abs(Mathf.DeltaAngle(_body.rotation.eulerAngles.y, Quaternion.LookRotation(_direction).eulerAngles.y)) < DirectionTolerance;
	}

	public bool CheckCollision()
	{
		var queryLocation = _collision.bounds.center;
		queryLocation += _direction * _collisionOffset;
		Debug.DrawLine(_collision.bounds.center, _collision.bounds.center + _direction * 2, Color.green, 10.0f, false);
		//ExtDebug.DrawBox(queryLocation, _collisionExtents, Quaternion.identity, Color.red);
		return !Physics.CheckBox(queryLocation, _collisionExtents, Quaternion.identity, _layerMask);
	}

	public Vector3 GetDirection() {
		return _direction;
	}

	public void SetDirection(Vector3 d) {
		_direction = d;
	}
}
