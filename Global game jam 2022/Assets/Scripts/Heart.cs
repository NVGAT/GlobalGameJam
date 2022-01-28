using System.Collections;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float movementSpeed;
    [SerializeField] private BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(enableColliderAfterTime(2));
    }

    private void Update()
    {
        if (collider.enabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().health++;
            Destroy(gameObject);
        }
    }

    IEnumerator enableColliderAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        collider.enabled = true;
    }
}
