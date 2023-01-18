using UnityEngine;

namespace BasedStrategy.Mouse
{
    public class MouseWorld : MonoBehaviour
    {
        [SerializeField] private LayerMask _mouseLayerMask;
        public static MouseWorld Instance;

        private void Awake()
        {
            Instance = this;
        }

        public static Vector3 GetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance._mouseLayerMask);
            return raycastHit.point;
        }
    }
}