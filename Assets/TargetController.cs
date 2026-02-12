using UnityEngine;

public class TargetController : MonoBehaviour
{
    [Header("的の設定")]
    public bool enableScore = true; // チェックを入れるとスコアが増える
    public bool enableWarp = true;  // ★追加：チェックを入れるとワープする

    [Header("出現範囲の設定")]
    public Vector3 spawnArea = new Vector3(1.0f, 1.0f, 0f);

    private Vector3 initialPosition;
    private bool isHit = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isHit) return;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            isHit = true;
            
            // 弾を消す
            Destroy(collision.gameObject);

            // スコア機能（ONの時だけ）
            if (enableScore)
            {
                if (ScoreManager.instance != null)
                {
                    ScoreManager.instance.AddHit();
                }
            }

            // ★変更点：ワープ機能（ONの時だけ）
            if (enableWarp)
            {
                Respawn();
            }

            // 処理が終わったのでフラグを戻す
            // ※ワープしない場合も、すぐに次の弾が当たらないように少し間隔を空ける
            Invoke("ResetHitFlag", 0.1f);
        }
    }

    void ResetHitFlag()
    {
        isHit = false;
    }

    void Respawn()
    {
        float randomX = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
        float randomY = Random.Range(-spawnArea.y / 2, spawnArea.y / 2);
        float randomZ = Random.Range(-spawnArea.z / 2, spawnArea.z / 2);

        transform.position = initialPosition + new Vector3(randomX, randomY, randomZ);

        MovingTarget mover = GetComponent<MovingTarget>();
        if (mover != null)
        {
            mover.ResetPosition();
        }
    }
}