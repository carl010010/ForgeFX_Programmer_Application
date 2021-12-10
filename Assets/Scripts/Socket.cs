using UnityEngine;
using Utils;

public class Socket : MonoBehaviour
{
    public StringEvent OnAttachedChanged = new StringEvent();
    public GameObject PreferedGameObject;

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
            else if(PreferedGameObject == null || PreferedGameObject.Equals(null) || transform.GetChild(0).name == PreferedGameObject.name)
            {
                text += " Attached";
            }
            else
            {
                text = "This feels Funny.";
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
