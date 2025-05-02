using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] Material notOccupiedMaterial;
    [SerializeField] Material occupiedMaterial;

    [HideInInspector] public bool isOccupied = false; // Indica si ya hay una torre colocada en este cell.
    private MeshRenderer cellRenderer;

    private void Awake()
    {
        cellRenderer = GetComponent<MeshRenderer>();
    }
    public void UpdateCellMaterial()
    {
        cellRenderer.material = isOccupied ? occupiedMaterial : notOccupiedMaterial;
    }
}
