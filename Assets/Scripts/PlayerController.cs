using UnityEngine;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody playerRB;
    public float speed = 5.0f;
    private GameObject focalPoint;
    public bool hasPowerup;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    
    // something;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            
            Debug.Log("Collided with " + collision.gameObject.name + 
                " with powerup set to " + hasPowerup);

            enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine() {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

}
