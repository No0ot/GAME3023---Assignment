using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraScript : MonoBehaviour
{
    public List<GameObject> patrolPoints;
    public GameObject currentPoint;
    public GameObject targetPoint;
    public GameObject cameraObject;

    public float moveSpeed;
    private float journeyLength;
    private float startTime;

    int startInt = 0;
    int nextInt = 1;

    private void Awake()
    {
        patrolPoints = new List<GameObject>();
        for(int i = 0; i < transform.childCount - 1; i++)
        {
            patrolPoints.Add(transform.GetChild(i).gameObject);
        }
        startTime = Time.time;
        journeyLength = Vector3.Distance(currentPoint.transform.position, targetPoint.transform.position);
        currentPoint = patrolPoints[0];
        targetPoint = patrolPoints[1];
    }
    private void Start()
    {
        //patrolPoints = new List<GameObject>();
        startTime = Time.time;
        journeyLength = Vector3.Distance(currentPoint.transform.position, targetPoint.transform.position);
        currentPoint = patrolPoints[startInt];
        targetPoint = patrolPoints[nextInt];
    }

    void Update()
    {
        CheckDistance();
        Move();
    }

    private void Move()
    {
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distCovered / journeyLength;
        cameraObject.transform.position = Vector3.Lerp(currentPoint.transform.position, targetPoint.transform.position, fractionOfJourney);
    }
    /// <summary>
    /// Checks the distance to the target tile to see if it needs to update the target tile or if it reaches the end of the path takes a life away.
    /// </summary>
    private void CheckDistance()
    {
        float distance = Vector3.Distance(cameraObject.transform.position, targetPoint.transform.position);
        if (distance < 0.01f)
        {
            //reaches end
            if (targetPoint == patrolPoints[patrolPoints.Count - 1])
            {
                //fade to black
                cameraObject.transform.position = patrolPoints[startInt].transform.position;
                currentPoint = patrolPoints[startInt];
                targetPoint = patrolPoints[startInt + 1];
                nextInt = 1;
                startTime = Time.time;
                journeyLength = Vector3.Distance(currentPoint.transform.position, targetPoint.transform.position);
            }
            // set next patrol point
            else
            {
                currentPoint = targetPoint;
                nextInt += 1;
                targetPoint = patrolPoints[nextInt];
                startTime = Time.time;
                journeyLength = Vector3.Distance(currentPoint.transform.position, targetPoint.transform.position);
            }
        }
    }
}
