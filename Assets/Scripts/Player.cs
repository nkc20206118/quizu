using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;　　//移動スピード
    public GameObject prefab;　//球
    public Transform shotpoint; //球撃つ場所

     void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Move(); //移動

        Shot();//球発射
    }

    void Move()
    {
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y, 0) * Time.deltaTime * speed;
    }


    void Shot()
    {
        //弾
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // プレハブから生成
            Instantiate(prefab, shotpoint.position, Quaternion.identity);
        }
    }
}
