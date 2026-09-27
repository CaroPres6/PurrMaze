using UnityEngine;

public class PorteSortie : MonoBehaviour
{
    private Animator animator;
    private bool porteOuverte;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (porteOuverte) return;
        if (!GestionJeu.Instance.ObjectifAtteint) return;

        porteOuverte = true;
        animator.SetTrigger("Ouvrir");
        GestionJeu.Instance.DeclencherVictoire();
    }
}
