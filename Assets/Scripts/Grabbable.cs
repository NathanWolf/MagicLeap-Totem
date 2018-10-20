using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Grabbable : MonoBehaviour
{
	public float RequiredConfidence = 0.9f;
	public float GrabDistance = 2.0f;
	
	private Rigidbody _body;
	private bool grabbed;
	
	// Use this for initialization
	private void Start () {
		_body = GetComponent<Rigidbody>();
		_body.useGravity = false;
		grabbed = true;
	}
	
	// Update is called once per frame
	private void Update()
	{
		MLHandKeyPose pose = MLHandKeyPose.NoHand;
		if (MLHands.Right.KeyPoseConfidence >= RequiredConfidence && MLHands.Right.KeyPose != MLHandKeyPose.NoHand)
		{
			pose = MLHands.Right.KeyPose;
		} 
		else if (MLHands.Left.KeyPoseConfidence >= RequiredConfidence)
		{
			pose = MLHands.Left.KeyPose;
		}

		switch (pose)
		{
			case MLHandKeyPose.C:
				grabbed = false;
				_body.AddForce(Vector3.zero);
				_body.useGravity = true;
				break;
			case MLHandKeyPose.Pinch:
				grabbed = true;
				_body.AddForce(Vector3.zero);
				_body.useGravity = false;
				break;
		}

		if (grabbed)
		{
			_body.transform.position = Camera.main.transform.position + Camera.main.transform.forward * GrabDistance;
		}
	}
}
