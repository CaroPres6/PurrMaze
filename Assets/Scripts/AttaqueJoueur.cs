using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AttaqueJoueur : MonoBehaviour
{
    [SerializeField] private Transform pointAttaque;
    [SerializeField] private float rayonAttaque = 0.5f;
    [SerializeField] private LayerMask coucheEnnemis;
    [SerializeField] private int degatsAttaque = 20;
    [SerializeField] private float vitesseAttaque = 0.5f;

    private Animator animator;
    private float tempsProchaineAttaque;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        LireClavier();
    }

    private void LireClavier()
    {
        if (Time.time < tempsProchaineAttaque) return;

        bool veutAttaquer =
            (Keyboard.current != null &&
             (Keyboard.current.qKey.wasPressedThisFrame ||
              Keyboard.current.spaceKey.wasPressedThisFrame)) ||
            (Mouse.current != null &&
             Mouse.current.leftButton.wasPressedThisFrame);

        if (veutAttaquer)
        {
            Attaquer();
            tempsProchaineAttaque = Time.time + vitesseAttaque;
        }
    }

    private void Attaquer()
    {
        animator.SetTrigger("Attaque");
        AppliquerDegats();
    }

    public void AppliquerDegats()
    {
        Vector2 centre = pointAttaque != null
            ? (Vector2)pointAttaque.position
            : (Vector2)transform.position;

        Collider2D[] touches = Physics2D.OverlapCircleAll(centre, rayonAttaque, coucheEnnemis);

        foreach (Collider2D objet in touches)
        {
            SanteEntite sante = objet.GetComponentInParent<SanteEntite>();
            if (sante != null)
            {
                sante.SubirDegats(degatsAttaque);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 centre = pointAttaque != null ? pointAttaque.position : transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centre, rayonAttaque);
    }
}