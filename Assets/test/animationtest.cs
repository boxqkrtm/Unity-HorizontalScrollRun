using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationtest : MonoBehaviour
{
    // Start is called before the first frame update
    Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ani.SetBool("ismove", true);
        }
        else
        {
            ani.SetBool("ismove", false);
        }
    }
}
