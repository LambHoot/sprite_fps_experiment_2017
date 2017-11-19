using UnityEngine;
using System.Collections;

public class RunnerBehavior : StalkerBehavior{
    /*
    The runner is the player's goal
    the runner is a character that runs between the stakler and player
    after collecting the runner, the player needs to escape
    */

    public GameObject stalkerTarget;
    public float mirrorFactor;//runner will always move to some position behind the stalker relative to the player

	void Start () {
        stalkSpeed = 0.008f;
        floatHeight = transform.position.y;
        stopDistance = 1.0f;
        mirrorFactor = 0.3f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, followTarget.transform.position) < stopDistance)
        {
            triggerCaughtRunner();
        }
        MoveTowardsTarget();
    }

    public new void MoveTowardsTarget()
    {
        //calculate distance between self and target
        float distance = Vector3.Distance(transform.position, followTarget.transform.position);
        if (distance > stopDistance)
        {
            //move the stalker to the point between player and stalker
            Vector3 vectorBetween = stalkerTarget.transform.position - followTarget.transform.position;
            Vector3 pointBetween = followTarget.transform.position + ((vectorBetween.magnitude/mirrorFactor) * Vector3.Normalize(vectorBetween));
            Vector3 direction = pointBetween - transform.position;
            direction = direction + Random.Range(-10f, 20f)*followTarget.transform.right;//cool effect, makes him mirror, but offset a bit to the right.
            direction = Vector3.ClampMagnitude(direction * stalkSpeed, stalkSpeed);
            transform.Translate(direction, Space.World);
            transform.position = new Vector3(transform.position.x, floatHeight, transform.position.z);
        }
        else
        {
            triggerCaughtRunner();
        }
    }

    public void triggerCaughtRunner()
    {
        //handle catching runner
        print("PICKED UP THE RUNNER!");
    }

}
