using UnityEngine;
using System.Collections;

public class PlayerFollower : MonoBehaviour
{
    public Transform target;
    [Range(0.1f, 1f)]
    public float Drag;

    void Update()
    {
        var newPos = Vector3.Lerp(transform.position, target.position, Drag);
        this.transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);        
    }
}
