using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public void SetActivation(bool isActive)
    {
        _meshRenderer.enabled = isActive;
    }
}
