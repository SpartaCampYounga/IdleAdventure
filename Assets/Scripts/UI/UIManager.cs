using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Condition UI")]
    [SerializeField] private Condition health;

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
    }

    private void OnDestroy()
    {
        playerCondition.OnHPChanged -= PlayerHPChanged;
        GameManager.Instance.OnGoldChanged -= PlayerGoldChanged;
    }

    private void PlayerHPChanged()
    {
        health.UpdateConditionUI(playerCondition.currentHealth, playerCondition.maxHealth);
    }

    private void PlayerGoldChanged()
    {
        goldText.text = GameManager.Instance.gold.ToString();
    }
}
