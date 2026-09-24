using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestionJeu : MonoBehaviour
{
    public static GestionJeu Instance { get; private set; }

    [Header("Progression")]
    [SerializeField] private int objectifBatteries = 3;
    [SerializeField] private int viesInitiales = 3;

    [Header("Interface")]
    [SerializeField] private TMP_Text texteBatteries;
    [SerializeField] private TMP_Text texteVies;
    [SerializeField] private Image barreProgression;
    [SerializeField] private GameObject panneauVictoire;
    [SerializeField] private GameObject panneauDefaite;

    [Header("Niveau")]
    [SerializeField] private GameObject porteSortie;
    [SerializeField] private MouvementJoueur joueur;
    //[SerializeField] private AudioJeu audioJeu;

    private int batteriesCollectees;
    private int vies;
    private bool partieTerminee;

    public bool PartieTerminee => partieTerminee;
    public bool ObjectifAtteint => batteriesCollectees >= objectifBatteries;

    [SerializeField] private EffetDegatsJoueur effetDegatsJoueur;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        vies = viesInitiales;
        batteriesCollectees = 0;
        partieTerminee = false;
        panneauVictoire.SetActive(false);
        panneauDefaite.SetActive(false);
        porteSortie.SetActive(false);
        ActualiserInterface();
        //audioJeu?.JouerLancement();
    }

    public void AjouterBatterie(int valeur = 1)
    {
        if (partieTerminee) return;
        batteriesCollectees += valeur;
        //audioJeu?.JouerCollecte();
        ActualiserInterface();
        if (ObjectifAtteint)
        {
            porteSortie.SetActive(true);
            //audioJeu?.JouerObjectif();
        }
    }

    public void PerdreVie()
    {
        if (partieTerminee)
            return;

        vies = Mathf.Max(vies - 1, 0);
        //audioJeu?.JouerImpact();

        if (effetDegatsJoueur != null)
            effetDegatsJoueur.DeclencherEffet();

        ActualiserInterface();

        if (vies == 0)
            DeclencherDefaite();
    }

    public void DeclencherVictoire()
    {
        if (partieTerminee || !ObjectifAtteint) return;
        partieTerminee = true;
        panneauVictoire.SetActive(true);
        //joueur.DesactiverCommandes();
        //audioJeu?.JouerVictoire();
    }

    public void DeclencherDefaite()
    {
        if (partieTerminee) return;
        partieTerminee = true;
        panneauDefaite.SetActive(true);
        //joueur.DesactiverCommandes();
        //audioJeu?.JouerDefaite();
    }

    public void TempsEcoule() => DeclencherDefaite();

    private void ActualiserInterface()
    {
        texteBatteries.text = $"Batteries : {batteriesCollectees}/{objectifBatteries}";
        texteVies.text = $"Vies : {vies}";
        if (barreProgression != null)
            barreProgression.fillAmount = objectifBatteries > 0
                ? (float)batteriesCollectees / objectifBatteries
                : 0f;
    }

    public void RecommencerPartie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
