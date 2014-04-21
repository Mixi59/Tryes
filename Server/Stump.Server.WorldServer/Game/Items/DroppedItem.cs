namespace Stump.Server.WorldServer.Game.Items
{
    public class DroppedItem
    {
        public DroppedItem(int itemId, uint amount)
        {
            ItemId = itemId;
            Amount = amount;
        }

        public int ItemId
        {
            get;
            set;
        }

        public uint Amount
        {
            get;
            set;
        }
    }
}