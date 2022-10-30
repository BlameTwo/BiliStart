using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart.Controls
{
    class TopManager
    {

        public int MaxSlot
        {
            get;
            private set;
        }

        public double TextHeight
        {
            get;
            private set;
        }

        private bool[] mSlotStatusList;
        private ArrayList IdleRows;

        public TopManager(double ContainerHeight, double TextHeight)
        {
            this.TextHeight = TextHeight;
            MaxSlot = (int)(ContainerHeight / TextHeight);
            IdleRows = new ArrayList();
            mSlotStatusList = new bool[MaxSlot];
        }

        public int getIdleSlot()
        {
            IdleRows.Clear();

            for (int i = 0; i < mSlotStatusList.Length; i++)
            {
                if (mSlotStatusList[i] == false)
                {
                    IdleRows.Add(i);
                }
            }

            if (IdleRows.Count == 0)
            {  // Force unlock all slot
                UnlockSlot();
                return GetRandomInt(0, MaxSlot - 1);
            }
            else
            {
                return (int)IdleRows[GetRandomInt(0, IdleRows.Count - 1)];
            }
        }

        private static Random ra = new Random();

        public static string GetRandomString(int _Length)
        {
            string _strList = "qwertyuioplkjhgfdsazxcvbnm1234567890";
            string _buffer = "";
            for (int i = 1; i <= _Length; i++)
            {
                _buffer += _strList[ra.Next(0, 35)];
            }
            return _buffer;
        }

        public static int GetRandomInt(int min, int max)
        {
            try
            {
                return ra.Next(min, max);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void LockSlot(int _row)
        {
            mSlotStatusList[_row] = true;
        }

        public void UnlockSlot(int _row = -1)
        {
            if (_row == -1)
            {
                mSlotStatusList = new bool[MaxSlot];
            }
            else
            {
                mSlotStatusList[_row] = false;
            }
        }
    }
}
