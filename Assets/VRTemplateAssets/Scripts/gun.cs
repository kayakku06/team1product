using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform muzzle;        // 発射口の位置
    public float bulletSpeed = 2000f; // 弾の速度

    void Update()
    {
        // マウス左クリック（0）が押された瞬間
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 弾を生成
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        
        // 弾のRigidbodyを取得して前方に力を加える
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(muzzle.forward * bulletSpeed);

        // 3秒後に弾を自動消去（メモリ対策）
        Destroy(bullet, 3f);
    }
}