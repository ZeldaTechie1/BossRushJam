using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    public float Damage;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Health>().AffectHealth(null ,-Damage);
            Destroy(this.gameObject);
        }
    }
}
