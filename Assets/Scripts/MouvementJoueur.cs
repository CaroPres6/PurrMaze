using UnityEngine;
using UnityEngine.InputSystem;
 
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class MouvementJoueur : MonoBehaviour
{
    [SerializeField] private float vitesse = 5f;

    private Rigidbody2D corps;
    private Animator animator;

    private Vector2 direction;

    private void Awake()
    {
        corps = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        LireClavier();

        // Informe l'Animator si le chat se déplace ou non.
        animator.SetBool(
            "EnMouvement",
            direction.sqrMagnitude > 0.01f
        );
    }

    private void FixedUpdate()
    {
        corps.linearVelocity =
            direction * vitesse;
    }

    private void LireClavier()
    {
        if (Keyboard.current == null)
        {
            direction = Vector2.zero;
            return;
        }

        float horizontal = 0f;
        float vertical = 0f;

        // Gauche
        if (Keyboard.current.leftArrowKey.isPressed ||
            Keyboard.current.aKey.isPressed)
        {
            horizontal = -1f;
        }

        // Droite
        if (Keyboard.current.rightArrowKey.isPressed ||
            Keyboard.current.dKey.isPressed)
        {
            horizontal = 1f;
        }

        // Bas
        if (Keyboard.current.downArrowKey.isPressed ||
            Keyboard.current.sKey.isPressed)
        {
            vertical = -1f;
        }

        // Haut
        if (Keyboard.current.upArrowKey.isPressed ||
            Keyboard.current.wKey.isPressed)
        {
            vertical = 1f;
        }

        direction =
            new Vector2(horizontal, vertical).normalized;
    }
}
