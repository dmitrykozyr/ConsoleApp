namespace GraphQL.Models;

public class ItemList
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<ItemData> ItemDatas { get; set; }

    public ItemList()
    {
        ItemDatas = new HashSet<ItemData>();
    }
}
