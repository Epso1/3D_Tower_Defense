using UnityEngine;
using UnityEngine.UI;

public class TowerShop : MonoBehaviour
{
    [SerializeField] TowerPlacementManager placementManager; // referencia al gestor de colocaci�n
    [SerializeField] GameObject towerPrefab; // prefab de la torre (puedes tener varios y asignarlos seg�n el bot�n)   

    // Este m�todo se vincula al bot�n "comprar" de la UI.
    public void OnBuyTowerButton()
    {
        /// TO DO: Comprobar si el jugador tiene saldo suficiente.
        placementManager.StartPlacement(towerPrefab);

    }
}
