using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    [SerializeField] private float moveDistanceX = 0;
    [SerializeField] private float moveDistanceY = 0;
    [SerializeField] private float moveTime = 0;
    [SerializeField] private float waitTime = 0;

    private float timeCounter;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        targetPosition = new Vector3(startPosition.x + moveDistanceX, startPosition.y + moveDistanceY, startPosition.z);

        StartCoroutine(Move(waitTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            timeCounter += Time.deltaTime / moveTime;
            transform.position = Vector2.Lerp(startPosition, targetPosition, timeCounter);

            if (transform.localPosition == targetPosition)
            {
                moving = false;
                targetPosition = startPosition;
                startPosition = transform.localPosition;

                StartCoroutine(Move(waitTime));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObj = collision.gameObject;

        if (collisionObj.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObj = collision.gameObject;

        if (collisionObj.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }

    private IEnumerator Move(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        timeCounter = 0f;
        moving = true;
    }
}
