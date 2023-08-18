namespace ApiUsuarios
{
    public class UserRepository
    {
        private readonly Dictionary<int, User> _users = new();

        public void Create(User user)
        {
            if (user is null)
                return;

            _users[user.Cpf] = user;
        }

        public List<User> GetAll()
        {
            return _users.Values.ToList();
        }

        public User GetByCpf(int cpf)
        {
            if (_users.ContainsKey(cpf)) return _users[cpf];
            else return null;
        }

        public void Update(int cpf, User user)
        {
            _users[cpf] = user;
        }

        public void Delete(int cpf)
        {
            _users.Remove(cpf);
        }
    }
}
