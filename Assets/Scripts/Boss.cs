
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    GameObject player;
    public GameObject killOrb;
    public GameObject wall;
    public GameObject coin;
    float bossY = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        InvokeRepeating("ShotOrb", 1.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 playerUpPosition = player.transform.position;
            playerUpPosition.z = 0;
            playerUpPosition.y = bossY;
            playerUpPosition.x += 8.5f;
            gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, playerUpPosition, 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerOrb")
        {
            Destroy(col.gameObject);
            for (int i = 0; i < 10; i++)
                Instantiate(coin, new Vector3(transform.position.x + Random.Range(0.1f, 1.0f), Random.Range(0.03f, -3.8f), 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void ShotOrb()
    {
        GameObject spawnObj = null;
        switch (Random.Range(0, 2))
        {
            case 0:
                spawnObj = killOrb;
                break;
            case 1:
                spawnObj = wall;
                break;
        }
        Vector2 spawnLoc = transform.position;
        spawnLoc.y = Random.Range(0.03f, -3.8f);
        spawnLoc.x = spawnLoc.x + 20;
        Instantiate(spawnObj, spawnLoc, Quaternion.identity);
        bossY = spawnLoc.y;
    }
}
