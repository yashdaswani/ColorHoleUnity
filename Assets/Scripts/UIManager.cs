using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Singleton class: UIManager

    public static UIManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    [Header("Level Progress UI")]
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] TMP_Text nextLevelText;
    [SerializeField] int sceneOffset;
    [SerializeField] Image filledImage;
    [SerializeField] TMP_Text levelcompletetext;
    [SerializeField] TMP_Text timecompletetext;
    [SerializeField] public  TMP_Text scoretext;
    [SerializeField] Image fadepanelimg;
    // Start is called before the first frame update
    void Start()
    {
        panelfade();
        filledImage.fillAmount = 0f;
        setLevelProgressText();
    }

    void setLevelProgressText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();

    }

    // Update is called once per frame
    public void UpdateLevelProgress()
    {
        float val = 1f - ((float)Level.Instance.objectsInScene / Level.Instance.totalObjects);
        filledImage.DOFillAmount(val, 0.4f);
    }

    public void showLevelcompltetext()
    {
        levelcompletetext.DOFade(1f, 0.6f).From(0f);
    }
    public void panelfade()
    {
        fadepanelimg.DOFade(0f, 1.3f).From(1f);
    }

    public void timeCompleted()
    {
        timecompletetext.DOFade(1f, 0.6f).From(0f);
    }
}
