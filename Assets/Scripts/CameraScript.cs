using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] protected Vector3 velocity = Vector3.zero;
    public Transform player;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Start(){
        offset = new Vector3 (0,0,-80);
    }

    void FixedUpdate(){
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
        //Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed); //alt solution
        transform.position = smoothedPosition;
    }
}
