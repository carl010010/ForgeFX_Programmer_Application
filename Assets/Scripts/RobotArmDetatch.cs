using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RobotArmDetatch : MonoBehaviour
{
    [Tooltip("The max distance squared to a socket that will attach the arm")]
    public float AttachDistanceSqr = 0.01f;


    public Material highlightMaterial;
    [Range(0, 1)]
    public float brightness = 0.4f;
    
    public List<MeshRenderer> MeshRenderersToHighlight = new List<MeshRenderer>();
    
    private List<Material> _materials = new List<Material>();
    private Rigidbody _rigidbody;
    private Plane _plane;

    private Socket _socket;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _plane = new Plane(Vector3.forward, 0);

        if (highlightMaterial == null)
        {
            Debug.LogError("No highlightMaterial found on " + name, this);
        }

        if(transform.parent != null)
        {
            _socket = transform.parent.GetComponent<Socket>();
        }


        foreach (MeshRenderer mr in MeshRenderersToHighlight)
        {
            if (mr != null)
            {
                _materials.Add(mr.material);
            }
        }

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
                if(socket.Attached == false)
                {
                    float distSqr = Vector3.SqrMagnitude(pos - socket.transform.position);
                    if(distSqr < AttachDistanceSqr)
                    {
                        _socket = socket;
                        transform.parent = _socket.transform;
                        _socket.Attached = true;
                        _rigidbody.isKinematic = true;
                        transform.position = _socket.transform.position;
                        transform.rotation = _socket.transform.rotation;
                    }
                }
            }
        }
    }

    private void OnMouseDrag()
    {
        _rigidbody.isKinematic = true;
        transform.parent = null;

        if(_socket != null && !_socket.Equals(null))
        {
            _socket.Attached = false;
            _socket = null;
        }

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

        UnHighlight();
    }

    private void OnMouseOver()
    {
        Highlight();
    }

    private void OnMouseExit()
    {
        if (!_rigidbody.isKinematic || transform.parent != null) //This covers a bug where the user is dragging the arm to fast the highlight flickers
        {
            UnHighlight();
        }
    }

    private void Highlight()
    {
        for (int i = 0; i < MeshRenderersToHighlight.Count; i++)
        {
            MeshRenderersToHighlight[i].material = highlightMaterial;
        }
    }

    private void UnHighlight()
    {
        for (int i = 0; i < MeshRenderersToHighlight.Count; i++)
        {
            MeshRenderersToHighlight[i].material = _materials[i];
        }
    }

}
