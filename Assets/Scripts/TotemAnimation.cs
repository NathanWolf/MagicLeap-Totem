using UnityEngine;
using UnityEngine.Animations;

public class TotemAnimation : MonoBehaviour
{
    // Components
    private Rigidbody _body;
    private Transform _head;
    private Directional _direction;
    
    private void Start () {
        _body = GetComponent<Rigidbody>();
        _direction = GetComponent<Directional>();
        var body1 = transform.Find("Body");
        var body2 = body1.Find("Body2");
        var body3 = body2.Find("Body3");
        _head = body3.Find("Head");
    }

    private void FixedUpdate()
    {
        SpinHead();
    }

    private void SpinHead()
    {
        _head.rotation = Quaternion.LookRotation(_direction.GetDirection()) * Quaternion.Inverse(_body.rotation);
        Debug.Log(" Head: " + _head.rotation + " from " + _direction.GetDirection() + " and " + _body.rotation);
    }
}
