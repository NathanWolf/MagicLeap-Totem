using UnityEngine;
using UnityEngine.XR.MagicLeap;

[RequireComponent(typeof(HandTracking))]
public class Grabbable : MonoBehaviour
{
	public float RequiredConfidence = 0.9f;
	
	private Rigidbody _body;
	
	// Use this for initialization
	void Start () {
		_body = GetComponent<Rigidbody>();
		_body.useGravity = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (MLHands.Right.KeyPoseConfidence >= RequiredConfidence)
		{
			Debug.Log(MLHands.Right.KeyPose);	
		} 
		else if (MLHands.Left.KeyPoseConfidence >= RequiredConfidence)
		{
			Debug.Log(MLHands.Left.KeyPose);
		}
	}
	
	void OnEnable()
	{
		MLResult result = MLHands.Start();
		if (!result.IsOk)
		{
			Debug.LogError("Error GesturesExample starting MLHands, disabling script.");
			enabled = false;
		}
	}
	
	void OnDisable()
	{
		MLHands.Stop();
	}
}
