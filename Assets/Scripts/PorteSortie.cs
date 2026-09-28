using UnityEngine;

public class PorteSortie : MonoBehaviour
{
    [SerializeField] private bool estPorteVictoire = false;
    [SerializeField] private string autreScene = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GestionJeu.Instance != null && estPorteVictoire)
        {
            GestionJeu.Instance.DeclencherVictoire();
        }
        else
        {
            if (autreScene.Trim() != "")
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(autreScene);
            }
        }
    }
}