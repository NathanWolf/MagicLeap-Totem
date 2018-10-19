using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ToggleVisibility : MonoBehaviour {
	private Renderer _renderer;
	public float RequiredConfidence = 0.9f;

	// Use this for initialization
	void Start ()
	{
		_renderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
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
			case MLHandKeyPose.Fist:
				_renderer.enabled = true;
				break;
			case MLHandKeyPose.Thumb:
				_renderer.enabled = false;
				break;
		}
	}
}
