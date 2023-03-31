namespace Deployed.Models.Response
{
    public class DependentResponseModel
    {
        public int DependentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
