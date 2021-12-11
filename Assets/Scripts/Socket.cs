using UnityEngine;
using Utils;

public class Socket : MonoBehaviour
{
    public StringEvent OnAttachedChanged = new StringEvent();
    public GameObject PreferredGameObject;

    private bool _attached = false;
    public bool Attached
    {
        get => _attached;
        set
        {
            _attached = value;

            string text = name;

            if(!_attached)
            {
                text += " Detached";
            }
            else if(PreferredGameObject == null || PreferredGameObject.Equals(null) || transform.GetChild(0).name == PreferredGameObject.name)
            {
                text += " Attached";
            }
            else
            {
                text = "This feels funny.";
            }   

            OnAttachedChanged?.Invoke(text);
        }
    }

    void Start()
    {
        SocketManager.Instance.AddSocketPosition(this);

        if(transform.childCount > 0)
            Attached = true;
        else
            Attached = false;
    }
}
