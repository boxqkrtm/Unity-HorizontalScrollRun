using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 playerUpPosition = player.transform.position;
            playerUpPosition.z = -10;
            playerUpPosition.y = transform.position.y;
            playerUpPosition.x += +4;
            gameObject.transform.position = playerUpPosition;
        }
    }
}
