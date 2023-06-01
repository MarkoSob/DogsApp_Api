namespace DogsApp_DAL.Models
{
    public class QueryStringParameters
    {
        public string? Attribute { get; set; }

        public string? Order { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
