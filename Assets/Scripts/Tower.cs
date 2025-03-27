using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float coolDownTime = 1f;
    [SerializeField] Transform bulletOrigin;
    [SerializeField] GameObject towerCharacter;
    [SerializeField] Animator towerCharacterAnimator;
    [SerializeField] float characterRotationSpeed = 5f;
    [HideInInspector] public List<GameObject> enemiesInRange = new List<GameObject>();
    [HideInInspector] public GameObject currentTarget;

    private Coroutine shootCoroutine = null;

    private void OnEnable()
    {
        Enemy.OnEnemyDestroyed += RemoveEnemyFromList;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDestroyed -= RemoveEnemyFromList;
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            UpdateCharacterRotation();

            // Si no se está disparando, iniciar la Coroutine
            if (shootCoroutine == null)
            {
                shootCoroutine = StartCoroutine(ShootTarget());
            }
        }
        else
        {
            // Si se pierde el objetivo, detener la Coroutine de disparo
            if (shootCoroutine != null)
            {
                StopCoroutine(shootCoroutine);
                shootCoroutine = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Se asigna un nombre único al enemigo
            other.gameObject.GetComponent<Enemy>().enemyName = "Enemy_" + Time.time.ToString();
            enemiesInRange.Add(other.gameObject);
            if (currentTarget == null)
            {
                currentTarget = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (enemiesInRange.Contains(other.gameObject))
            {
                enemiesInRange.Remove(other.gameObject);
                currentTarget = enemiesInRange.Count > 0 ? enemiesInRange[0] : null;
            }
        }
    }

    private IEnumerator ShootTarget()
    {
        while (currentTarget != null)
        {
            towerCharacterAnimator.SetTrigger("Shoot");
            GameObject bullet = Instantiate(bulletPrefab, bulletOrigin.position, bulletPrefab.transform.rotation);
            bullet.GetComponent<Bullet>().target = currentTarget.transform;
            yield return new WaitForSeconds(coolDownTime);
        }
        // Al salir del while, el target es nulo; se limpia la referencia
        shootCoroutine = null;
    }

    private void UpdateCharacterRotation()
    {
        if (currentTarget != null)
        {
            // Calcula la dirección ignorando el eje Y
            Vector3 direction = currentTarget.transform.position - towerCharacter.transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            towerCharacter.transform.rotation = Quaternion.Slerp(
                towerCharacter.transform.rotation,
                targetRotation,
                characterRotationSpeed * Time.deltaTime
            );
        }
    }

    private void RemoveEnemyFromList(Enemy enemy)
    {
        GameObject enemyGO = enemy.gameObject;
        if (enemiesInRange.Contains(enemyGO))
        {
            Debug.Log($"Removing {enemy.enemyName} via event from tower {name}");
            enemiesInRange.Remove(enemyGO);
            currentTarget = enemiesInRange.Count > 0 ? enemiesInRange[0] : null;
        }
    }
}
