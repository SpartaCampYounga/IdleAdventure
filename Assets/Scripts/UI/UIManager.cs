using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Condition UI")]
    [SerializeField] private Condition health;

    [Header("Level UI")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Condition exp;

    [Header("Gold UI")]
    [SerializeField] private TextMeshProUGUI goldText;


    private PlayerCondition playerCondition;

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        playerCondition = GameManager.Instance.player.Condition;

        playerCondition.OnHPChanged += PlayerHPChanged;
        GameManager.Instance.OnGoldChanged += PlayerGoldChanged;
        GameManager.Instance.OnLevelChanged += PlayerLevelChanged;
        GameManager.Instance.OnExpChanged += PlayerEXPChanged;
    }

    private void OnDestroy()
    {
        if(playerCondition != null)
            playerCondition.OnHPChanged -= PlayerHPChanged;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGoldChanged -= PlayerGoldChanged;
            GameManager.Instance.OnExpChanged -= PlayerEXPChanged;
            GameManager.Instance.OnLevelChanged -= PlayerLevelChanged;
        }
    }

    private void PlayerHPChanged()
    {
        health.UpdateConditionUI(playerCondition.currentHealth, playerCondition.maxHealth);
    }

    private void PlayerGoldChanged()
    {
        goldText.text = GameManager.Instance.gold.ToString();
    }

    private void PlayerEXPChanged()
    {
        int currentEXP = GameManager.Instance.exp;
        int maxEXP = GameManager.Instance.maxEXP;
        exp.UpdateConditionUI(currentEXP, maxEXP);
    }

    private void PlayerLevelChanged()
    {
        levelText.text = GameManager.Instance.level.ToString();
    }
}
