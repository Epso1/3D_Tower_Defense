using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject vanishPrefab;
    [SerializeField] float vanishTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("VanishItem", vanishTime);
    }

    private void VanishItem()
    {
        Instantiate(vanishPrefab, transform.position, vanishPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
