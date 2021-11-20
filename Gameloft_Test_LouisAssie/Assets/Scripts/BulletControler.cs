using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{

    Bullet stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * stats.projectileSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, stats.parentPosition) >= stats.range)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            other.GetComponentInParent<Enemy>().DamageTaken(stats);
            gameObject.SetActive(false);
        }
    }

}
