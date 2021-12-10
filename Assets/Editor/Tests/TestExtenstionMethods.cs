using System.Reflection;
using UnityEngine;

public static class TestExtenstionMethods
{
    public static void CallStart(this MonoBehaviour mono)
    {
        mono.GetType().GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Invoke(mono,null);
    }
}
