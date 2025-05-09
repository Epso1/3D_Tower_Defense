using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    Vector3 direction = Vector3.forward;
    [SerializeField] private float speed = 16f;
    [SerializeField] private int damagePoints = 1;
    [SerializeField] GameObject hitGroundPrefab;
    [HideInInspector] public Transform target;

    private void Start()
    {
        if (target != null)
        {
            // Calcula la direcci�n desde la flecha hasta el target.
            direction = (target.position - transform.position).normalized;
            // Aplica LookRotation y ajusta con un offset.
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0);
        }
    }
    void Update()
    {
        // Mueve la flecha en la direcci�n ya calculada.
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 hitPosition = Vector3.Lerp(other.transform.position + new Vector3(0, 0.1f, 0), transform.position, 0.2f);
            Instantiate(hitGroundPrefab, hitPosition, transform.rotation, other.transform);
            other.GetComponent<Enemy>().ReceiveDamage(damagePoints);            
            Destroy(gameObject);
        }
        if (other.CompareTag("Ground")) 
        { 
            Instantiate(hitGroundPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
