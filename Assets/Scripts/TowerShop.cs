using UnityEngine;
using UnityEngine.UI;

public class TowerShop : MonoBehaviour
{
    public TowerPlacementManager placementManager; // referencia al gestor de colocaci�n
    public GameObject towerPrefab; // prefab de la torre (puedes tener varios y asignarlos seg�n el bot�n)

    // Este m�todo se vincula al bot�n "comprar" de la UI.
    public void OnBuyTowerButton()
    {
        // Aqu� podr�as comprobar si el jugador tiene saldo suficiente.
        placementManager.StartPlacement(towerPrefab);
    }
}
