using System.Collections;
using UnityEngine;

public class VanishAfterTime : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] float vanishTime;
    [SerializeField] Renderer rend;

    void Start()
    {
        StartCoroutine(Vanish());
    }

    IEnumerator Vanish()
    {
        // Espera el tiempo antes de iniciar el efecto.
        yield return new WaitForSeconds(waitTime);

        float elapsed = 0f;
        // Guardamos el color inicial para conservar los valores RGB.
        Color initialColor = rend.material.color;

        while (elapsed < vanishTime)
        {
            elapsed += Time.deltaTime;
            // Calculamos el alpha interpolado de 1 a 0
            float alpha = Mathf.Lerp(1f, 0f, elapsed / vanishTime);
            Color newColor = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            rend.material.color = newColor;
            yield return null;
        }

        // Aseguramos que el alpha quede en 0 al finalizar.
        Color finalColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        rend.material.color = finalColor;
        Destroy(gameObject);
    }
}

