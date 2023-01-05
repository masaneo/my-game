using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform leftBounds;
    public Transform rightBounds;
    public Transform background;

    public float SmoothDampTime = 0.1f;

    private Vector3 SmoothDampVelocity = Vector3.zero;
    private float camWidth, camHeight, levelMinX, levelMaxX;

    // Start is called before the first frame update
    void Start()
    {
        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;

        float leftBoundWidth = leftBounds.GetComponentInChildren<SpriteRenderer> ().bounds.size.x / 2;
        float rightBoundWidth = rightBounds.GetComponentInChildren<SpriteRenderer> ().bounds.size.x / 2;

        levelMinX = leftBounds.position.x + leftBoundWidth + (camWidth/2);
        levelMaxX = rightBounds.position.x - rightBoundWidth - (camWidth/2);
    }

    // Update is called once per frame
    void Update()
    {
        if (target) {
            float targetX = Mathf.Max(levelMinX, Mathf.Min(levelMaxX, target.position.x));
            float targetY = target.position.y;

            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref SmoothDampVelocity.x, SmoothDampTime);
            float y = Mathf.SmoothDamp(transform.position.y, targetY, ref SmoothDampVelocity.y, SmoothDampTime);

            transform.position = new Vector3(x, y, transform.position.z);
            background.position = new Vector3(x, y, 0);
        }
    }
}
