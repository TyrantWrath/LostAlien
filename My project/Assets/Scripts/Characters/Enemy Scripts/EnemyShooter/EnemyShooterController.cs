using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBulletType
{
    SingleShot,
    Normal,
    Spread,
    SlowSpread,
    BurstShot,
    ShotGunBlast,
    homingMissileShot,
}
public class EnemyShooterController : MonoBehaviour
{
    [SerializeField] GameObject enemyBullet;
    [SerializeField] GameObject enemyMissile;
    [SerializeField] private int numberOfBullet = 4;
    [SerializeField] private int numberOfShotGunShots = 8;
    [SerializeField] private bool isHomingMissile = false;
    [SerializeField] private Hashtable enumValues = new Hashtable();

    [Space(20)]

    [Header("Select Shooting Pattern")]
    [SerializeField] private EnemyBulletType bulletType = EnemyBulletType.Spread;
    [SerializeField] private string[] bulletYUPE;
    [SerializeField] private float bulletSpeed = 1.3f;
    private void Start()
    {
        enumValues.Add(1, EnemyBulletType.SingleShot);
        enumValues.Add(2, EnemyBulletType.Normal);
        enumValues.Add(3, EnemyBulletType.Spread);
        enumValues.Add(4, EnemyBulletType.SlowSpread);
        enumValues.Add(5, EnemyBulletType.BurstShot);
        enumValues.Add(6, EnemyBulletType.ShotGunBlast);
        enumValues.Add(7, EnemyBulletType.homingMissileShot);

    }


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
        switch (bulletType)
        {
            case EnemyBulletType.SingleShot:
                SingleShot(direction, origin);
                break;

            case EnemyBulletType.SlowSpread:
                SpreadShot(direction, origin, true);
                break;

            case EnemyBulletType.Spread:
                SpreadShot(direction, origin, true);
                break;

            case EnemyBulletType.Normal:
                NormalShoot(direction, origin);
                break;

            case EnemyBulletType.BurstShot:
                BurstShot(direction, origin);
                break;

            case EnemyBulletType.ShotGunBlast:
                ShotGunShot(direction, origin);
                break;

            case EnemyBulletType.homingMissileShot:
                HomingMissileShot(direction, origin);
                break;
        }
    }

    private void SpreadShot(Vector3 direction, Vector3 origin, bool isBulletSlow)
    {
        float offset = 0.5f;

        for (int i = 0; i < numberOfShotGunShots; i++)
        {
            Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            GameObject bullet = Instantiate(enemyBullet, origin, rot);

            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed / 2.5f;
            //bullet.GetComponent<EnemyBullet>().SetIsSlow(isBulletSlow);

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

    private void BurstShot(Vector3 direction, Vector3 origin)
    {
        Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        StartCoroutine(DelayBeforeTheNextShot(direction, origin, rot));
    }

    IEnumerator DelayBeforeTheNextShot(Vector3 direction, Vector3 origin, Quaternion rot)
    {
        GameObject bullet = Instantiate(enemyBullet, origin, rot);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        yield return new WaitForSeconds(0.1f);
        GameObject bullet1 = Instantiate(enemyBullet, origin, rot);
        bullet1.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        yield return new WaitForSeconds(0.1f);
        GameObject bullet2 = Instantiate(enemyBullet, origin, rot);
        bullet2.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

    }

    private void ShotGunShot(Vector3 direction, Vector3 origin)
    {
        float offset = 0.2f;
        float homingOffset = 360;

        if (!isHomingMissile)
        {
            for (int i = 0; i < numberOfShotGunShots; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                GameObject bullet = Instantiate(enemyBullet, origin, rot);

                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                direction.x += Random.Range(-offset, offset);
                direction.y += Random.Range(-offset, offset);
            }
        }

        else if (isHomingMissile)
        {
            for (int i = 0; i < numberOfShotGunShots; i++)
            {
                Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x)
                * Mathf.Rad2Deg * Random.Range(-homingOffset, homingOffset));

                GameObject bullet = Instantiate(enemyMissile, origin, rot);
            }
        }

    }

    private void SingleShot(Vector3 direction, Vector3 origin)
    {
        Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        GameObject bullet = Instantiate(enemyBullet, origin, rot);

        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    private void HomingMissileShot(Vector3 direction, Vector3 origin)
    {
        float offset = 360;
        Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x)
        * Mathf.Rad2Deg * Random.Range(-offset, offset));

        GameObject bullet = Instantiate(enemyMissile, origin, rot);

    }

}
