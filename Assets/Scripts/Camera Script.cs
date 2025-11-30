using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  
    public float smoothSpeed = 5f;
    public float minY = -1f;  
    public float xOffset = -3f;   
    public float yOffset = 0f;

    void LateUpdate()
    {
        if (target == null) return;
        Debug.LogWarning("CameraFollow: No target assigned for the camera to follow.");
            
        Vector3 desiredPos = new Vector3(target.position.x + xOffset,target.position.y + yOffset,transform.position.z);

        
        if (desiredPos.y < minY)
            desiredPos.y = minY;

        
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        
    }
}
