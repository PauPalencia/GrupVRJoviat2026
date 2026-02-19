using UnityEngine;

public class FollowXYZ : MonoBehaviour
{
    public Transform target;

    void Start()
    {
    }

    void Update()
    {
        if (target == null) return;

        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            target.position.z
        );
        transform.rotation = new Quaternion(
            target.rotation.x,
            target.rotation.y,
            target.rotation.z,
            target.rotation.w
        );
    }
}