namespace CRUDWebApi.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public string ImageProfile { get; set; }
    }
}
