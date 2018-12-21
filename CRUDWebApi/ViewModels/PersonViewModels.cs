namespace CRUDWebApi.ViewModels
{
    public class PersonViewModels
    {
        public bool Gender;
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string GenderByName { get { return Gender == true ? "Male" : "Female"; } }
        public bool SetGender{set { Gender = value; }
        }
    }
}
