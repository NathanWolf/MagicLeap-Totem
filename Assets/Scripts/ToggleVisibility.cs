using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ToggleVisibility : MonoBehaviour {
	private Renderer _renderer;
	public Material occlusion;
	public Material wireframe;
	public float RequiredConfidence = 0.9f;


	private Material[] _materials; 

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
			case MLHandKeyPose.L:
				Material[] materials1 = _renderer.materials;
				materials1[0] = wireframe;
				_renderer.materials = materials1;
				break;
			case MLHandKeyPose.Thumb:
				Material[] materials2 = _renderer.materials;
				materials2[0] = occlusion;
				_renderer.materials = materials2;
				break;
		}
	}
}
