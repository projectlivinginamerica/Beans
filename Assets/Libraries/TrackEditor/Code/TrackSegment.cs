using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TrackSegment : MonoBehaviour
{
    public List<TrackConnection> inConnections;
    public List<TrackConnection> outConnections;

    // Start is called before the first frame update
    void Start()
    {
        TrackConnection[] childConnections = GetComponentsInChildren<TrackConnection>();
        if(childConnections == null) return;
    
        for(int i = 0; i < childConnections.Length; i++)
        {
            if(childConnections[i].GetConnectionType() == ConnectionType.Input)
            {
                inConnections.Add(childConnections[i]);
            }
            else
            {
                outConnections.Add(childConnections[i]);
            }
        }
    }
}
