using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

    Vector2 shootingDirection;

    // Update is called once per frame
    void Update()
    {
        shootingDirection = new Vector2(playerAnimator.GetFloat("LastHorizontal"), playerAnimator.GetFloat("LastVertical"));
        shootingDirection.Normalize();
        if (Input.GetButtonDown("Fire1"))
        {
            playerAnimator.SetTrigger("Attack");
        }
    }

    // used by player animation event
    void Shoot()
    {
        GameObject weapon = ObjectPooler.current.GetPooledObject();
        if (weapon == null) return;

        weapon.transform.position = firePoint.position;
        weapon.transform.rotation = Quaternion.identity;
        weapon.SetActive(true);
        weapon.GetComponent<Weapon>().Setup(shootingDirection);

        weapon.GetComponent<AudioSource>().Play();
    }
}
