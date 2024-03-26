using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;

    public float health = 10f;

    public int points = 1;
    public Enums.Path path { get; set; }

    public GameObject target { get; set; }
    private int pathIndex = 1;

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameManager.Instance.RemoveInGameEnemy();
            GameManager.Instance.AddCredits(points);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
        // check how close we are to the target 
        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            // if close, request a new waypoint 
            target = EnemySpawner.Get.RequestTarget(path, pathIndex);
            pathIndex++;

            // if target is null, we have reached the end of the path. 
            // Destroy the enemy at this point 
            if (target == null)
            {
                Destroy(gameObject);
                GameManager.Instance.RemoveInGameEnemy();
                if(path == Enums.Path.Path1)
                {
                    GameManager.Instance.AttackGate(Enums.Path.Path1);
                }
                else if (path == Enums.Path.Path2)
                {
                    GameManager.Instance.AttackGate(Enums.Path.Path2);
                }
            }
        }
    }

}
