using System.Collections;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int AssignedPositionIndex { get; set; }
    public int OrderId { get; private set; }
    private bool orderPlaced = false;
    private OrderManager orderManager;
    private bool isWaitingForOrder = false; // Tracks if the customer is waiting for an order
    private bool orderFulfilled = false;   // Tracks if the order has been fulfilled

    [SerializeField] private GameObject meshMale;
    [SerializeField] private GameObject meshFemale;

    private Animator maleAnimator;
    private Animator femaleAnimator;

    [SerializeField] private float patienceTime = 30f; // Time before the customer leaves if no order is given
    public Transform exitPoint; // The point where the customer exits the restaurant

    public CustomerManager customerManager = null;
    private float destroyTime = 5f;

    private Vector3 targetPosition; // Store target position for movement

    private bool leavingRestaurant = false;

    void Start()
    {
        orderManager = FindObjectOfType<OrderManager>();
        if (orderManager == null)
        {
            Debug.LogError("OrderManager not found in the scene.");
        }

        // Set active character mesh and animator
        if (meshMale && meshFemale)
        {
            bool randomCharMesh = Random.value > 0.5f;
            if (randomCharMesh)
            {
                meshMale.SetActive(true);
                meshFemale.SetActive(false);
                maleAnimator = meshMale.GetComponent<Animator>(); // Get male animator
            }
            else
            {
                meshFemale.SetActive(true);
                meshMale.SetActive(false);
                femaleAnimator = meshFemale.GetComponent<Animator>(); // Get female animator
            }
        }

        // Set animator speed based on moveSpeed
        SetAnimatorSpeed(moveSpeed);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition; // Set the target position to move towards
        StartCoroutine(MoveTowards(targetPosition));
    }

    private IEnumerator MoveTowards(Vector3 targetPosition)
    {
        // Set the speed in the animator to start moving
        SetAnimatorSpeed(moveSpeed);

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            // Move the character towards the target
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Stop the animation by setting speed to 0
        SetAnimatorSpeed(0);

        if (!orderPlaced)
        {
            PlaceOrder();
            orderPlaced = true;

            // Start the patience timer after placing the order
            StartCoroutine(PatienceTimer());
        }
    }

    private void Update()
    {
        // Rotate character towards the target position each frame
        if (targetPosition != null && Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Rotate the character to face the movement direction
            if (direction != Vector3.zero && leavingRestaurant) // Ensure the direction is valid
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Smooth rotation
            }
        }
    }

    private void PlaceOrder()
    {
        if (orderManager != null)
        {
            OrderId = orderManager.MakeOrder("hamBurger");
            isWaitingForOrder = true; // Customer starts waiting for the order
        }
    }

    private IEnumerator PatienceTimer()
    {
        float elapsedTime = 0f;

        while (elapsedTime < patienceTime)
        {
            if (orderFulfilled) // If the order is fulfilled, stop the timer
                yield break;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // If patience runs out, make the customer leave
        if (!orderFulfilled)
        {
            LeaveRestaurant();
            // orderFulfilled = true; // Mark the order as fulfilled
            isWaitingForOrder = false; // Customer is no longer waiting
            orderManager.RemoveOrder(OrderId);
        }
    }

    public void FulfillOrder()
    {
        orderFulfilled = true; // Mark the order as fulfilled
        isWaitingForOrder = false; // Customer is no longer waiting
    }

    private void LeaveRestaurant()
    {
        
        if (exitPoint != null)
        {
            // Move the customer to the exit point
            leavingRestaurant = true;
            StartCoroutine(MoveTowards(exitPoint.position));
        }
        else
        {
            Debug.LogWarning("Exit point not assigned for the customer.");
        }

        // Optionally destroy the customer object after leaving
        StartCoroutine(DestroyAfterDelay(destroyTime)); //not destroy but it will call the customer manager's remove customer function
    }

    private void SetAnimatorSpeed(float speed)
    {
        if (maleAnimator != null)
        {
            maleAnimator.SetFloat("Speed", speed); // Set the Speed parameter for the male animator
        }
        if (femaleAnimator != null)
        {
            femaleAnimator.SetFloat("Speed", speed); // Set the Speed parameter for the female animator
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (customerManager)
        {
            customerManager.RemoveCustomer(this.gameObject);
        }
    }
}
