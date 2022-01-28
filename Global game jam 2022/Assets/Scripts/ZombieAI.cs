using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    [SerializeField] private Transform target;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float minNoticeDistance;

    private void Update()
    {
        FacePlayer();
        if (Vector2.Distance(target.position, transform.position) < minNoticeDistance)
        {
            //If we're close enough to the player, we can start running towards him.
            transform.position = Vector2.MoveTowards(transform.position, target.position,
                movementSpeed * Time.deltaTime);
        }
    }

    public void onDeath()
    {
        GameObject Heart = Instantiate(heart, transform.position, heart.transform.rotation);
        Heart.GetComponent<Heart>().player = target;
        Destroy(gameObject);
    }

    void FacePlayer()
    {
        //Gets the vector between the player and figures out how it should flip in order to face the player
        var transform1 = transform;
        Vector3 delta = target.position - transform1.position;
        float scaleX = delta.x < 0f ? -10f : 10f;
        transform1.localScale = new Vector3(scaleX, 10f, 10f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathTrigger"))
        {
            onDeath();
        }
    }
}
