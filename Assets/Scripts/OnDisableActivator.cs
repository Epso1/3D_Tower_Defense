using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableActivator : MonoBehaviour
{
    [SerializeField]
    public bool enableGameObjectOnDisable;
    public GameObject gameObjectToEnable;

    private void OnDisable()
    {
        if (enableGameObjectOnDisable)
            gameObjectToEnable.SetActive(true);
    }
}
