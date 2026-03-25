using UnityEngine;

public class FloatingMove : MonoBehaviour
{
    [Header("上下動の設定")]
    // 上下動の速さ（周波数）
    public float verticalSpeed = 2f; 
    // 上下動の幅（振幅）
    public float verticalAmplitude = 0.5f; 

    [Header("横揺れの設定 (0にすると無効)")]
    // 横揺れの速さ
    public float horizontalSpeed = 1f;
    // 横揺れの幅
    public float horizontalAmplitude = 0.2f;

    [Header("回転の設定")]
    // 1秒間の回転角度（Vector3で各軸の速さを指定）
    public Vector3 rotationSpeed = new Vector3(0, 30, 0); // 初期値はY軸に毎秒30度

    // スポーン時の初期位置
    private Vector3 initialPosition;
    // ランダムな位相オフセット（全ての的が同じ動きをしないようにするため）
    private float randomOffset;

    void Start()
    {
        // ゲーム開始時（またはスポーン時）の位置を記憶
        initialPosition = transform.position;
        // 0〜2πの範囲でランダムな値を生成
        randomOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        // --- 1. 位置の計算 ---
        Vector3 currentPos = initialPosition;

        // Time.time（ゲーム開始からの経過時間）にオフセットを足して、動きをずらす
        float timeWithOffset = Time.time + randomOffset;

        // 上下動 (Y軸) : Sin関数を使って波を作る
        // Math.Sinは -1 〜 1 の値を返す
        float newY = Mathf.Sin(timeWithOffset * verticalSpeed) * verticalAmplitude;
        currentPos.y += newY;

        // 横揺れ (X軸) : こちらはCos関数を使うと、Y軸とずれて面白い動きになる
        if (horizontalAmplitude > 0)
        {
            float newX = Mathf.Cos(timeWithOffset * horizontalSpeed) * horizontalAmplitude;
            currentPos.x += newX;
        }

        // 計算した位置を適用
        transform.position = currentPos;

        // --- 2. 回転の計算 ---
        // 毎フレーム、指定した速さで回転させる
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
