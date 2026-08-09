using Administration.Entities.DTOs;

namespace Administration.Interfaces
{
    public interface ILoginRepo
    {
        public Dictionary<string, object> loginUser(LoginDTO loginDTO);
    }
}
