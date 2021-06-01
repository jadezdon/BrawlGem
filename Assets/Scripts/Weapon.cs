using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int damageValue = 10;
    [SerializeField] float moveSpeed = 20f;

    Vector3 shootDirection;
    ObjectPooler pooler;

    public void Setup(Vector3 shootDir)
    {
        shootDirection = shootDir;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        transform.Rotate(0f, 0f, angle);
    }

    private void Start()
    {
        pooler = ObjectPooler.current;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += shootDirection * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(pooler.prefab.GetComponent<Collider2D>(), other.collider);
        }
        else
        {
            if (other.collider.CompareTag("Enemy"))
                other.gameObject.GetComponent<Enemy>().DamageEnemy(damageValue);
            gameObject.SetActive(false);
        }
    }
}
