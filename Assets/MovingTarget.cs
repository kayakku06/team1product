using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [Header("動きの設定")]
    [SerializeField] private float speed = 2.0f;  // 動く速さ
    [SerializeField] private float width = 3.0f;  // 左右に動く振れ幅（メートル）

    private Vector3 initialPosition;

    void Start()
    {
        // ゲーム開始時の位置を基準点として記録
        initialPosition = transform.position;
    }

    void Update()
    {
        // サイン波を使って左右（X軸）のオフセットを計算
        // Time.time * speed で時間の経過とともに値を変化させる
        float xOffset = Mathf.Sin(Time.time * speed) * width;

        // 現在の位置を更新（初期位置 + 計算したオフセット）
        // Y軸とZ軸はそのまま維持
        transform.position = new Vector3(initialPosition.x + xOffset, transform.position.y, transform.position.z);
    }

    // もし既存の処理で「的がワープ（位置リセット）」する場合、
    // ワープ後にこの関数を呼ぶことで、移動の基準点を更新できます
    public void ResetPosition()
    {
        initialPosition = transform.position;
    }
}