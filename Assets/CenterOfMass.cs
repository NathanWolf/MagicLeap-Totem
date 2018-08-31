using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This behavior will adjust the body's center of mass
public class CenterOfMass : MonoBehaviour {

	void Start () {
		// Move the center of mass to the location of the empty COM object
		Rigidbody body = GetComponent<Rigidbody>();
		Transform centerOfMass = gameObject.transform.Find("CenterOfMass");
		body.centerOfMass = centerOfMass.localPosition;
	}
}
