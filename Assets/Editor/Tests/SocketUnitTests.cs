using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class SocketUnitTests
{
    public Socket socket;

    [SetUp]
    public void SetUp()
    {
        socket = new GameObject("Socket").AddComponent<Socket>();
    }

    [Test]
    public void TestSocketIsNotNull()
    {
        Assert.IsNotNull(socket);
    }

    [Test]
    public void TestSocketAttachedIsFalse()
    {
        Assert.IsFalse(socket.Attached);
    }

    [Test]
    public void TestSocketAttachedIsTrue()
    {
        new GameObject("test").transform.parent = socket.transform;
        socket.CallStart();

        Assert.IsTrue(socket.Attached);
    }

    [Test]
    public void TestSocketEventDoesSendDetached()
    {
        string testString = null; 

        socket.OnAttachedChanged.AddListener((str) => {
            testString = str;
            Assert.AreEqual("Socket Detached", str);
        });
        
        socket.CallStart();

        Assert.NotNull(testString);
    }

    [Test]
    public void TestSocketEventDoesSendAttached()
    {
        string testString = null;

        socket.OnAttachedChanged.AddListener((str) => {
            testString = str;
            Assert.AreEqual("Socket Attached", str);
        });

        new GameObject("test").transform.parent = socket.transform;
        socket.CallStart();

        Assert.NotNull(testString);
    }

}
