using UnityEngine;

public class ProceduralTrackGenerator : MonoBehaviour
{
    [SerializeField] ProcGenTrackSegment[] TrackSegmentList;
    [SerializeField] bool GenerateTrackNow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValidate()
    {
        if (GenerateTrackNow == true)
        {
            GenerateTrack();
        }
    }

    private void GenerateTrack()
    {
        Debug.Log("GenerateTrack Now!");
        GenerateTrackNow = false;

        Transform genTrackParent = transform.Find("Generated Track");
        if (genTrackParent != null)
        {
            genTrackParent.DetachChildren();
        }
        else
        {
            genTrackParent = new GameObject("Generated Track").transform;
            genTrackParent.position = Vector3.zero;
            genTrackParent.rotation = Quaternion.identity;
            genTrackParent.transform.parent = transform;
        }

        GameObject prev = null;
        for (int i = 0; i < TrackSegmentList.Length; i++)
        {
            GameObject newSegment = GameObject.Instantiate(TrackSegmentList[i].gameObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
            newSegment.transform.parent = genTrackParent;
            if (i > 0)
            {
                SnapSegment(prev.GetComponent<ProcGenTrackSegment>(), newSegment.GetComponent<ProcGenTrackSegment>());

            }
            prev = newSegment;
        }
    }

	bool SnapSegment(ProcGenTrackSegment startSegment, ProcGenTrackSegment nextSegment)
    {
        if (startSegment == null || nextSegment == null)
        {
            return false;
        }

        Transform startSegmentConnection = startSegment.transform.Find("TrackSegment_End");
        if (startSegmentConnection == null)
        {
            return false;
        }

        Transform endSegmentConnection = nextSegment.transform.Find("TrackSegment_Start");
        if (endSegmentConnection == null)
        {
            return false;
        }

        Debug.Log("Snaping Segment " + startSegment + ", " +  nextSegment);

        Vector3 endConnectEulerAngles = endSegmentConnection.transform.eulerAngles;
        Vector3 startConnectEulerAngles = startSegmentConnection.transform.eulerAngles;

        Debug.Log("Start Angles = " + startConnectEulerAngles + ", end Angles = " + endConnectEulerAngles);

       // endSegmentConnection.transform.rotation.to2
        startConnectEulerAngles = new Vector3(startConnectEulerAngles.x, startConnectEulerAngles.y, startConnectEulerAngles.z);
            nextSegment.transform.eulerAngles = startConnectEulerAngles;
        Vector3 endSegmentConnectionOffset = endSegmentConnection.transform.position - nextSegment.transform.position;

        nextSegment.transform.position = startSegmentConnection.transform.position - endSegmentConnectionOffset;
      //  nextSegment.transform.position = connectionTransform.position;
      //  nextSegment.transform.rotation = connectionTransform.rotation;

        return true;
    }
}
