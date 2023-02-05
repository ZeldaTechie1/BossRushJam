using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffin : Projectile
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void HandleCollision(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public override void Launch(Vector3 target)
    {
        throw new System.NotImplementedException();
    }
}
