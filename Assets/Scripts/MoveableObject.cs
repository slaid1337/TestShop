using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Attach(Transform origin)
    {
        transform.position = origin.position;
        transform.parent = origin;
        _rb.isKinematic = true;
    }

    public void Push(Vector3 direction)
    {
        transform.parent = null;
        _rb.isKinematic = false;
        _rb.AddForce(direction);
    }
}
