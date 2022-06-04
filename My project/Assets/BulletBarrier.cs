using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBarrier : MonoBehaviour
{



    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.BULLET_TAG))
        {
            //col.GetComponent<Bullet>().BulletReachedBarrier(true);
        }
    }
}
