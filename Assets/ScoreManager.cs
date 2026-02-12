using UnityEngine;
using TMPro; // TextMeshProを使うために必要

public class ScoreManager : MonoBehaviour
{
    // どこからでもこのスクリプトを呼び出せるようにする（簡易的なシングルトン）
    public static ScoreManager instance;

    [Header("UIの設定")]
    [SerializeField] private TextMeshProUGUI scoreText; // 数字を表示するテキスト

    private int hitCount = 0; // 当たった数

    void Awake()
    {
        // 自分自身を登録
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    // 的に当たった時に呼ばれる関数
    public void AddHit()
    {
        hitCount++; // 数を増やす
        UpdateScoreText(); // 表示を更新
    }

    void UpdateScoreText()
    {
        // 画面に表示する文字を更新
        scoreText.text = "HIT: " + hitCount;
    }
}