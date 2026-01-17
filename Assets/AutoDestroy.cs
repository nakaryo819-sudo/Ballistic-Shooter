using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // パーティクルの再生が終わったら消したいので、少し長めの1秒後に設定
    void Start()
    {
        Destroy(gameObject, 1.0f);
    }
}