using UnityEngine;
using Random = UnityEngine.Random;

// This behavior makes a body randomly hop around
public class Hopping : MonoBehaviour {

	// Config constants
	public float Speed = 2.0f;
	public float JumpStrength = 10.0f;
	public float JumpCooldown = 0.5f;
	public float TurnAmount = 1;
	public float MaxFall = 5.0f;
	public float LandSoundCooldown = 0.1f;
	public AudioClip BounceSound;
	public float BounceMinPitch = 0.5f;
	public float BounceMaxPitch = 1.5f;
	public float BounceVolume = 1.0f;
	public AudioClip LandSound;
	public float LandMinPitch = 0.5f;
	public float LandMaxPitch = 1.5f;
	public float LandVolume = 1.0f;
	
	// TODO: Is it possible to calculate these automatically based on JumpStrength?
	public float MaxJump = 1.0f;
	public float ForwardJump = 1.0f;

	// Components
	private Rigidbody _body;
	private Directional _direction;
	private Grounded _ground;
	private AudioSource _bounceSource;
	private AudioSource _landSource;

	// State
	private float _lastJump;
	private float _lastCollision;

	private void Start () {
		_body = GetComponent<Rigidbody>();
		_direction = GetComponent<Directional>();
		_ground = GetComponent<Grounded>();
	}

	private void FixedUpdate()
	{
		if (!_ground.IsGrounded() || !_ground.IsUpright()) return;
		if (_direction.CheckCollision() && _direction.CheckFloor(ForwardJump, MaxFall, MaxJump)) {
			if (IsJumpOnCooldown()) return;
			_direction.UpdateBody();
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
		if (BounceSound != null)
		{
			if (_bounceSource == null)
			{
				_bounceSource = gameObject.AddComponent<AudioSource>();
				_bounceSource.clip = BounceSound;
				_bounceSource.volume = BounceVolume;
			}

			_bounceSource.pitch = Random.Range(BounceMinPitch, BounceMaxPitch);
			_bounceSource.Play();
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (LandSound != null && Time.time > _lastCollision + LandSoundCooldown)
		{
			if (_landSource == null)
			{
				_landSource = gameObject.AddComponent<AudioSource>();
				_landSource.clip = LandSound;
				_landSource.volume = LandVolume;
			}

			_landSource.pitch = Random.Range(LandMinPitch, LandMaxPitch);
			_landSource.Play();
			_lastCollision = Time.time;
		}
	}
}
