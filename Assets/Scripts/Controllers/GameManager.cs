using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Charlles
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;


        [Header("UI Game")]
        [SerializeField] private GameObject m_panelUIGame;
        [SerializeField] private GameObject m_uiPlay;


        [Header("Backpack")]
        [SerializeField] private int m_initialCapacity = 1;
        [SerializeField] private Backpack m_backpack;

        [Space(10)]
        [SerializeField] private TextMeshProUGUI m_textBackpackCapacity;


        [Header("Money")]
        [SerializeField] private UIMoney m_prefabUIMoney;

        [Space(10)]
        [SerializeField] private RectTransform m_parentUI;
        [SerializeField] private TextMeshProUGUI m_textMoneyValue;


        [Header("LevelUp")]
        [SerializeField] private SkinnedMeshRenderer m_meshPlayer;


        [Header("Audios")]
        [SerializeField] private AudioSource m_punchingSound;
        [SerializeField] private AudioSource m_impactSound;
        [SerializeField] private AudioSource m_collectingSound;
        [SerializeField] private AudioSource m_sellSound;
        [SerializeField] private AudioSource m_levelupSound;

        private int m_allMoney;

        public int AllMoney
        {
            get => m_allMoney;
            set
            {
                m_allMoney = value;
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

        private bool m_playing;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            BackpackCapacity = m_initialCapacity;
        }

        public void PlayGame()
        {
            m_uiPlay.SetActive(false);
            m_panelUIGame.SetActive(true);
            m_playing = true;
        }

        public void AddNewPack()
        {
            m_backpack.NewPackage();
            m_collectingSound?.Play();
            BackpackCapacityUpdate();
        }

        internal void SellAll(int m_price)
        {
            if (m_backpack.GetPackgeCount() == 0)
                return;

            int moneyEarned = m_backpack.GetPackgeCount() * m_price;
            AllMoney += moneyEarned;
            m_sellSound?.Play();

            m_backpack.Clean();
            CreateEarnedMoneyText(moneyEarned, true);
            BackpackCapacityUpdate();
        }

        internal void LevelUp(int cost)
        {
            AllMoney -= cost;
            m_levelupSound?.Play();

            BackpackCapacity++;

            CreateEarnedMoneyText(cost, false);
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

        internal bool IsPlaying()
        {
            return m_playing;
        }

        internal void PlayPunchSound()
        {
            m_punchingSound?.Play();
        }

        internal void PlayImpactSound()
        {
            m_impactSound?.Play();
        }
    }
}
