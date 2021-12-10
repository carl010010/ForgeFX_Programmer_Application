using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

[RequireComponent(typeof(Rigidbody))]
public class RobotArmDetatch : MonoBehaviour
{
    [Tooltip("The max distance squared to a socket that will attach the arm")]
    public float AttachDistanceSqr = 0.01f;

    Rigidbody _rigidbody;
    Plane _plane;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _plane = new Plane(Vector3.forward, 0);
    }

    private void Update()
    {
        if (transform.parent != null)
            return;

        Vector3 pos = transform.position;

        if(pos.y < 0)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            transform.position = new Vector3(pos.x, 0.5f, pos.z);
        }

        if(transform.parent == null)
        {
            foreach(Socket socket in SocketManager.Instance.GetEnumerator())
            {
                float distSqr = Vector3.SqrMagnitude(pos - socket.transform.position);
                if(distSqr < AttachDistanceSqr)
                {
                    transform.parent = socket.transform;
                    socket.Attached = true;
                    _rigidbody.isKinematic = true;
                    transform.position = socket.transform.position;
                    transform.rotation = socket.transform.rotation;
                }
            }
        }
    }

    private void OnMouseDrag()
    {
        _rigidbody.isKinematic = true;
        transform.parent = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (_plane.Raycast(ray, out float distance))
        {
            gameObject.transform.position = ray.GetPoint(distance);
        }
    }

    private void OnMouseUp()
    {
        if(transform.parent == null)
            _rigidbody.isKinematic = false;
    }
}
