using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RESPAWN_TYPE2
{
    UP, // 上
    RIGHT, // 右
    //DOWN, // 下
    LEFT, // 左
    SIZEOF, // 敵の出現位置の数
}
public class SinusoidalMove : MonoBehaviour
{
    public Vector2 m_respawnPosInside; // 敵の出現位置（内側）
    public Vector2 m_respawnPosOutside; // 敵の出現位置（外側）
    private Vector3 m_direction; // 進行方向
    private int type;

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float frequency = 20f;

    [SerializeField]
    float magnitude = 0.5f;

    bool facingRight = true;

    Vector3 pos, localScale;

    public GameObject animationObj;//爆破

    Score score;
    
    // Use this for initialization
    void Start()
    {

        pos = transform.position;

        localScale = transform.localScale;

        Destroy(gameObject, 5f);

        score = GameObject.Find("GameManager").GetComponent<Score>();

    }

    // Update is called once per frame
    void Update()
    {

        CheckWhereToFace();
        if (type == 2)
        {
            if (facingRight)
                MoveRight();
            else
                MoveLeft();
        }
        else if (type == 1)
        {
            transform.localPosition += m_direction * moveSpeed / 100;
        }
    }

    void CheckWhereToFace()
    {
        if (pos.x < -7f)
            facingRight = false;

        else if (pos.x > 7f)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;

    }

    void MoveRight()
    {
        pos += m_direction * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Cos(Time.time * -frequency) * magnitude;
    }

    void MoveLeft()
    {
        pos -= m_direction * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Cos(Time.time * -frequency) * magnitude;
    }

    public void Init(RESPAWN_TYPE respawnType)
    {
        var pos = Vector3.zero;

        // 指定された出現位置の種類に応じて、
        // 出現位置と進行方向を決定する
        switch (respawnType)
        {
            // 出現位置が上の場合
            case RESPAWN_TYPE.UP:
                Destroy(gameObject);
                break;

            // 出現位置が右の場合
            case RESPAWN_TYPE.RIGHT:
                pos.x = m_respawnPosOutside.x;
                pos.y = Random.Range(
                    -m_respawnPosInside.y, m_respawnPosInside.y);
                m_direction = Vector2.left;
                type = 2;
                break;

            //// 出現位置が下の場合
            //case RESPAWN_TYPE.DOWN:
            //    Destroy(gameObject);
            //    break;

            // 出現位置が左の場合
            case RESPAWN_TYPE.LEFT:
                pos.x = -m_respawnPosOutside.x;
                pos.y = Random.Range(
                    -m_respawnPosInside.y, m_respawnPosInside.y);
                m_direction = Vector2.right;
                type = 2;
                break;
        }

        // 位置を反映する
        transform.localPosition = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // 弾と衝突した場合
        if (collision.CompareTag("Playerbullet"))
        {
            // 弾を削除する
            Destroy(collision.gameObject);


            //アニメーション

            Instantiate(animationObj, transform.position, transform.rotation);

            // 敵を削除する
            Destroy(gameObject);
            score.addpoint();
        }

        //player
        if (collision.CompareTag("Player"))
        {
            //アニメーション
            //敵
            Instantiate(animationObj, transform.position, transform.rotation);
            //player
            Instantiate(animationObj, collision.transform.position, transform.rotation);
            score.GameOver();
        }

        // 敵を削除する
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }
}