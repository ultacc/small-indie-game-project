using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class other_movemnt : MonoBehaviour
{
    public Transform backLeft;
    public Transform backRight;
    public Transform frontLeft;
    public Transform frontRight;
    public RaycastHit lr;
    public RaycastHit rr;
    public RaycastHit lf;
    public RaycastHit rf;
    public Vector3 upDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(backLeft.position + Vector3.up, Vector3.down, out lr);
        Physics.Raycast(backRight.position + Vector3.up, Vector3.down, out rr);
        Physics.Raycast(frontLeft.position + Vector3.up, Vector3.down, out lf);
        Physics.Raycast(frontRight.position + Vector3.up, Vector3.down, out rf);
        upDir = (Vector3.Cross(rr.point - Vector3.up, lr.point - Vector3.up) +
                 Vector3.Cross(lr.point - Vector3.up, lf.point - Vector3.up) +
                 Vector3.Cross(lf.point - Vector3.up, rf.point - Vector3.up) +
                 Vector3.Cross(rf.point - Vector3.up, rr.point - Vector3.up)
                ).normalized;
        Debug.DrawRay(rr.point, Vector3.up);
        Debug.DrawRay(lr.point, Vector3.up);
        Debug.DrawRay(lf.point, Vector3.up);
        Debug.DrawRay(rf.point, Vector3.up);
        transform.up = upDir;

    }
}

