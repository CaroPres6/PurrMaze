using UnityEngine;

public class PorteSortie : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GestionJeu.Instance != null)
        {
            GestionJeu.Instance.DeclencherVictoire();
        }
    }
}