using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Engine
{
    public class Vendor : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public BindingList<InventoryItem> Inventory { get; private set; }

        public Vendor(string name)
        {
            Name = name;
            Inventory = new BindingList<InventoryItem>();
        }

        public void AddItemToInventory(Item itemToAdd, int quantity = 1)
        {
            InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToAdd.ID);

            if (item == null)
            {
                // They didn't have the item, so add it to their inv
                Inventory.Add(new InventoryItem(itemToAdd, quantity));
            }
            else
            {
                // They do have the item, so increase quantity
                item.Quantity += quantity;
            }

            // Notify UI that inv has changed
            OnPropertyChanged("Inventory");
        }

        public void RemoveItemFromInventory(Item itemToRemove, int quantity = 1)
        {
            InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToRemove.ID);

            if (item == null)
            {
                // The item is not in player's inv, so ignore it.
                // Might want to raise an error for this situation
            }
            else
            {
                // They have item in inv, so decrease quantity
                item.Quantity -= quantity;

                // Don't allow negative quantities
                // We might want to raise an error for this situation
                if (item.Quantity < 0)
                {
                    item.Quantity = 0;
                }

                // If quantity is 0, remove item from the list
                if (item.Quantity == 0)
                {
                    Inventory.Remove(item);
                }

                // Notify UI that inv has changed
                OnPropertyChanged("Inventory");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
