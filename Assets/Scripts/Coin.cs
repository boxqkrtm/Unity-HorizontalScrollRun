using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            int amount = (int)Mathf.Round(col.gameObject.GetComponent<PlayerMove>().speed * 10f);
            gameManager.getCoin(amount);
            Destroy(gameObject);
        }
    }
}
