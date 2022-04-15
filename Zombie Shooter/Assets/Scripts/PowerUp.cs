using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float delayMultiplier = 0.5f;
    [SerializeField] float cooldown = 10f;

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
            yield return new WaitForSeconds(cooldown);

            transform.position = new Vector3(Random.Range(10.0f, 40.0f), 0, Random.Range(10.0f, 40.0f));
            GetComponent<Collider>().enabled = true;
            GetComponent<Renderer>().enabled = true;
        }
    }
}
