using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpawner : MonoBehaviour
{
    [SerializeField] private GameObject deathBallPrefab = null;
    [SerializeField] private float ballSpeed = 2f;
    [SerializeField] private float followTime = 3f;
    private PlayerController player = null;
    private GameObject spawnedBall = null;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (player == null) throw new System.Exception("Player not found");

        SpawnBall();

        Debug.Log(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedBall != null)
        {
            spawnedBall.transform.position = Vector2.MoveTowards(spawnedBall.transform.position, player.transform.position, ballSpeed * Time.deltaTime);
        }
        else
        {
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        spawnedBall = Instantiate(deathBallPrefab, transform.position, Quaternion.identity);

        Destroy(spawnedBall, followTime);
    }
}
