using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 minPosition;
    public Vector2 maxPostion;

    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPostion.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPostion.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
