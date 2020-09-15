using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : Ability
{
    int _healAmount = 25;

    public override void Use(Transform origin, Transform target)
    {
        Debug.Log("Cast Cure on " + target.gameObject.name);
    }
}
