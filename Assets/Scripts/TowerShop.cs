using UnityEngine;
using UnityEngine.UI;

public class TowerShop : MonoBehaviour
{
    [SerializeField] TowerPlacementManager placementManager; // referencia al gestor de colocación
    [SerializeField] GameObject towerPrefab; // prefab de la torre (puedes tener varios y asignarlos según el botón)   

    // Este método se vincula al botón "comprar" de la UI.
    public void OnBuyTowerButton()
    {
        /// TO DO: Comprobar si el jugador tiene saldo suficiente.
        placementManager.StartPlacement(towerPrefab);

    }
}
