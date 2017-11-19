using UnityEngine;
using System.Collections;

public class StalkerBehavior : MonoBehaviour {

    public GameObject followTarget;
    public float stopDistance;
    public float stalkSpeed;
    public float targetViewDistance;
    Renderer renderer;
    public float floatHeight;//the height that the quad floats above the ground at all times

    void Start () {
        stopDistance = 1.0f;
        stalkSpeed = 0.0118f;
        targetViewDistance = 10f;
        floatHeight = transform.position.y;
        renderer = GetComponent<Renderer>();
    }
	
	void Update () {
        if(Vector3.Distance(transform.position, followTarget.transform.position) < stopDistance){
            triggerGameOver();
        }
        MoveTowardsTarget();
    }

    public void MoveTowardsTarget()
    {
        if (isInPlayerView())
            return;
        //calculate distance between self and target
        float distance = Vector3.Distance(transform.position, followTarget.transform.position);
        if(distance > stopDistance)
        {
            //move the stalker in X and Z
            Vector3 direction = followTarget.transform.position - transform.position;
            direction = Vector3.ClampMagnitude(direction * stalkSpeed, stalkSpeed);
            transform.Translate(direction, Space.World);
            transform.position = new Vector3(transform.position.x, floatHeight, transform.position.z);
        }
        else
        {
            triggerGameOver();
        }
    }

    public bool isInPlayerView()
    {
        if(Vector3.Distance(transform.position, followTarget.transform.position) > targetViewDistance)
            return false;//if too far, move regardless of visibility
        if (renderer.isVisible)
            return true;//if is visible, don't move
        return false;//otherwise, move
    }

    public void triggerGameOver()
    {
        //handle game over
        print("GAME OVER");
    }

}
