using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TrackConnectionLocator : MonoBehaviour
{
    private Transform _location;
    public LocatorPosition position;

    void Start()
    {
        _location = gameObject.transform;
    }
}

public enum LocatorPosition
{
    UpperLeft,
    UpperRight,
    LowerLeft,
    LowerRight
}
