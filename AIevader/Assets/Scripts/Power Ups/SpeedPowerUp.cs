using System.Collections;
using System.Collections.Generic;
using Invector.CharacterController;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{

    [SerializeField] private float speedBoost;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            vThirdPersonController player = col.gameObject.GetComponent<vThirdPersonController>();
            player.freeWalkSpeed *= speedBoost;
            player.freeRunningSpeed *= speedBoost;
            player.freeSprintSpeed *= speedBoost;
            player.jumpForward *= speedBoost;
            player.isSpeedBoosted = true;
            Destroy(gameObject);
        }
    }
}
