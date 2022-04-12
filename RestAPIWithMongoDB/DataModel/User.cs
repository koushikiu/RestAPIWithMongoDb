namespace RestAPIWithMongoDB.DataModel
{
    public record User
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
    }
}
