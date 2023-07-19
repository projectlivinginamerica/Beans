using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackConnection : MonoBehaviour
{
    [SerializeField]
    private ConnectionType type;
}

public enum ConnectionType
{
    Input,
    Output
}
