using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    [SerializeField] private Material _standart;
    [SerializeField] private Material _mobile;

    [SerializeField] private MeshRenderer[] _parts;

    private void Start()
    {
        if (LvlData.Instance.Id() > 3)
            SetMaterial(_mobile);
        else
            SetMaterial(_standart);

    }
    private void SetMaterial(Material material)
    {
        foreach (MeshRenderer mesh in _parts)
            mesh.material = material;
    }
}
