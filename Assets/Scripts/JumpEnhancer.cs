using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnhancer : MonoBehaviour
{
    [SerializeField] private Tower _tower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Jumper jumper))
        {
            jumper.ChangeJumpForce(_tower.HumansInTowerCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Jumper jumper))
        {
            jumper.ChangeJumpForce(1/ _tower.HumansInTowerCount);
        }
    }
}
