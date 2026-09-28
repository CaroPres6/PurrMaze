using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private string sceneChoisie;

    [SerializeField] private Image fondLevel1;
    [SerializeField] private Image fondLevel2;

    [SerializeField] private Color couleurSelec = Color.green;
    [SerializeField] private Color couleurNormal = Color.white;

    void Start()
    {
        SetLevel1();
    }

    public void SetLevel1()
    {
        sceneChoisie = "Level1";
        ChangerCouleurBoutons(true);
    }

    public void SetLevel2()
    {
        sceneChoisie = "Level2";
        ChangerCouleurBoutons(false);
    }
    private void ChangerCouleurBoutons(bool estLevel1Selectionne)
    {
        if (fondLevel1 != null)
        {
            fondLevel1.color = estLevel1Selectionne ? couleurSelec : couleurNormal;
        }

        if (fondLevel2 != null)
        {
            fondLevel2.color = estLevel1Selectionne ? couleurNormal : couleurSelec;
        }
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneChoisie);
    }
}