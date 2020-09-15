using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AbilityLoadout _abilityLoadout;
    [SerializeField] Ability _startingAbility;
    [SerializeField] Ability _newAbilityToTest;

    public Transform CurrentTarget { get; private set; }

    private void Awake()
    {
        if(_startingAbility != null)
        {
            _abilityLoadout?.EquipAbility(_startingAbility);
        }
    }

    //TODO consider breaking into separate script
    public void SetTarget(Transform newTarget)
    {
        CurrentTarget = newTarget;
    }

    private void Update()
    {
        //TODO in reality, Inputs would be detected elsewhere,
        // and passed into the Player class. We're doing it here
        // for simplification of example
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _abilityLoadout.UseEquippedAbility(CurrentTarget);
        }
        //equip new weapon
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _abilityLoadout.EquipAbility(_newAbilityToTest);
        }
        //set target for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //target ourselves in this case
            SetTarget(transform);
        }
    }
}
