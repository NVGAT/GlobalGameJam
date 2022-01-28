using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Components and assignables")]
    [SerializeField] private GameObject[] uiHearts;
    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CameraShake shake;

    [Header("Values")]
    public int health;

    private void Start()
    {
        shake = Camera.main.GetComponent<CameraShake>();
        movementScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (health > 3)
        {
            health = 3;
        }

        //Really bad code to determine which hearts we should show, couldn't get properties to work
        switch (health)
        {
            case 0:
                for (int i = 0; i < 2; i++)
                {
                    uiHearts[i].gameObject.SetActive(false);
                }
                uiManager.GameOver();
                gameObject.SetActive(false);
                break;
            case 1:
                uiHearts[0].gameObject.SetActive(true);
                uiHearts[1].gameObject.SetActive(false);
                uiHearts[2].gameObject.SetActive(false);
                break;
            case 2:
                uiHearts[0].gameObject.SetActive(true);
                uiHearts[1].gameObject.SetActive(true);
                uiHearts[2].gameObject.SetActive(false);
                break;
            case 3:
                uiHearts[0].gameObject.SetActive(true);
                uiHearts[1].gameObject.SetActive(true);
                uiHearts[2].gameObject.SetActive(true);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathTrigger"))
        {
            health = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MeleeEnemy"))
        {
            if (movementScript.isDashing)
            {
                //If we're dashing we pull an epic uno reverse card moment and destroy the enemy
                other.gameObject.GetComponent<ZombieAI>().onDeath();
            }
            else
            {
                //If we're not dashing we can't pull the epic reverse uno and we have to take damage :(
                health -= 1;
                StartCoroutine(shake.Shake(.15f, .4f));
            }
        }
    }
}
