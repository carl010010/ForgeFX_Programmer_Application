using System.Collections.Generic;
using UnityEngine;

public class SocketManager: MonoBehaviour
{

    private static SocketManager _instance;
    public static SocketManager Instance
    {
        get 
        { 
            if(_instance == null || _instance.Equals(null))
            {
                GameObject socketManager = new GameObject("socketManager");
                _instance = socketManager.AddComponent<SocketManager>();
            }

            return _instance; 
        }
    }

    private List<Socket> _socketPositions = new List<Socket>();

    public void AddSocketPosition(Socket pos)
    {
        _socketPositions.Add(pos);
    }

    public void RemoveSocketPosition(Socket pos)
    {
        _socketPositions.Remove(pos);
    }

    public IEnumerable<Socket> GetEnumerator()
    {
        return _socketPositions.AsReadOnly();
    }

    private void OnDrawGizmos()
    {
        foreach (var socket in _socketPositions)
        {
            Gizmos.DrawWireSphere(socket.transform.position, 0.1f);
        }
    }
}
