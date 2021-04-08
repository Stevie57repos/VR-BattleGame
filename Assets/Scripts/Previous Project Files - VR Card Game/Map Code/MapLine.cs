using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLine : MonoBehaviour
{
    //public GameObject startPos;
    public GameObject EndPos;

    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetLine();
    }

    void SetLine()
    {
        Vector3 lineStart = transform.position;
        Vector3 lineEnd = EndPos.transform.position;

        lineRenderer.SetPosition(0, lineStart);
        lineRenderer.SetPosition(1, lineEnd);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
