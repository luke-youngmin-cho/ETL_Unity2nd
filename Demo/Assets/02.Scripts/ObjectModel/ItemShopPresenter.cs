using System;

public class ItemShopPresenter
{
    // GoldData 에 대한 Dependency source
    public class GoldSource
    {
        public int gold
        {
            get => _gold;
            set
            {
                _gold = value;
                onGoldChanged?.Invoke(value);
            }
        }
        private int _gold;
        public event Action<int> onGoldChanged;

        public GoldSource()
        {
            if (DataRepository.instance.TryGet<GoldDataModel>(out IDataModel data))
            {
                _gold = ((GoldDataModel)data).gold;
                ((GoldDataModel)data).onGoldChanged += (value) => gold = value;
            }
        }
    }
    public GoldSource goldSource;

    public class ItemShopSource : ObservableCollection<int>
    {
        public ItemShopSource()
        {
            if (DataRepository.instance.TryGet<ItemShopDataModel>(out IDataModel data))
            {
                foreach (var item in ((ItemShopDataModel)data))
                {
                    items.Add(item);
                }
                ((ItemShopDataModel)data).onItemChanged += (slotIndex, item) => Change(slotIndex, item);
            }
        }
    }
    public ItemShopSource itemShopSource;

    public class SellCommand
    {
        public bool CanExecute(int slotIndex, int num)
        {
            return true; // todo -> check item is enough
        }

        public void Execute(int slotIndex, int num)
        {
            // todo -> increase gold & change slot data.
        }
    }
    public SellCommand sell;

    public ItemShopPresenter()
    {
        goldSource = new GoldSource();
        itemShopSource = new ItemShopSource();
        sell = new SellCommand();
    }


}