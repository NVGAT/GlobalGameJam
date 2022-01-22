using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        //Smooth movement towards a target, in this case from the camera to the player
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
        transform.position = smoothedPosition;
    }
}
