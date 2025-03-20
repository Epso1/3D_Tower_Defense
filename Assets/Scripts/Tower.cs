using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float coolDownTime = 1f;
    [SerializeField] Transform bulletOrigin;
    [HideInInspector] public List<GameObject> enemiesInRange = new List<GameObject>();
    [HideInInspector] public GameObject currentTarget;
    private bool canShoot = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadMultiply)) PrintList();
                
        if (canShoot && currentTarget != null)
        {
            StartCoroutine(ShootTarget());
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

    private void PrintList()
    {
        Debug.Log($"*** Printing list with {enemiesInRange.Count} enemies... ***");
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            Debug.Log($"{enemiesInRange[i].GetComponent<Enemy>().enemyName}");
        }
    }

}
