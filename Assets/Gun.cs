using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleGunController : MonoBehaviour
{
    [Header("設定")]
    public GameObject bulletPrefab;
    public Transform muzzlePoint;
    public float bulletSpeed = 20f;

    [Header("入力設定")]
    public InputActionProperty triggerAction;

    private bool isFiring = false; // 連射防止用

    void Update()
    {
        // トリガーの押し込み具合（0.0 ～ 1.0）を取得
        float triggerValue = triggerAction.action.ReadValue<float>();

        // トリガーを半分以上（0.5）引いたら発射
        if (triggerValue > 0.5f)
        {
            if (!isFiring) // 押しっぱなしで連射されないようにする
            {
                Fire();
                isFiring = true;
            }
        }
        else
        {
            // トリガーを戻したら、また撃てるようにリセット
            isFiring = false;
        }
    }

    void Fire()
    {
        // ログを出して、コードが動いているか確認（Console画面に出ます）
        Debug.Log("バン！発射しました！");

        if (bulletPrefab == null || muzzlePoint == null)
        {
            Debug.LogError("プレハブか発射位置（Muzzle）がセットされていません！");
            return;
        }

        // 弾を生成
        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        
        // 弾に物理挙動があるか確認して速度を与える
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 物理的な力を加える
            rb.AddForce(muzzlePoint.forward * bulletSpeed, ForceMode.VelocityChange);
        }
        
        // 3秒後に消す
        Destroy(bullet, 3.0f);
    }
}