using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵の出現位置の種類
public enum RESPAWN_TYPE
{
    UP, // 上
    RIGHT, // 右
    //DOWN, // 下
    LEFT, // 左
    SIZEOF, // 敵の出現位置の数
}

// 敵を制御するコンポーネント
public class EnemyController : MonoBehaviour
{
    public Vector2 m_respawnPosInside; // 敵の出現位置（内側）
    public Vector2 m_respawnPosOutside; // 敵の出現位置（外側）
    public float m_speed; // 移動する速さ
    public int m_hpMax; // HP の最大値
    public int m_exp; // この敵を倒した時に獲得できる経験値
    public int m_damage; // この敵がプレイヤーに与えるダメージ

    private int m_hp; // HP
    private Vector3 m_direction; // 進行方向

    public GameObject animationObj;//爆破
    Score score;

    // 敵が生成された時に呼び出される関数
    private void Start()
    {
        // HP を初期化する
        m_hp = m_hpMax;
        //
        Destroy(gameObject,4f);

        score = GameObject.Find("GameManager").GetComponent<Score>();
    }

    // 毎フレーム呼び出される関数
    private void Update()
    {
        // まっすぐ移動する
        transform.localPosition += m_direction * m_speed;
        if (transform.position.y == -5.5f)
        {
            Destroy(gameObject);
        }
    }

    // 敵が出現する時に初期化する関数
    public void Init(RESPAWN_TYPE respawnType)
    {
        var pos = Vector3.zero;

        // 指定された出現位置の種類に応じて、
        // 出現位置と進行方向を決定する
        switch (respawnType)
        {
            // 出現位置が上の場合
            case RESPAWN_TYPE.UP:
                pos.x = Random.Range(
                    -m_respawnPosInside.x, m_respawnPosInside.x);
                pos.y = m_respawnPosOutside.y;
                m_direction = Vector2.down;
                break;

            // 出現位置が右の場合
            case RESPAWN_TYPE.RIGHT:
                pos.x = m_respawnPosOutside.x;
                pos.y = Random.Range(
                    -m_respawnPosInside.y, m_respawnPosInside.y);
                m_direction = Vector2.left;
                
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
                break;
        }

        // 位置を反映する
        transform.localPosition = pos;
    }

    // 他のオブジェクトと衝突した時に呼び出される関数
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // 弾と衝突した場合
        if (collision.CompareTag("Playerbullet"))
        {
            // 弾を削除する
            Destroy(collision.gameObject);

            // 敵の HP を減らす
            m_hp--;

            // 敵の HP がまだ残っている場合はここで処理を終える
            if (0 < m_hp) return;

            //アニメーション

            Instantiate(animationObj, transform.position, transform.rotation);

            // 敵を削除する
            Destroy(gameObject);
            Destroy(collision.gameObject);
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
