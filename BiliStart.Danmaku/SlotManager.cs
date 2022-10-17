using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BiliStart.Danmaku
{
    class SlotManager {

        public int MaxSlot {
            get;
            private set;
        }

        public double TextHeight {
            get;
            private set;
        }

        private bool[] mSlotStatusList;
        private ArrayList IdleRows;

        /// <summary>
        /// Horizontal movement danmaku slot manager
        /// Using fixed font size for best practice
        /// </summary>
        /// <param name="ContainerHeight">Danmaku container height</param>
        /// <param name="TextHeight">Danmaku text height</param>
        public SlotManager(double ContainerHeight, double TextHeight) {
            this.TextHeight = TextHeight;
            MaxSlot = (int)(ContainerHeight / TextHeight);
            IdleRows = new ArrayList();
            mSlotStatusList = new bool[MaxSlot];
        }

        public int getIdleSlot() {
            IdleRows.Clear();

            for (int i = 0; i < mSlotStatusList.Length; i++) {
                if (mSlotStatusList[i] == false) {
                    IdleRows.Add(i);
                }
            }

            if (IdleRows.Count == 0) {  // Force unlock all slot
                UnlockSlot();
                return Utils.GetRandomInt(0, MaxSlot - 1);
            } else {
                return (int)IdleRows[Utils.GetRandomInt(0, IdleRows.Count - 1)];
            }
        }

        public void LockSlot(int _row) {
            mSlotStatusList[_row] = true;
        }

        public void UnlockSlot(int _row = -1) {
            if (_row == -1) {
                mSlotStatusList = new bool[MaxSlot];
            } else {
                mSlotStatusList[_row] = false;
            }
        }
    }
}
