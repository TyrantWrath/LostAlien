using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBulletType
{
    Normal,
    Spread,
    SlowSpread
}
public class EnemyShooterController : MonoBehaviour
{
    [SerializeField] GameObject enemyBullet;
    [SerializeField] private int numberOfBullet = 4;
    [SerializeField] private EnemyBulletType bulletType = EnemyBulletType.Spread;
    [SerializeField] private float bulletSpeed = 1.3f;

    public void ShootOnRandom(Vector3 direction, Vector3 origin)
    {
        int randomShoot = Random.Range(0, 3);

        if (randomShoot == 0)
        {
            SpreadShot(direction, origin, false);
        }

        else if (randomShoot == 1)
        {
            SpreadShot(direction, origin, true);
        }

        else if (randomShoot == 2)
        {
            NormalShoot(direction, origin);
        }

    }
    public void Shoot(Vector3 direction, Vector3 origin)
    {
        if (bulletType == EnemyBulletType.Spread)
        {
            SpreadShot(direction, origin, false);
        }

        if (bulletType == EnemyBulletType.SlowSpread)
        {
            SpreadShot(direction, origin, true);
        }

        if (bulletType == EnemyBulletType.Normal)
        {
            NormalShoot(direction, origin);
        }
    }

    private void SpreadShot(Vector3 direction, Vector3 origin, bool isBulletSlow)
    {
        float offset = 0.5f;

        for (int i = 0; i < numberOfBullet; i++)
        {
            Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            GameObject bullet = Instantiate(enemyBullet, origin, rot);

            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed / 2.5f;
            bullet.GetComponent<EnemyBullet>().SetIsSlow(isBulletSlow);

            direction.x += Random.Range(-offset, offset);
            direction.y += Random.Range(-offset, offset);
        }
    }
    private void NormalShoot(Vector3 direction, Vector3 origin)
    {
        float offset = 0.5f;

        for (int i = 0; i < numberOfBullet; i++)
        {
            Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            GameObject bullet = Instantiate(enemyBullet, origin, rot);

            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            direction.x += Random.Range(-offset, offset);
            direction.y += Random.Range(-offset, offset);
        }
    }
}
