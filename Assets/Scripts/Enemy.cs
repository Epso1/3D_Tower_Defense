using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private float speed = 2f; // Velocidad del enemigo
    [SerializeField] private int healthPoints = 10;
    [SerializeField] private Material hitMaterial;
    private SplineContainer spline;
    private float t = 0f; // Valor para recorrer la spline (0 a 1)
    [SerializeField] Renderer[] renderers;
    [HideInInspector] public string enemyName;

    private bool isDamaged = false;
    private void Awake()
    {
        spline = FindObjectOfType<SplineContainer>();
    }

    void Update()
    {
        if (spline != null)
        {
            float previousT = t; // Guardamos el valor anterior de t
            t += (speed / spline.CalculateLength()) * Time.deltaTime; // Avanzar en la spline
            t = Mathf.Clamp01(t); // Limitar t entre 0 y 1

            Vector3 currentPosition = spline.EvaluatePosition(t);
            transform.position = currentPosition; // Mover el enemigo

            // Calcular la dirección de movimiento
            Vector3 nextPosition = spline.EvaluatePosition(Mathf.Clamp01(t + 0.01f)); // Pequeña anticipación
            Vector3 direction = (nextPosition - currentPosition).normalized;

            // Rotar en la dirección del movimiento
            if (direction != Vector3.zero)
            {
                transform.forward = direction;
            }
        }
    }

    public void ReceiveDamage(int damagePoints)
    {     
        if (!isDamaged) StartCoroutine(ReceiveDamageEnum(damagePoints));
    }

    private IEnumerator ReceiveDamageEnum(int damagePoints)
    {
        isDamaged = true;
        healthPoints -= damagePoints;

        List<Material> materials = new List<Material>();
        for (int i = 0; i < renderers.Length; i++)
        {
            Material initialMaterial = renderers[i].materials[0];
            materials.Add(initialMaterial);
            renderers[i].material = hitMaterial;
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = materials[i];
        }

        if (healthPoints <= 0) { EnemyDies(); }
        isDamaged = false;
    }

    private void EnemyDies()
    {
        DestroyThis();
    }

    private void OnBecameInvisible()
    {
        DestroyThis();
    }

    private void DestroyThis()
    {
        Debug.Log($"Destroying {this.enemyName}...");
        var towers = FindObjectsOfType<Tower>();
        foreach (Tower tower in towers)
        {
            if(tower.enemiesInRange.Count > 0)
            {
                Debug.Log($"tower.enemiesInRange.Count: {tower.enemiesInRange.Count}.");
                for (int i = 0; i < tower.enemiesInRange.Count; i++)
                {
                    Debug.Log($"Searching {this.enemyName} in {tower.enemiesInRange}...");
                    if (tower.enemiesInRange[i].GetComponent<Enemy>().enemyName.Equals(this.enemyName))
                    {
                        Debug.Log($"Removing {tower.enemiesInRange[i].GetComponent<Enemy>().enemyName} from {tower.enemiesInRange}");
                        tower.enemiesInRange.Remove(tower.enemiesInRange[i]);
                        if (tower.enemiesInRange.Count > 0)
                        {
                            tower.currentTarget = tower.enemiesInRange[0];
                            Debug.Log($"Current target = {tower.currentTarget.GetComponent<Enemy>().enemyName}");
                        }
                        else
                        {
                            tower.currentTarget = null;
                            Debug.Log($"Current target = null");
                        }
                        i--;
                    }
                }
                Debug.Log($"Search finished. Enemy removed from {tower.enemiesInRange}.");
            }
            Debug.Log($"Search finished in tower {tower.name}.");
        }
        Debug.Log("Enemy removed from all lists.");
        Destroy(gameObject);
    }
}
