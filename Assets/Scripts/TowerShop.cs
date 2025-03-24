using UnityEngine;
using UnityEngine.UI;

public class TowerShop : MonoBehaviour
{
    public TowerPlacementManager placementManager; // referencia al gestor de colocación
    public GameObject towerPrefab; // prefab de la torre (puedes tener varios y asignarlos según el botón)

    // Este método se vincula al botón "comprar" de la UI.
    public void OnBuyTowerButton()
    {
        // Aquí podrías comprobar si el jugador tiene saldo suficiente.
        placementManager.StartPlacement(towerPrefab);
    }
}
