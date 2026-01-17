using UnityEngine;
using TMPro; // ★追加！これでTextMeshProが使えるようになる
using UnityEngine.SceneManagement; // ★追加！これがないとシーン操作できません

public class GameManager : MonoBehaviour
{
    // ここにさっき作った「的のプレハブ」をセットする
    public GameObject targetPrefab;
    public TextMeshProUGUI scoreText; // ★追加！UIを入れる箱
    public GameObject gameOverUI; // ★追加：GAME OVERの文字を入れる箱

    // staticをつけることで、どこからでも "GameManager.score" でアクセスできる共有変数になる
    // C++の static メンバ変数と同じ概念です
    public static int score = 0;
    public static bool isGameOver = false; // ★追加：ゲームが終わったかどうかのフラグ

    // ★追加：現在の「的の寿命」を管理する変数（最初は1.0秒）
    public static float currentLifeTime = 1.0f;

    void Start()
    {
        score = 0; // ゲーム開始時にリセット
        isGameOver = false; //リセット
        currentLifeTime = 1.0f; //リセット
        UpdateScoreText(); // 最初の0点を表示


        // ★修正：「一定時間」ではなく「再帰的」に呼ぶように変更
        // InvokeRepeatingだと間隔を変えられないので、新しいやり方に変えます
        Invoke("SpawnTarget", 1.0f);
    }

    void Update()
    {
        // ★毎フレーム、画面の文字を現在のスコアに書き換える
        // (本来はスコア加算時だけ呼ぶのが軽量ですが、今は簡単さ優先でUpdateに書きます)
        UpdateScoreText();
    }

    // ★追加：ゲームオーバーになったら呼ばれる関数
    public void GameOver()
    {
        // すでに終わってたら何もしない（多重呼び出し防止）
        if (isGameOver) return;

        isGameOver = true;

        // 敵が出るのを止める
        CancelInvoke("SpawnTarget");

        // GAME OVERの文字を表示する（チェックボックスをONにするのと同じ）
        gameOverUI.SetActive(true);

        Debug.Log("ゲームオーバー！");
    }

    void SpawnTarget()
    {
        if (isGameOver) return;

        // ランダムな座標を決める（X: -5〜5, Y: 1〜4, Z: 0）
        // 機械系ならわかると思いますが、3次元ベクトルを作っています
        // 1. 的を出す
        float x = Random.Range(-5.0f, 5.0f);
        float y = Random.Range(1.0f, 4.0f);
        Vector3 randomPos = new Vector3(x, y, 0);

        // プレハブを、その座標に実体化（インスタンス化）する
        Instantiate(targetPrefab, randomPos, Quaternion.identity);

        // 2. 次の難易度を計算（ここがアルゴリズム！）
        // スコア1000点ごとに、寿命が0.1秒ずつ短くなる（最短0.3秒）
        // Math.Maxで「0.3秒以下にはならない」ように制限
        currentLifeTime = Mathf.Max(0.3f, 1.0f - (score / 1000) * 0.1f);

        // 3. 次のスポーンを予約（難易度が上がるとスポーン間隔も短くする）
        float nextSpawnDelay = currentLifeTime;
        Invoke("SpawnTarget", nextSpawnDelay);
    }

    // ★追加：ボタンが押されたら実行する関数
    public void Retry()
    {
        // 現在のシーンの名前を取得して、読み込み直す（＝リセット）
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ★スコア更新用の便利関数
    void UpdateScoreText()
    {
        if (scoreText != null) // エラー防止
        {
            scoreText.text = "Score: " + score;
        }
    }


}