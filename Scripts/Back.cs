using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    public float speed;
    public float posmax;
    public float posmin;

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime);

        if (transform.position.y <= posmax)
        {
            transform.position = new Vector3(0, posmin);
        }
    }
}
