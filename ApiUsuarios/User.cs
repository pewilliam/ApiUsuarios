namespace ApiUsuarios
{
    public class User
    {
        public int Cpf { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public User(int cpf, string name, DateTime birthDate)
        {
            Cpf = cpf;
            Name = name;
            BirthDate = birthDate.Date;
        }
    }
}
