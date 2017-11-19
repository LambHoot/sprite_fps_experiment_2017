using UnityEngine;
using System.Collections;

public class BillboardBehavior : MonoBehaviour {

    public GameObject target;
    public bool vertical;

    void Start () {
	
	}
	
	void Update () {
        lookAtTarget();
    }

    //from http://wiki.unity3d.com/index.php?title=CameraFacingBillboard
    void lookAtTarget()
    {
        transform.LookAt(transform.position + target.transform.rotation * Vector3.forward,
            target.transform.rotation * Vector3.up);
        if (!vertical)
        {
            transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);
        }
    }

}
