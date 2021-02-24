using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatDeathBall : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int direction = 1;
    PlayerController player;
    Vector2 respawn;
    Rigidbody2D ballRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (player == null) throw new System.Exception("Player not found");

        ballRigidbody = transform.gameObject.GetComponent<Rigidbody2D>();

        respawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ballRigidbody.velocity = new Vector2(direction * moveSpeed, ballRigidbody.velocity.y);

        //if (transform.position.y < -100) Destroy(gameObject);
    }

    public void RespawnBall()
    {
        ballRigidbody.velocity = Vector2.zero;
        transform.position = respawn;
    }

    public void RespawnAndGoLeft()
    {
        RespawnBall();
        direction = -1;
    }
}
