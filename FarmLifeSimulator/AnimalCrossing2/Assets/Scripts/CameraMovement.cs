using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    public Transform targetToFollow;
    public float smoothTime = 1f;

    private void FixedUpdate()
    {
        Vector3 followVector = new Vector3(targetToFollow.position.x, targetToFollow.position.y + 3f, targetToFollow.position.z - 5f);
        transform.position = Vector3.Lerp(transform.position, followVector, Time.deltaTime * smoothTime); //or last args smoothTime
    }
}

