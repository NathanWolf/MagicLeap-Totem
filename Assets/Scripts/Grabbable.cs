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
				_body.useGravity = true;
				break;
			case MLHandKeyPose.Ok:
				grabbed = true;
				_body.useGravity = false;
				break;
		}

		if (grabbed)
		{
			_body.transform.position = Camera.main.transform.position + Camera.main.transform.forward * GrabDistance;
		}
	}

	private void OnEnable()
	{
		MLResult result = MLHands.Start();
		if (!result.IsOk)
		{
			Debug.LogError("Error GesturesExample starting MLHands, disabling script.");
			enabled = false;
		}
		else
		{
			MLHandKeyPose[] poseSet = {MLHandKeyPose.C, MLHandKeyPose.Ok};
			var status = MLHands.KeyPoseManager.EnableKeyPoses(poseSet, true, true);
			if (!status)
			{
				Debug.LogError("HandTracking failed during a call to enable tracked KeyPoses.\n"
				               + "Disabling HandTracking component.");
				enabled = false;
			}
		}
	}

	private void OnDisable()
	{
		MLHands.Stop();
	}
}
