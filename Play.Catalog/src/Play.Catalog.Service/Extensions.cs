namespace Play.Catalog.Service
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Entities.Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}