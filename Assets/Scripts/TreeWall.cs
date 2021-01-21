using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().hitTree();
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "PlayerOrb")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
