using virtual_pet.Core.Level.Common;
using virtual_pet.Core.Entity.Common;
using virtual_pet.Core.GameEngine.Menu;

namespace virtual_pet.Core.Model
{
    public class Shop : ConsoleMenu
    {
        private List<ItemBase> availableItems;
        private LevelBase owner;

        public Shop(LevelBase owner, List<ItemBase> items)
        {
            UseDefaultHandler = false;
            onItemSelected += menuHandler;
            this.owner = owner;
            availableItems = items;

            for (int i = 0; i < items.Count; i++)
            {
                CreateClickItem(items[i].Name + " [" + items[i].Count + "]", onItemSelection);
            }
        }

        private void menuHandler(object sender, int id)
        {
            if (id == ACTION_CLOSE)
            {
                owner.CloseShop();
                return;
            }
            DefaultItemSelected(id);
        }

        private void onItemSelection(object sender, string text)
        {
            ClickItem item = (ClickItem)sender;
            SellItem(availableItems[item.Id]);
            item.Text = availableItems[item.Id].Name + " [" + availableItems[item.Id].Count + "]";
        }

        public void SellItem(ItemBase item)
        {
        }
    }
}
