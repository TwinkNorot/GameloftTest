using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameManager gameManager;

    Bullet stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Bullet>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.canPlay)
        {
            transform.position += transform.forward * stats.projectileSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, stats.parentPosition) >= stats.range)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy") || other.tag.Equals("Ally"))
        {
            other.GetComponentInParent<Unit>().DamageTaken(gameObject);
            gameObject.SetActive(false);
        }
    }

}
