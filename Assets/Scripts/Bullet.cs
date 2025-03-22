using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 direction = Vector3.forward;
    [SerializeField] private float speed = 16f;
    [SerializeField] private int damagePoints = 1;
    [SerializeField] GameObject hitGroundPrefab;
    public Transform target;

    private void Start()
    {
        if (target != null)
        {
            // Calcula la dirección desde la flecha hasta el target.
            direction = (target.position - transform.position).normalized;
            // Aplica LookRotation y ajusta con un offset.
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0);
        }
    }
    void Update()
    {
        // Mueve la flecha en la dirección ya calculada.
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
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

}
