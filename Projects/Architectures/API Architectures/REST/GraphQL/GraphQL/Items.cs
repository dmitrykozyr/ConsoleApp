using GraphQL.Models;

namespace GraphQL.GraphQL;

public class Items
{
    public record AddItemInput(string title, string description, bool isDone, int listId);

    public record AddItemPayload(ItemData data);

    public record AddListInput(string name);

    public record AddListPayload(ItemList list);
}
