using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrolling : MonoBehaviour
{
    public Transform[] points;
    public float speed = 5f;
    public float pauseTime = 2f;
    public float detectionRadius = 5f;
    public float chaseSpeed = 9f;
    public float attackDistance = 1f;

    private int currentPoint = 0;
    private bool isMoving = true;
    private float timer = 0f;
    public bool Chasing = false;

    public attacks damage;

    int health;
    public GameObject playerBody;


    void Start()
    {
        damage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<attacks>();
    }

    void Update()
    {


        if (isMoving)
        {
            MoveToNextPoint();
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= pauseTime)
            {
                isMoving = true;
                timer = 0f;
            }
        }

        CheckForPlayer();
    }

    void MoveToNextPoint()
    {
        if (Vector3.Distance(transform.position, points[currentPoint].position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
            currentPoint = (currentPoint + 1) % points.Length;
        }
    }

    void CheckForPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Cylinder"))
            {
                isMoving = false;
                ChasePlayer(collider.transform);
                return;
            }
        }
    }

    void ChasePlayer(Transform playerTransform)
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, chaseSpeed * Time.deltaTime);
        Chasing = true;
        if (Vector3.Distance(transform.position, playerTransform.position) < attackDistance)
        {
            damage.attacked();
        }



    }

    public void StopChase()
    {
        Chasing = false;
        // Implementa la logica per fermare l'inseguimento qui
        // Puoi anche reimplementare il metodo come necessario
        // In questo esempio, fermiamo semplicemente l'oggetto nemico.
        isMoving = true;

    }

    


}
