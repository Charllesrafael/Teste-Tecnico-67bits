using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Charlles
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Package")]
        [SerializeField] private Transform m_backpackPlayer;
        [SerializeField] private Package m_packagePrefab;
        [SerializeField] private UnityEvent m_eventColetable;

        [Header("Money")]
        [SerializeField] private TextMeshProUGUI m_textMoneyValue;
        [SerializeField] private UnityEvent m_eventChangeMoney;




        private Transform m_previousPack;
        private Stack<Package> m_packageList;

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

        void Awake()
        {
            Instance = this;
            m_previousPack = m_backpackPlayer;
            m_packageList = new Stack<Package>();
        }

        public void AddNewPack()
        {
            Package currentPackage = Instantiate(m_packagePrefab, m_previousPack.position, m_previousPack.rotation);
            currentPackage.SetTarget(m_previousPack);
            m_packageList.Push(currentPackage);
            m_previousPack = currentPackage.transform;
            m_eventColetable?.Invoke();
        }

        public bool HasPackage()
        {
            return m_packageList.Count > 0;
        }

        public void SellAll()
        {
            if (m_packageList.Count == 0)
                return;

            CreateEarnedMoneyText(m_packageList.Count);
            AllMoney += m_packageList.Count;

            foreach (var package in m_packageList)
            {
                Destroy(package.gameObject);
            }
            m_packageList.Clear();
            m_previousPack = m_backpackPlayer;
        }

        public void CreateEarnedMoneyText(int moneyEarned)
        {

        }
    }
}
