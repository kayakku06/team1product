using UnityEngine;

public class TargetController : MonoBehaviour
{
    [Header("出現範囲の設定")]
    // X軸（横）、Y軸（縦）、Z軸（奥行き）の範囲
    public Vector3 spawnArea = new Vector3(1.0f, 1.0f, 0f);

    [Header("中心位置（変更不要）")]
    private Vector3 initialPosition;

    void Start()
    {
        // ゲーム開始時の位置を「基準点」として覚える
        initialPosition = transform.position;
    }

    //何かがぶつかった時に呼ばれる関数
    void OnCollisionEnter(Collision collision)
    {
        // ぶつかった相手のタグが "Bullet" かどうか確認
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 1. 当たった弾を消す（貫通しないように）
            Destroy(collision.gameObject);

            // 2. 自分（的）を別の場所に移動させる
            Respawn();
            
            // ここに「スコア加算」や「効果音」の処理を追加できます
        }
    }

    void Respawn()
    {
        // ランダムな位置を計算する
        // Random.Range(最小値, 最大値) でランダムな数字を作る
        float randomX = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
        float randomY = Random.Range(-spawnArea.y / 2, spawnArea.y / 2);
        float randomZ = Random.Range(-spawnArea.z / 2, spawnArea.z / 2);

        // 基準点 + ランダムなズレ = 新しい位置
        transform.position = initialPosition + new Vector3(randomX, randomY, randomZ);
    }
}
