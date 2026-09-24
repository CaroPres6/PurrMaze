using UnityEngine;

public class Collecter : MonoBehaviour
{
    [SerializeField, Min(1)] private int valeur = 1;
    private bool collectee;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collectee || !collision.CompareTag("Player")) return;
        collectee = true;
        GestionJeu.Instance.AjouterBatterie(valeur);
        Destroy(gameObject);
    }
}
