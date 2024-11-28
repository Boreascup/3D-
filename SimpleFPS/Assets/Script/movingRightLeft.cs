using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingRightLeft : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveDistance = 5f;

    private Vector3 initPosition;
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTarget();
    }

    void MoveTarget()
    {
        Vector3 nextPos = transform.position + Vector3.right * (movingRight ? 1 : -1) * moveSpeed * Time.deltaTime;
        float distanceToInit = Vector3.Distance(nextPos, initPosition);
        if (distanceToInit > moveDistance)
        {
            movingRight = !movingRight;
        }

        transform.position = nextPos;
    }
}

