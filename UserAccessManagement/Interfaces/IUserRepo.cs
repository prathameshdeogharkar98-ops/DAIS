using UserAccessManagement.Entities.DTOs;

namespace UserAccessManagement.Interfaces
{
    public interface IUserRepo
    {
        public Dictionary<string, object> createOrUpdateUser(UserDTO userDTO);

        public Dictionary<string, object> deleteUser(String CustId, String loggedInUser);
    }
}
