using UnityEngine;

namespace Charlles
{
    public class Buy : Counter
    {
        [SerializeField] private int m_valueLevelUp = 1;

        public override bool NotValid()
        {
            return !GameManager.Instance.HasEnoughMoney(m_valueLevelUp);
        }

        public override void Activate()
        {
            GameManager.Instance.LevelUp(m_valueLevelUp);
        }
    }
}
