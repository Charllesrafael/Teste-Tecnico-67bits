using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Charlles
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Backpack")]
        [SerializeField] private int m_initialCapacity = 1;
        [SerializeField] private Backpack m_backpack;
        [SerializeField] private TextMeshProUGUI m_textBackpackCapacity;
        [SerializeField] private UnityEvent m_eventColetable;

        [Header("Money")]
        [SerializeField] private UIMoney m_prefabUIMoney;
        [SerializeField] private RectTransform m_parentUI;
        [SerializeField] private TextMeshProUGUI m_textMoneyValue;
        [SerializeField] private UnityEvent m_eventChangeMoney;


        [Header("LevelUp")]
        [SerializeField] private SkinnedMeshRenderer m_meshPlayer;
        [SerializeField] private UnityEvent m_eventLevelUp;

        private int m_allMoney;

        public int AllMoney
        {
            get => m_allMoney;
            set
            {
                m_allMoney = value;
                m_eventChangeMoney?.Invoke();
                m_textMoneyValue.text = m_allMoney.ToString("00");
            }
        }


        private int m_backpackCapacity;

        public int BackpackCapacity
        {
            get => m_backpackCapacity;
            set
            {
                m_backpackCapacity = value;
                BackpackCapacityUpdate();
            }
        }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            BackpackCapacity = m_initialCapacity;
        }

        public void AddNewPack()
        {
            m_backpack.NewPackage();
            m_eventColetable?.Invoke();
            BackpackCapacityUpdate();
        }

        internal void SellAll(int m_price)
        {
            if (m_backpack.GetPackgeCount() == 0)
                return;

            int moneyEarned = m_backpack.GetPackgeCount() * m_price;
            CreateEarnedMoneyText(moneyEarned, true);
            AllMoney += moneyEarned;

            m_backpack.Clean();
            BackpackCapacityUpdate();
        }

        internal void LevelUp(int cost)
        {
            AllMoney -= cost;
            BackpackCapacity++;

            CreateEarnedMoneyText(cost, false);

            m_eventLevelUp?.Invoke();
            m_meshPlayer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        public void CreateEarnedMoneyText(int moneyEarned, bool isPositive)
        {
            Vector3 position = Camera.main.WorldToScreenPoint(m_backpack.GetBackpack().position);
            UIMoney uIMoney = Instantiate(m_prefabUIMoney, m_parentUI);
            uIMoney.transform.position = position;

            uIMoney.SetText((isPositive ? "+" : "-") + moneyEarned.ToString());
        }

        private void BackpackCapacityUpdate()
        {
            m_textBackpackCapacity.text = (m_backpack.GetPackgeCount().ToString("00") + "/" + BackpackCapacity.ToString("00"));
        }

        public bool HasPackage()
        {
            return m_backpack.GetPackgeCount() > 0;
        }

        internal bool HasEnoughMoney(int cost)
        {
            return AllMoney >= cost;
        }

        internal bool Backpackfull()
        {
            return BackpackCapacity == m_backpack.GetPackgeCount();
        }
    }
}
