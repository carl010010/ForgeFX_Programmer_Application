using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Socket : MonoBehaviour
{
    public StringEvent OnAttachedChanged;

    private bool _attached = false;
    public bool Attached
    {
        get => _attached;
        set
        {
            _attached = value;
            OnAttachedChanged?.Invoke(Attached.ToString());
        }
    }

    void Start()
    {
        SocketManager.Instance.AddSocketPosition(this);
    }
}
