using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float camVelocity = 0.025f;
    public Vector3 scrolling;

    private void LateUpdate()
    {
        Vector3 nextPosition = playerTransform.position + scrolling;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, nextPosition, camVelocity);

        transform.position = smoothPosition;
    }
}
