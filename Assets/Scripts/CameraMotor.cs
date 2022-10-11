using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt;
    private Vector3 startOffSet;
    private Vector3 moveVector;

    private float transition = 0.0f;
    //Time in seconds for start animation (camera swoop)
    private float animationDuration = 3.0f;
    private Vector3 animationOffSet = new Vector3(0, 5, 5);

    // Start is called before the first frame update
    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffSet = transform.position - lookAt.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = lookAt.position + startOffSet;

        // X
        moveVector.x = 0;
        // Y
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);

        if (transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            // Animation for game start
            transform.position = Vector3.Lerp(moveVector + animationOffSet, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }

    }
}
