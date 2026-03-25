using UnityEngine;

public class BulletHit : MonoBehaviour
{
    // 何かにぶつかった瞬間に呼ばれるメソッド
    private void OnCollisionEnter(Collision collision)
    {
        // ぶつかった相手（collision.gameObject）が DynamicTarget を持っているか確認
        DynamicTarget target = collision.gameObject.GetComponent<DynamicTarget>();
        
        // もし持っていたら（＝的だったら）
        if (target != null)
        {
            // 的に「当たったよ」と伝えて再配置させる
            target.OnHit();
        }

        // 的に当たっても、壁に当たっても、ぶつかったらこの銃弾自体は消滅させる
        Destroy(gameObject);
    }
}