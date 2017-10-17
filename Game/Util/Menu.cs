using System;
using System.Collections.Generic;

namespace Game.Util
{
    class Menu
    {
        public List<MenuItem> Items;
        public int SelectedItem;

        private int posY;
        private int horizontalOffset;

        public Menu(int posY, int horizontalOffset)
        {
            Items = new List<MenuItem>();
            SelectedItem = 0;
            this.posY = posY;
            this.horizontalOffset = horizontalOffset;
        }

        public void AddItem(MenuItem item)
        {
            int posX = (Console.WindowWidth / 2) - (item.Text.Length / 2);
            item.Pos = new Point(posX + horizontalOffset, posY + (Items.Count * 2));
            Items.Add(item);
        }

        public void Paint()
        {
            for(int i = 0; i < Items.Count; i++)
            {
                if (i == SelectedItem)
                    Items[i].Select();
                else
                    Items[i].Deselect();
            }
        }

        public void SelectNext()
        {
            Items[SelectedItem++].Deselect();
            if (SelectedItem == Items.Count)
                SelectedItem = 0;
            Items[SelectedItem].Select();
        }

        public void SelectPrevious()
        {
            Items[SelectedItem--].Deselect();
            if (SelectedItem == -1)
                SelectedItem = Items.Count - 1;
            Items[SelectedItem].Select();
        }

        public int Click()
        {
            return Items[SelectedItem].Click();
        }
    }
}
