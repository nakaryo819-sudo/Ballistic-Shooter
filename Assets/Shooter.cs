using UnityEngine;

public class Shooter : MonoBehaviour
{
    // ★追加：爆発の設計図を入れる変数
    public GameObject explosionPrefab;

    // Updateは毎フレーム呼ばれる（while(true)の中にいるイメージ）
    void Update()
    {
        // マウスの左クリック（0番）が押された瞬間だけ実行
        if (Input.GetMouseButtonDown(0))
        {
            FireRay();
        }
    }

    void FireRay()
    {
        // 1. レイ（光線）の作成
        // Camera.main はシーンにあるメインカメラ
        // ScreenPointToRay は「マウスの位置（2D）」を「3D空間の直線」に変換する関数
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 2. 衝突情報を入れる箱を用意（C++の参照渡しに近い）
        RaycastHit hit;

        // 3. レイを発射！ (もし何かに当たったら true を返す)
        // out hit は C++ でいう &hit (結果をここに書き込んでね、という意味)
        if (Physics.Raycast(ray, out hit))
        {

            // もし当たったのが "Sphere" なら消滅させる（メモリ解放的な処理）
            if (hit.collider.CompareTag("Target"))
            {
                GameManager.score += 100;

                // ★追加：当たった場所(hit.point)に、爆発(explosionPrefab)を作る
                // Quaternion.LookRotation(hit.normal) は「面に対して垂直な向き」にする計算ですが
                // 今はとりあえず Quaternion.identity (回転なし) でもOKです
                Instantiate(explosionPrefab, hit.point, Quaternion.identity);

                Debug.Log("現在のスコア: " + GameManager.score);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}