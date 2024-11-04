using UnityEngine;

namespace Charlles
{
    public class Sell : Counter
    {
        [SerializeField] private int m_price = 1;

        public override bool NotValid()
        {
            return !GameManager.Instance.HasPackage();
        }

        public override void Activate()
        {
            GameManager.Instance.SellAll(m_price);
        }
    }
}
