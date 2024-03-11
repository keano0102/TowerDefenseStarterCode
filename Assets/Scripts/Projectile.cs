using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;

    // Start is called before the first frame update 
    void Start()
    {
        if (target != null)
        {
            // Rotate the projectile towards the target
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Debug.LogWarning("No target assigned to projectile.");
        }
    }

    // Update is called once per frame 
    void Update()
    {
        if (target == null)
        {
            // If target is null, destroy this object
            Destroy(gameObject);
            return;
        }

        // Move the projectile towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Check if the distance between this object and the target is smaller than 0.2
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            // If so, destroy this object
            Destroy(gameObject);
        }
    }
}

