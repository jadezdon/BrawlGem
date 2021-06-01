using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int damageValue = 10;

    Player player;
    float hurtDelay = 2f;
    bool isTouching;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouching)
        {
            hurtDelay -= Time.deltaTime;
            if (hurtDelay <= 0)
            {
                player.DamagePlayer(damageValue);
                hurtDelay = 2f;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
            other.gameObject.GetComponent<Player>().DamagePlayer(damageValue);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
            isTouching = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isTouching = false;
            hurtDelay = 2f;
        }
    }
}
