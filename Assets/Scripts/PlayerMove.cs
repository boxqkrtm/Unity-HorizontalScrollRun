using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator ani;
    public float speed = 5f;
    public int orbCount = 0;

    public float maxspeed = 10f;

    public GameObject playerOrb;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        InvokeRepeating("ShotOrb", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < maxspeed)
        {
            speed += speed * 0.03f * Time.deltaTime;
        }
        else
        {
            speed = maxspeed;
        }
        if (Input.GetKey(KeyCode.DownArrow) || (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width / 2.0f))
        {
            ani.SetBool("moveDown", true);
            transform.Translate(new Vector2(0, -5 * Time.deltaTime));
        }
        else
        {
            ani.SetBool("moveDown", false);
        }
        if (Input.GetKey(KeyCode.UpArrow) || (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2.0f))
        {
            ani.SetBool("moveUp", true);
            transform.Translate(new Vector2(0, 5 * Time.deltaTime));
        }
        else
        {
            ani.SetBool("moveUp", false);
        }

        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     ani.SetBool("moveLeft", true);
        //     transform.Translate(new Vector2(-speed * Time.deltaTime, 0f));
        //     gameObject.transform.localScale = new Vector2(1, 1);
        // }
        if (Input.GetKey(KeyCode.RightArrow) || true)
        {
            //반전
            ani.SetBool("moveLeft", true);
            gameObject.transform.localScale = new Vector2(-1, 1);
            transform.Translate(new Vector2(speed * Time.deltaTime, 0f));
        }
        // if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) == false)
        // {
        //     ani.SetBool("moveLeft", false);
        // }
    }

    void ShotOrb()
    {
        if (orbCount > 0)
        {
            Vector2 orbLoca = transform.position;
            orbLoca.x += 1f;
            Instantiate(playerOrb, orbLoca, Quaternion.identity);
            orbCount--;
        }
    }
}
