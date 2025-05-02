using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyDestroyed;

    [SerializeField] private float speed = 2f;
    [SerializeField] private int healthPoints = 10;
    [SerializeField] private Material hitMaterial;
    private SplineContainer spline;
    private float t = 0f;
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
            t += (speed / spline.CalculateLength()) * Time.deltaTime;
            t = Mathf.Clamp01(t);

            Vector3 currentPosition = spline.EvaluatePosition(t);
            transform.position = currentPosition;

            // Calcula la dirección de movimiento para rotar
            Vector3 nextPosition = spline.EvaluatePosition(Mathf.Clamp01(t + 0.01f));
            Vector3 direction = (nextPosition - currentPosition).normalized;

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

        if (healthPoints <= 0)
        {
            EnemyDies();
        }
        isDamaged = false;
    }

    private void EnemyDies()
    {
        DestroyThis();
    }

    private void DestroyThis()
    {
        //Debug.Log($"Destroying {this.enemyName}...");
        // Notifica a todas las torres suscriptoras
        OnEnemyDestroyed?.Invoke(this);
        //Debug.Log("Enemy removed from all lists.");
        Destroy(gameObject);
    }
}
