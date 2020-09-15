using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    int _rank = 1;

    public override void Use(Transform origin, Transform target)
    {
        if (target == null)
        {
            Debug.LogWarning("Fireball: No target! Cannot fire");
            return;
        }

        Debug.Log("Cast a rank " + _rank + " fireball on " + target.gameObject.name + " !");
    }
}
