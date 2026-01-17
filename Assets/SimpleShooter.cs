using UnityEngine;

public class SimpleShooter : MonoBehaviour
{
    // �e�̃v���n�u�������ɃZ�b�g����
    public GameObject bulletPrefab;
    // ���˂��鑬�x
    public float speed = 20f;

    void Update()
    {
        // ���N���b�N�i0�j���ꂽ��
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // 1. �e�𐶐�����i���m�A�ꏊ�A�����j
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // 2. �e��Rigidbody�i�����G���W���j���擾����
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // 3. �O�����ɗ͂�������i���x �~ �O�����j
        // ���d�͗�����Unity������Ɍv�Z���Ă���܂��I
        rb.linearVelocity = transform.forward * speed;
    }
}