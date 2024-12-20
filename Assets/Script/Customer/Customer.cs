using System.Collections;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform exitPoint; // The point where the customer exits the restaurant
    public int AssignedPositionIndex { get; set; }
    // public int OrderId { get; private set; }\
    public Recipe customerOrder;
    // private bool orderPlaced = false;
    // private OrderManager orderManager;
    private bool isWaitingForOrder = false; // Tracks if the customer is waiting for an order
    private bool isOrderFulfilled = false;   // Tracks if the order has been fulfilled
    private bool hasLeft = false;
    // public CustomerManager customerManager = null;
    // private float destroyTime = 5f;

    /* Animation Helper */
    [SerializeField] private GameObject meshMale;
    [SerializeField] private GameObject meshFemale;

    private Animator maleAnimator;
    private Animator femaleAnimator;
    private Vector3 targetPosition; // Store target                                    position for movement
    private bool leavingRestaurant = false;

    /* Timer */ 
    [SerializeField] private float patienceTime = 30f; // Time before the customer leaves if no order is given
    void Start()
    {
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

        // this.targetPosition = targetPosition;
    }

    public void OrderFood(Vector3 targetPosition, Recipe randomOrder) {
        // set where the customer should stand
        this.targetPosition = targetPosition;

        // move towards the counter
        StartCoroutine(MoveTowards(targetPosition));

        PlaceOrder(randomOrder);
        StartCoroutine(PatienceTimer());
    }

    // public void MoveTo(Vector3 targetPosition)
    // {
    //     this.targetPosition = targetPosition; // Set the target position to move towards
    //     StartCoroutine(MoveTowards(targetPosition));
    // }

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

        // // place food order
        // // if (!orderPlaced)
        // // {
        //     PlaceOrder();
        //     // orderPlaced = true;

        //     // Start the patience timer after placing the order
        //     StartCoroutine(PatienceTimer());
        // // }

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

    private void PlaceOrder(Recipe randomOrder)
    {
        // if (orderManager != null)
        // {
        //     OrderId = orderManager.MakeOrder("hamBurger");
        //     isWaitingForOrder = true; // Customer starts waiting for the order
        // }
        // set the customerOrder 
        customerOrder = randomOrder;
        isWaitingForOrder = true; 
    }

    private IEnumerator PatienceTimer()
    {
        float elapsedTime = 0f;

        while (elapsedTime < patienceTime)
        {
            if (isOrderFulfilled) // If the order is fulfilled, stop the timer
                yield break;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // If patience runs out, make the customer leave
        if (!isOrderFulfilled)
        {
            LeaveRestaurant();
            // orderFulfilled = true; // Mark the order as fulfilled
            isWaitingForOrder = false; // Customer is no longer waiting
            // orderManager.RemoveOrder(OrderId);
        }
    }

    public void FulfillOrder()
    {
        isOrderFulfilled = true; // Mark the order as fulfilled
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
        // StartCoroutine(DestroyAfterDelay(destroyTime)); //not destroy but it will call the customer manager's remove customer function
        hasLeft = true;
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

    // IEnumerator DestroyAfterDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     if (customerManager)
    //     {
    //         customerManager.RemoveCustomer(this.gameObject);
    //     }
    // }

}
