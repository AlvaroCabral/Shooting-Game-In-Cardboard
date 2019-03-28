using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAndDamageScript : MonoBehaviour
{
    int life = 100;

    void damageHealth(int damage, AudioClip CollisionSound, AudioSource Sound)
    {
        life -= damage;
        Sound.PlayOneShot(CollisionSound);
    }

    private void StartParticleSystem(Collider collider, GameObject PreFab)
    {
        GameObject Blow = GameObject.Instantiate(PreFab, collider.transform.position, collider.transform.rotation.normalized) as GameObject;
        Destroy(Blow, 2);
    }

}
