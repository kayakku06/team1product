using UnityEngine;

public class SimpleGunManager : MonoBehaviour
{
    // 弾を発射する座標を入れる変数
    public Transform bulletSpawnTransform = null!;

    // 弾のプレハブを入れる変数
    public GameObject bulletPrefab = null!;

    // 弾の射出速度の変数
    public float m_bulletSpeed = 10.0f;

    // 弾の寿命時間の変数
    public float m_bulletLife = 5.0f;

    // 弾の重力の有無
    public bool m_bulletGravity;

    public void Fire()
    {
        // Instantiateで弾のPrefabを複製し、弾の射出座標に配置する
        var newBullet = Instantiate(bulletPrefab, bulletSpawnTransform);

        // 弾は銃から独立したオブジェクトであることを保証する
        newBullet.gameObject.transform.parent = null;

        // 弾の物理演算に干渉するため、弾のRigidbodyを呼び出す
        var rbBullet = newBullet.GetComponent<Rigidbody>();

        // 弾の重力設定に作用する
        rbBullet.useGravity = m_bulletGravity;

        // 弾を弾の正面方向（ローカル座標のZ軸の正、青い矢印）に向かって
        // AddForceのImpulseで射出する
        rbBullet.AddForce(newBullet.transform.forward * m_bulletSpeed, ForceMode.Impulse);

        // 弾が寿命を迎えたら消滅させる
        Destroy(newBullet, m_bulletLife);
    }
}