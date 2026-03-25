using UnityEngine;
using UnityEngine.InputSystem; 

public class SimpleGun : MonoBehaviour
{
    [Header("設定")]
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform spawnPoint;    // 銃口
    public float bulletSpeed = 20f; // 弾の速さ

    [Header("連射設定")]
    [Tooltip("弾と弾の発射間隔（秒）。小さいほど連射が速くなります")]
    public float fireRate = 0.1f; 
    private float nextFireTime = 0f; // 次に撃てる時間（内部計算用）

    [Header("入力設定")]
    // ここにコントローラーのボタン設定を登録します
    public InputActionProperty triggerAction;

    void Update()
    {
        // 変更点1：「押された瞬間」ではなく「押され続けているか」をチェック
        if (triggerAction.action != null && triggerAction.action.IsPressed())
        {
            // 変更点2：現在の時間が「次に撃てる時間」を過ぎているかチェック
            if (Time.time >= nextFireTime)
            {
                // 次に撃てる時間を更新（現在時刻 ＋ 発射間隔）
                nextFireTime = Time.time + fireRate;
                
                // 弾を撃つ
                Fire();
            }
        }
    }

    void Fire()
    {
        // 弾がセットされていなければ何もしない
        if (bulletPrefab == null || spawnPoint == null) return;

        // 1. 弾を生成
        GameObject spawnedBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

        // 2. 弾を飛ばす
        Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = spawnPoint.forward * bulletSpeed;
        }

        // 3. 3秒後に消す
        Destroy(spawnedBullet, 3.0f);
    }
}