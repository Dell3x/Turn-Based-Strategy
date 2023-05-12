using UnityEngine;

namespace BasedStrategy.Views
{
    public class GridVisualView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        public void SetActivation(bool isActive)
        {
            _meshRenderer.enabled = isActive;
        }
    }
}