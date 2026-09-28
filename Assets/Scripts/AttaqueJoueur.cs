using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AttaqueJoueur : MonoBehaviour
{
    [SerializeField] private Vector2 decalageAttaque = new Vector2(1f, 0f);
    [SerializeField] private float rayonAttaque = 0.5f;
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

    private Vector2 CentreAttaque()
    {
        float direction = transform.localScale.x < 0 ? 1f : -1f;

        return (Vector2)transform.position +
               new Vector2(decalageAttaque.x * direction, decalageAttaque.y);
    }

    private void Attaquer()
    {
        animator.SetTrigger("Attaque");
        AppliquerDegats();
    }

    public void AppliquerDegats()
    {
        Vector2 centre = CentreAttaque();

        Collider2D[] touches = Physics2D.OverlapCircleAll(centre, rayonAttaque);
        Debug.Log($"Colliders touchés : {touches.Length}");

        foreach (Collider2D objet in touches)
        {
            Debug.Log($"Touché : {objet.name} | tag : {objet.tag}");

            if (!objet.CompareTag("Ennemi")) continue;

            VieEnnemi vie = objet.GetComponentInParent<VieEnnemi>();
            Debug.Log($"VieEnnemi trouvé : {vie != null}");

            if (vie != null)
            {
                vie.SubirDegats(degatsAttaque);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CentreAttaque(), rayonAttaque);
    }
}