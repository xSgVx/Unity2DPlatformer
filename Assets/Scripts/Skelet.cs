using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelet : Entity
{
    public Animator animator;

    private Sensor_HeroKnight m_groundSensor;
    private bool m_grounded = false;

    private float speed = 3.5f;
    private Vector3 dir;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_grounded = true;
        dir = transform.right;

        lives = 3;
    }

    public override void GetDamage()
    {
        lives -= 1;
        Debug.Log($"cur health = {lives}");

        animator.SetTrigger("TakeHit");

        if (lives <= 0)
        {            
            Die();
        }
    }

    public override void Die()
    {
        Debug.Log("enemy died");

        animator.SetBool("IsDead", true);
        m_grounded = true;

        //отключить после убийства
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
