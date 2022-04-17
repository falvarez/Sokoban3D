using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public Vector3 targetPosition;
    public Direction direction;
    public Vector2 axisDirection;
    public GameObject child;

    public Stack<Movement> movements;


    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        direction = Direction.down;
        movements = new Stack<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Restart scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        if ((targetPosition == transform.position)
            && Input.GetKeyDown(KeyCode.U))
        {
            UndoMovement();
        }

        axisDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if ((axisDirection != Vector2.zero)
            && (targetPosition == transform.position)
        )
        {
            // El jugador ha pulsado una dirección y no nos estamos moviendo

            // Desvinculamos la caja que estábamos empujando (si lo estábamos haciendo)
            if (child != null)
            {
                // child.transform.parent = null;
                child = null;
            }

            // Calculamos el nuevo destino en función del input del jugador
            if (Mathf.Abs(axisDirection.x) > Mathf.Abs(axisDirection.y))
            {
                if (axisDirection.x > 0)
                {
                    direction = Direction.right;
                    SetTargetPosition(targetPosition + Vector3.right, direction);
                }
                else
                {
                    direction = Direction.left;
                    SetTargetPosition(targetPosition + Vector3.left, direction);
                }
            }
            else
            {
                if (axisDirection.y > 0)
                {
                    direction = Direction.up;
                    SetTargetPosition(targetPosition + Vector3.forward, direction);
                }
                else
                {
                    direction = Direction.down;
                    SetTargetPosition(targetPosition + Vector3.back, direction);
                }
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (child != null)
        {
            child.transform.position = transform.position + MapVector3FromDirection(direction);
        }

    }

    void SetTargetPosition(Vector3 targetPositionCandidate, Direction direction)
    {
        // Validar que targetPosition no está ocupada
        GameObject gameObjectInTargetPosition = this.HasGameObject(targetPositionCandidate);
        if (gameObjectInTargetPosition != null)
        {
            if (gameObjectInTargetPosition.tag == "Wall")
            {
                return;
            }
            if (gameObjectInTargetPosition.tag == "Box")
            {
                // Hacemos que la caja sea hija del gameobject, para moverlas juntas
                // gameObjectInTargetPosition.transform.parent = transform.parent; // @TODO esto no he conseguido que funcione
                child = gameObjectInTargetPosition;
            }
        }

        if (child != null)
        {
            Vector3 boxTargetPosition = targetPositionCandidate;
            boxTargetPosition += MapVector3FromDirection(direction);

            if ((boxTargetPosition != targetPositionCandidate)
                && (HasGameObject(boxTargetPosition) != null)
            )
            {
                // Ocupada, no podemos mover
                // gameObjectInTargetPosition.transform.parent = null;
                child = null;
                return;
            }
        }

        if (child != null)
        {
            movements.Push(new Movement(direction, child));
        } else
        {
            movements.Push(new Movement(direction));
        }
        targetPosition = targetPositionCandidate;
    }

    GameObject HasGameObject(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            // Debug.Log("Testing " + position);
            // Debug.Log("Tag found " + hitCollider.gameObject.tag);
            if (hitCollider.gameObject.tag != "Goal")
            {
                return hitCollider.gameObject;
            }
        }
        return null;
    }

    Vector3 MapVector3FromDirection(Direction direction)
    {
        if (direction == Direction.left)
        {
            return Vector3.left;
        }
        else if (direction == Direction.right)
        {
            return Vector3.right;
        }
        else if (direction == Direction.up)
        {
            return Vector3.forward;
        }
        else if (direction == Direction.down)
        {
            return Vector3.back;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private void UndoMovement()
    {
        // Undo movement
        if (movements.Count == 0)
        {
            return;
        }

        Movement lastMovement = movements.Pop();
        direction = lastMovement.direction;
        child = lastMovement.gameObject;
        targetPosition += lastMovement.direction.GetOpposite().GetVector3();
    }
}
