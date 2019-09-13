using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowRover : MonoBehaviour
{
    public float CAMERA_MOVE_COOLDOWN = 5;
    public int CAMERA_MOVE_DISTANCE = 500;
    public float CAMERA_MOVE_SPEED = 300;

    Camera mainCamera;
    GameObject rover;
    IEnumerator coroutine;
    float cameraMoveCooldown;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rover = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraMoveCooldown <= 0)
        {
            Vector2 roverPoint = mainCamera.WorldToViewportPoint(rover.transform.position);
            // If the rover is more than 80% across the camera's view
            if (roverPoint.x > 0.8)
            {
                Vector3 cameraStartPosition = gameObject.transform.position;
                Vector3 cameraDestinationPosition = cameraStartPosition;
                cameraDestinationPosition.x += CAMERA_MOVE_DISTANCE;
                coroutine = MoveFromTo(gameObject.transform, cameraStartPosition, cameraDestinationPosition, CAMERA_MOVE_SPEED);
                StartCoroutine(coroutine);
                cameraMoveCooldown = CAMERA_MOVE_COOLDOWN;
            }
        } else
        {
            cameraMoveCooldown -= Time.deltaTime;
        }
    }

    IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
    }
}
