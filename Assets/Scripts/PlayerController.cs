using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    int score = 0;
    public int health = 5;
    public GameObject teleporter1;
    public GameObject teleporter2;
    Transform player;


    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Game Over!");
            health = 5;
            score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void FixedUpdate()
    {
        float horizontaleInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontaleInput, 0, verticalInput) * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score += 1;
            Debug.Log("Score: " + score);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Trap"))
        {
            health -= 1;
            Debug.Log("Health: " + health);
        }
        if (other.CompareTag("Goal"))
        {
            Debug.Log("You win!");
        }
        if (other.CompareTag("Teleporter"))
        {
            StartCoroutine("Teleport");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Teleporter"))
        {
            StopAllCoroutines();
        }
    }

    public IEnumerator Teleport()
    {
        if (Mathf.Approximately(transform.position.x, teleporter1.transform.position.x))
        {
            yield return new WaitForSeconds(2f);
            transform.position = new Vector3(teleporter2.transform.position.x, 1.2f, teleporter2.transform.position.z);
            Debug.Log("entrée");
        }
        else
        {
            yield return new WaitForSeconds(2f);
            transform.position = new Vector3(teleporter1.transform.position.x, 1.2f, teleporter1.transform.position.z);

            Debug.Log("sortie");
        }

    }

}
