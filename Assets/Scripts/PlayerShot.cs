using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;
    public float life = 3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        gameObject.transform.Translate(direction * Time.deltaTime);
        if (life < 0)
        {
            Destroy(gameObject);
        }
    }

    void SetDirection(Vector3 input)
    {
        direction = input;
    }
}
