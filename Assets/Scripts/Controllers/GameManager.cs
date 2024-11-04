using System.Collections.Generic;
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
            BackpackCapacity = m_initialCapacity;
        }

        public void AddNewPack()
        {
            m_backpack.NewPackage();
            m_eventColetable?.Invoke();
            BackpackCapacityUpdate();
        }

        public void SellAll(int m_price)
        {
            if (m_backpack.GetPackgeCount() == 0)
                return;

            CreateEarnedMoneyText(m_backpack.GetPackgeCount());
            AllMoney += m_backpack.GetPackgeCount() * m_price;

            m_backpack.Clean();
            BackpackCapacityUpdate();
        }

        private void BackpackCapacityUpdate()
        {
            m_textBackpackCapacity.text = (m_backpack.GetPackgeCount().ToString("00") + "/" + BackpackCapacity.ToString("00"));
        }

        public void CreateEarnedMoneyText(int moneyEarned)
        {

        }

        public bool HasPackage()
        {
            return m_backpack.GetPackgeCount() > 0;
        }

        internal void LevelUp(int cost)
        {
            AllMoney -= cost;
            BackpackCapacity++;
            m_eventLevelUp?.Invoke();
            m_meshPlayer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
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
