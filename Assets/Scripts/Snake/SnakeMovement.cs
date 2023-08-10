using TMPro;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private float ForwardSpeed = 5;
    [SerializeField] private float Sensitivity = 10;

    [SerializeField] private int Length = 1;

    [SerializeField] private TextMeshPro PointsText;

    private Camera mainCamera;
    private Rigidbody2D componentRigidbody;
    private SnakeTail componentSnakeTail;

    private Vector2 touchLastPos;
    private float sidewaysSpeed;

    private void Start()
    {
        mainCamera = Camera.main;
        componentRigidbody = GetComponent<Rigidbody2D>();
        componentSnakeTail = GetComponent<SnakeTail>();

        componentSnakeTail.AddCircle(Length);

        PointsText.SetText(Length.ToString());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            sidewaysSpeed = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2) mainCamera.ScreenToViewportPoint(Input.mousePosition) - touchLastPos;
            sidewaysSpeed += delta.x * Sensitivity;
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 4) sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        componentRigidbody.velocity = new Vector2(sidewaysSpeed * 5, ForwardSpeed);

        sidewaysSpeed = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Block block))
        {
            block.Fill();
            Length--;
            componentSnakeTail.RemoveCircle();
            PointsText.SetText(Length.ToString());
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bonus bonus))
        {
            int addCount = bonus.Collect();
            Length += addCount;
            componentSnakeTail.AddCircle(addCount);
            PointsText.SetText(Length.ToString());
        }
    }
}
