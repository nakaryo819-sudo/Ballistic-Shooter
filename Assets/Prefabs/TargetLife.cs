using UnityEngine;

public class TargetLife : MonoBehaviour
{
    void Start()
    {
        // ★修正：GameManagerの計算した「現在の寿命」をもらう
        float lifeTime = GameManager.currentLifeTime;

        // その時間後にTimeOut
        Invoke("TimeOut", lifeTime);
    }

    void TimeOut()
    {
        // ここが呼ばれた＝撃たれずに時間が来たということ

        // GameManagerを探して、GameOver関数を呼ぶ
        if (GameManager.isGameOver == false)
        {
            FindObjectOfType<GameManager>().GameOver();
        }

        // 自分は消える
        Destroy(gameObject);
    }
}