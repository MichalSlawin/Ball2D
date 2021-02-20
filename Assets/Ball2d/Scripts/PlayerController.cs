using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 500f;
    private Vector2 target;
    private bool isOnPlatform = false;
    private Vector2 respawn;
    private float fellOffPoint = -20f;
    private float fellOffPointUp = 50f;
    private int points = 0;
    private float moveClickOffset = 1f;
    Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        respawn = transform.position;
        playerRigidbody = transform.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleClick();

        if(transform.position.y < fellOffPoint || transform.position.y > fellOffPointUp)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.gravityScale = 1;
        transform.position = respawn;
        points--;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject collisionObj = collision.gameObject;

        if (collisionObj.CompareTag("Platform"))
        {
            isOnPlatform = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObj = collision.gameObject;

        if (collisionObj.CompareTag("Finish"))
        {
            Debug.Log("score: " + points);
        }

        if (collisionObj.CompareTag("GravityChange"))
        {
            playerRigidbody.gravityScale *= -1;
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

        if (collisionObj.CompareTag("Death"))
        {
            HandleDeath();
        }

        if (collisionObj.CompareTag("Respawn"))
        {
            respawn = transform.position;
        }
    }

    private void HandleClick()
    {
        if (Input.GetMouseButton(0) && GetClickedObjTag() != "Player")
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Transform parent = transform.parent;
            if (parent != null)
            {
                transform.parent = null;
            }

            float direction = 0;
            if (target.x > transform.position.x + moveClickOffset) direction = 1;
            else if (target.x < transform.position.x - moveClickOffset) direction = -1;

            transform.parent = parent;

            playerRigidbody.velocity = new Vector2(direction * moveSpeed, playerRigidbody.velocity.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (GetClickedObjTag() == "Player" && isOnPlatform)
            {
                float direction = 1;
                if (playerRigidbody.gravityScale < 0) direction = -1;
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce * direction);
            }
        }
    }

    private string GetClickedObjTag()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(target, Vector2.zero);

        if (hit.collider != null)
        {
            return hit.collider.gameObject.tag;
        }

        return "";
    }
}
