using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] private float delayMultiplier = 0.5f;

    public float DelayMultiplier => delayMultiplier; 

    private void OnTriggerEnter(Collider other)
    {
        var playerWeapon = other.GetComponent<PlayerWeapon>();
        if (playerWeapon)
        {
            playerWeapon.AddPowerUp(this);
            StartCoroutine(DisableAfterDelay(playerWeapon));
            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }

        IEnumerator DisableAfterDelay(PlayerWeapon playerWeapon)
        {
            yield return new WaitForSeconds(duration);
            playerWeapon.RemovePowerUp(this);
        }
    }
}
