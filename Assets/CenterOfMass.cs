using UnityEngine;

// This behavior will adjust the body's center of mass
public class CenterOfMass : MonoBehaviour {
	private void Start () {
		// Move the center of mass to the location of the empty COM object
		var body = GetComponent<Rigidbody>();
		var centerOfMass = gameObject.transform.Find("CenterOfMass");
		body.centerOfMass = centerOfMass.localPosition;
	}
}
