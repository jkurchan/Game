using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Util
{
    class Menu
    {
        public List<MenuItem> Items;
        public int SelectedItem;

        private int startingHeight;

        public Menu(int startingHeight)
        {
            Items = new List<MenuItem>();
            SelectedItem = 0;
            this.startingHeight = startingHeight;
        }

        public void AddItem(MenuItem item)
        {
            int posY = startingHeight - Items.Count;
            Items.Add(item);

            foreach (MenuItem i in Items)
            {
                int posX = (Console.WindowWidth / 2) - (i.Text.Length / 2);
                i.Pos = new Point(posX, posY);
                posY += 2;
            }
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
