using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float coolDownTime = 1f;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] GameObject towerCharacter;
    [SerializeField] float characterRotationSpeed = 5f;
    [HideInInspector] public List<GameObject> enemiesInRange = new List<GameObject>();
    [HideInInspector] public GameObject currentTarget;
    private bool canShoot = true;

    private void Update()
    {           
        if (currentTarget != null)
        { 
            UpdateCharacterRotation(); 
            if (canShoot) { StartCoroutine(ShootTarget()); }
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().enemyName = "Enemy_" + Time.time.ToString();
            enemiesInRange.Add(other.gameObject);
            Debug.Log($"{other.gameObject.GetComponent<Enemy>().enemyName} added to list.");
            if (!currentTarget)
            {
                currentTarget = enemiesInRange[enemiesInRange.IndexOf(other.gameObject)];
                Debug.Log($"Current target = {currentTarget.GetComponent<Enemy>().enemyName}.");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                if (other.gameObject.GetComponent<Enemy>().enemyName.Equals(enemiesInRange[i].GetComponent<Enemy>().enemyName)) 
                {
                    Debug.Log($"Removing {enemiesInRange[i].GetComponent<Enemy>().enemyName} from list.");
                    enemiesInRange.Remove(enemiesInRange[i]);
                    if (enemiesInRange.Count > 0)
                    {
                        currentTarget = enemiesInRange[0];
                        Debug.Log($"Current target = {currentTarget.GetComponent<Enemy>().enemyName}");
                    }
                    else
                    {
                        currentTarget = null;
                        Debug.Log($"Current target = null");
                    }                    
                }
            }          
        }
    }

    private IEnumerator ShootTarget()
    {
        canShoot = false;

        while (currentTarget != null)
        {
            if (currentTarget.transform != null)
            {
                Vector3 direction = (currentTarget.transform.position - bulletOrigin.position).normalized;
                GameObject bullet = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().direction = direction;
                yield return new WaitForSeconds(coolDownTime);
            }                    
        }
        canShoot = true;
    }

    private void RotateCharacter()
    {
        // Obt�n la posici�n del objetivo y la posici�n del personaje
        Vector3 targetPosition = currentTarget.transform.position;
        Vector3 towerPosition = towerCharacter.transform.position;

        // Ignora la diferencia en el eje Y para evitar que el personaje mire hacia abajo
        targetPosition.y = towerPosition.y;

        // Haz que el personaje mire hacia el objetivo (solo en el eje Y)
        towerCharacter.transform.LookAt(targetPosition);
    }

    private void UpdateCharacterRotation()
    {
        if (currentTarget != null)
        {
            // Obt�n la direcci�n hacia el objetivo (ignorando el eje Y)
            Vector3 direction = currentTarget.transform.position - towerCharacter.transform.position;
            direction.y = 0; // Ignora la diferencia en el eje Y

            // Calcula la rotaci�n deseada
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Interpola suavemente hacia la rotaci�n deseada
            towerCharacter.transform.rotation = Quaternion.Slerp(
                towerCharacter.transform.rotation,
                targetRotation,
                characterRotationSpeed * Time.deltaTime
            );
        }
    }
}
