using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float rotateSpeed = 75;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private KeyCode keyJump = KeyCode.Space;
    [SerializeField] private float distanceFromGround = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed = 100;

    private const string VERTICAL_AXIS = "Vertical";
    private const string HORIZPNTAL_AXIS = "Horizontal";

    private float vInput;
    private float hInput;
    private Rigidbody rb;
    private bool canJump = false;
    private CapsuleCollider capsuleCollider;
    private bool canShoot = false;
    
    private GameBehavior gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        gameManager = GameObject.FindObjectOfType<GameBehavior>().GetComponent<GameBehavior>();
    }

    private void Update()
    {
        vInput = Input.GetAxis(VERTICAL_AXIS) * moveSpeed;
        hInput = Input.GetAxis(HORIZPNTAL_AXIS) * rotateSpeed;

        if (Input.GetKeyDown(keyJump))
        {
            canJump = true;
        }
        
        // transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        // transform.Rotate(Vector3.up * hInput * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            canShoot = true;
        }
    }

    private void FixedUpdate()
    {
        if (canJump && IsGrounded())
        {
            canJump = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (canShoot)
        {
            canShoot = false;
            GameObject newBullet = Instantiate(bullet, transform.position + new Vector3(1, 0, 0), transform.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        }
        
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angelRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        rb.MovePosition(transform.position + transform.forward * vInput * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * angelRot);
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom =
            new Vector3(capsuleCollider.center.x, capsuleCollider.bounds.min.y, capsuleCollider.center.z);

        bool isGrounded = Physics.CheckCapsule(capsuleCollider.bounds.center, capsuleBottom, distanceFromGround,
            groundLayer, QueryTriggerInteraction.Ignore);

        return isGrounded;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameManager.PlayerHP -= 1;
        }
    }
}
