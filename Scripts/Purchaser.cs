using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour
{
    public void OnPurschaseComplete(Product product)
    {
        switch (product.definition.id)
        {
            case "noad":
                RemoveAd();
                break;
        }
    }
    public void RemoveAd()
    {
        PlayerPrefs.SetInt("AdEnabled", 0);
        MenuUI.Instance.DisableAd();
    }
}
