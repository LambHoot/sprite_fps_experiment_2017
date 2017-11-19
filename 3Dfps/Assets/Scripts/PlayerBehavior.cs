using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    public float playerSpeed;
    float vHorizontal, vVertical;//for movement

    //For mouselook
    public bool lockCursor = false;

    public float sensitivity = 30;
    public int smoothing = 10;

    float ymove;
    float xmove;

    int iteration = 0;

    float xaggregate = 0;
    float yaggregate = 0;

    //int Ylimit = 0;
    public int Xlimit = 20;

    // Use this for initialization
    void Start () {
        playerSpeed = 0.1f;

        //for mouselook
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
	
	// Update is called once per frame
	void Update () {
        movePlayer();
        mouseLook();
    }

    void movePlayer()
    {
        vHorizontal = Input.GetAxis("Horizontal");
        vVertical = Input.GetAxis("Vertical");
        if (!Mathf.Approximately(vVertical, 0.0f) || !Mathf.Approximately(vHorizontal, 0.0f))
        {
            Vector3 direction = new Vector3(vHorizontal, 0.0f, vVertical);
            direction = Vector3.ClampMagnitude(direction * playerSpeed, 1.0f);
            transform.Translate(direction, Space.Self);
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }
    }

    void mouseLook()
    {
        //from https://forum.unity3d.com/threads/a-free-simple-smooth-mouselook.73117/
        // ensure mouseclicks do not effect the screenlock
        if (lockCursor)
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        float[] x = new float[smoothing];
        float[] y = new float[smoothing];

        // reset the aggregate move values
        xaggregate = 0;
        yaggregate = 0;

        // receive the mouse inputs
        ymove = Input.GetAxis("Mouse Y");
        xmove = Input.GetAxis("Mouse X");

        // cycle through the float arrays and lop off the oldest value, replacing with the latest
        y[iteration % smoothing] = ymove;
        x[iteration % smoothing] = xmove;

        iteration++;

        // determine the aggregates and implement sensitivity
        foreach (float xmov in x)
        {
            xaggregate += xmov;
        }

        xaggregate = xaggregate / smoothing * sensitivity;

        foreach (float ymov in y)
        {
            yaggregate += ymov;
        }

        yaggregate = yaggregate / smoothing * sensitivity;

        // turn the x start orientation to non-zero for clamp
        Vector3 newOrientation = transform.eulerAngles + new Vector3(-yaggregate, xaggregate, 0);


        float xclamp = Mathf.Clamp(newOrientation.x, Xlimit, 360 - Xlimit) % 360;

        // rotate the object based on axis input (note the negative y axis)
        transform.eulerAngles = newOrientation;
    }

}
