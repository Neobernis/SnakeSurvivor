using UnityEngine;

namespace CodeBase.Snake
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private Vector2 DefaultResolution = new Vector2(720, 1280);
        [Range(0f, 1f), SerializeField] private float WidthOrHeight = 0;

        private Camera componentCamera;

        private float initialSize;
        private float targetAspect;

        private void Start()
        {
            componentCamera = GetComponentInChildren<Camera>();
            initialSize = componentCamera.orthographicSize;

            targetAspect = DefaultResolution.x / DefaultResolution.y;
        }

        private void Update()
        {
            Vector3 transformPosition = transform.position;
            if(_target != null)
                transformPosition.y = _target.position.y;
            transform.position = transformPosition;

            float constantWidthSize = initialSize * (targetAspect / componentCamera.aspect);
            componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, WidthOrHeight);
        }
    }
}
