using UnityEngine;
using UnityEngine.InputSystem; // これが重要！

public class SimpleGun : MonoBehaviour
{
    [Header("設定")]
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform spawnPoint;    // 銃口
    public float bulletSpeed = 20f; // 弾の速さ

    [Header("入力設定")]
    // ここにコントローラーのボタン設定を登録します
    public InputActionProperty triggerAction;

    void Update()
    {
        // 毎フレーム「トリガーが押されたか？」をチェック
        // (actionが設定されていて、かつ、そのフレームで押された瞬間なら実行)
        if (triggerAction.action != null && triggerAction.action.WasPressedThisFrame())
        {
            Fire();
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