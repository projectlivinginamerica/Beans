using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[ExecuteInEditMode]
public class TrackConnection : MonoBehaviour
{
    public static readonly int NUM_LOCATORS_PER_CONNECTION = 4;

    [SerializeField]
    private ConnectionType type;
    public ConnectionType GetConnectionType() { return type; }

    TrackConnectionLocator[] connectionLocators = new TrackConnectionLocator[NUM_LOCATORS_PER_CONNECTION];

    void Start()
    {
        TrackConnectionLocator[] childLocators = GetComponentsInChildren<TrackConnectionLocator>();
        Assert.IsTrue(childLocators != null);
        Assert.IsTrue(childLocators.Length == NUM_LOCATORS_PER_CONNECTION);
        
        for(int i = 0; i < childLocators.Length; i++)
        {
            int index = (int) childLocators[i].position;
            Assert.IsTrue(connectionLocators[index] == null, "Cannot have multiple locators in the same position within one connection!");
            connectionLocators[index] = childLocators[i];
        }
    }
}

public enum ConnectionType
{
    Input,
    Output
}
