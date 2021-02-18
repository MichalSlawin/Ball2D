using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 500f;
    private Vector2 target;
    private bool isOnPlatform = false;
    private Vector2 respawn;
    private float fellOffPoint = -10f;
    private int points = 0;
    Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        respawn = transform.position;
        playerRigidbody = transform.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleClick();

        if(transform.position.y < fellOffPoint)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        transform.position = respawn;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObj = collision.gameObject;

        if (collisionObj.CompareTag("Platform"))
        {
            isOnPlatform = true;
            Debug.Log("platform enter");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObj = collision.gameObject;

        if (collisionObj.CompareTag("Platform"))
        {
            isOnPlatform = false;
            Debug.Log("platform exit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObj = collision.gameObject;

        if (collisionObj.CompareTag("Point"))
        {
            Destroy(collisionObj);
            points++;
        }
    }

    private void HandleClick()
    {
        if (Input.GetMouseButton(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, transform.position.y), moveSpeed * Time.deltaTime);
        }

        if(Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(target, Vector2.zero);
            GameObject selectedObject = null;
            if(hit.collider != null)
            {
                selectedObject = hit.collider.gameObject;
            }
            
            if (selectedObject != null && selectedObject.CompareTag("Player") && isOnPlatform)
            {
                playerRigidbody.AddForce(new Vector2(0, jumpForce));
            }
        }
    }
}
