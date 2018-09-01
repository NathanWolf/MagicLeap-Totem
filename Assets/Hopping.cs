using UnityEngine;

// This behavior makes a body randomly hop around
public class Hopping : MonoBehaviour {

	// Config constants
	public float Speed = 2.0f;
	public float JumpStrength = 10.0f;
	public float JumpCooldown = 0.5f;
	public float TurnAmount = 1;

	// Components
	private Rigidbody _body;
	private Directional _direction;
	private Grounded _ground;

	// State
	private float _lastJump;

	private void Start () {
		_body = GetComponent<Rigidbody>();
		_direction = GetComponent<Directional>();
		_ground = GetComponent<Grounded>();
	}

	private void FixedUpdate()
	{
		if (!_ground.IsGrounded() || !_ground.IsUpright()) return;
		if (_direction.CheckCollision()) {
			if (!_direction.IsFacingDirection() || IsJumpOnCooldown()) return;
			Jump();	
			_lastJump = Time.time;
		} else {
			_direction.ChangeDirection(TurnAmount);
		}
	}

	private bool IsJumpOnCooldown()
	{
		return (Time.time < _lastJump + JumpCooldown);
	}

	private void Jump() {
		var jump = Vector3.up * JumpStrength + _direction.GetDirection() * Speed;
		_body.AddForce(jump.x, jump.y, jump.z, ForceMode.Impulse);
		_lastJump = Time.time;
	}
}
