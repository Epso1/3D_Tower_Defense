using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public Vector3 direction = Vector3.forward;
    [SerializeField] private float speed = 16f;
    [SerializeField] private int damagePoints = 1;
    [SerializeField] GameObject hitGroundPrefab;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().ReceiveDamage(damagePoints);
            Destroy(gameObject);
        }
        if (other.CompareTag("Ground")) 
        { 
            Instantiate(hitGroundPrefab, transform.position, hitGroundPrefab.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
