using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TripleShot : ShotAbility
{
    public GameObject Bullet;

    public override void Activate()
    {
        Instantiate(Bullet, shooting.transform.position + 3 * shooting.transform.forward, shooting.transform.rotation);
        Instantiate(Bullet, shooting.transform.position + 2 * shooting.transform.forward, shooting.transform.rotation);
    }
}
