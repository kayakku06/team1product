using UnityEngine;

public class DynamicTarget : MonoBehaviour
{
    [Header("リスポーン設定")]
    [Tooltip("プレイヤーのカメラ（Main Camera）を指定します")]
    public Transform playerTransform;
    [Tooltip("プレイヤーから的が出現する最大半径（これ以上は離れないようにします）")]
    public float maxSpawnRadius = 10f;
    [Tooltip("プレイヤーに近すぎないための最小半径（これ以上は近づかないようにします）")]
    public float minSpawnRadius = 3f;
    [Tooltip("的が出現する高さの最小値（床からの高さ）")]
    public float minSpawnHeight = 1f;
    [Tooltip("的が出現する高さの最大値")]
    public float maxSpawnHeight = 3f;

    [Header("動きの設定")]
    public float moveSpeed = 2f;
    public float floatAmplitude = 0.5f; // 浮遊の上下の振れ幅
    public float floatFrequency = 2f;   // 浮遊のスピード

    // 動きのパターンを定義
    private enum MovementType { Linear, Floating, Follow }
    private MovementType currentMovement;
    
    private Vector3 startPosition;
    private Vector3 moveDirection;

    void Start()
    {
        Respawn();
    }

    void Update()
    {
        MoveTarget();
    }

    public void OnHit()
    {
        Respawn();
    }

    private void Respawn()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(minSpawnRadius, maxSpawnRadius);
        
        Vector3 newPos = playerTransform.position;
        newPos.x += randomCircle.x;
        newPos.z += randomCircle.y;
        newPos.y = Random.Range(minSpawnHeight, maxSpawnHeight);

        transform.position = newPos;
        startPosition = transform.position;

        currentMovement = (MovementType)Random.Range(0, 3);
        
        // 最初はランダムな水平方向へ
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        moveSpeed = Random.Range(1f, 3f);
    }

    private void MoveTarget()
    {
        // 🌟 プレイヤーとの現在の水平距離を計算（高さの違いは無視して計算します）
        Vector3 targetPosXZ = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPosXZ = new Vector3(playerTransform.position.x, 0, playerTransform.position.z);
        float distanceToPlayer = Vector3.Distance(targetPosXZ, playerPosXZ);

        switch (currentMovement)
        {
            case MovementType.Linear:
                transform.position += moveDirection * moveSpeed * Time.deltaTime;

                // 【問題1の解決】最大半径より遠ざかったら、プレイヤーの方向へUターンさせる
                if (distanceToPlayer > maxSpawnRadius)
                {
                    Vector3 directionToPlayer = (playerPosXZ - targetPosXZ).normalized;
                    moveDirection = directionToPlayer; // 進行方向をプレイヤーへ向ける
                }
                break;

            case MovementType.Floating:
                Vector3 nextPos = transform.position + (moveDirection * moveSpeed * 0.5f * Time.deltaTime);
                nextPos.y = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
                transform.position = nextPos;

                // 【問題1の解決】浮遊しながら遠ざかった場合も、プレイヤーの方向へUターンさせる
                if (distanceToPlayer > maxSpawnRadius)
                {
                    Vector3 directionToPlayer = (playerPosXZ - targetPosXZ).normalized;
                    moveDirection = directionToPlayer;
                }
                break;

            case MovementType.Follow:
                // 【問題2の解決】最小半径よりも遠い（離れている）時だけ近づくようにする
                if (distanceToPlayer > minSpawnRadius)
                {
                    Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
                    transform.position += directionToPlayer * (moveSpeed * 0.5f) * Time.deltaTime;
                    
                    // 追従中は上下にブレないように、startPositionの高さを更新しておく（浮遊と切り替わった時のため）
                    startPosition = transform.position; 
                }
                // minSpawnRadiusより近づいたら移動処理が行われないため、その場で止まって待機します。
                break;
        }
    }
}