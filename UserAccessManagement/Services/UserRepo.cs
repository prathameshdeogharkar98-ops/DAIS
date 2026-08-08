using Microsoft.Data.SqlClient;
using System.Data;
using UserAccessManagement.Entities.DTOs;
using UserAccessManagement.Interfaces;
using Utilities.Commons;
using Utilities.DataManagementService;

namespace UserAccessManagement.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly IDataManagementService _dataManagementService;

        public UserRepo(IDataManagementService dataManagementService)
        {
            _dataManagementService = dataManagementService;
        }

        public Dictionary<string, object> createOrUpdateUser(UserDTO userDTO)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Action", userDTO.Action),
                new SqlParameter("@CustomerId", userDTO.CustomerId),
                new SqlParameter("@FirstName", userDTO.FirstName),
                new SqlParameter("@LastName", (object?)userDTO.LastName ?? DBNull.Value),
                new SqlParameter("@Email", userDTO.Email),
                new SqlParameter("@MobileNumber", (object?)userDTO.MobileNumber ?? DBNull.Value),
                new SqlParameter("@PasswordHash", userDTO.PasswordHash),
                new SqlParameter("@IsEmailVerified", userDTO.IsEmailVerified),
                new SqlParameter("@IsMobileVerified", userDTO.IsMobileVerified),
                new SqlParameter("@Status", userDTO.Status),
                new SqlParameter("@LoggedInUser", userDTO.LoggedInUser),

                new SqlParameter("@OutStatus", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                }
            };

            _dataManagementService.ExecuteNonQuery(
                     CommonUtils.usp_ManageUser,
                    CommandType.StoredProcedure,
                    parameters);

            int result = Convert.ToInt32(parameters.Last().Value);

            return new Dictionary<string, object> { {"Output", result } };

        }

        public Dictionary<string, object> deleteUser(string CustId, string loggedInUser)
        {
            SqlParameter[] parameters =
            {
                
                new SqlParameter("@CustomerId", CustId),
                new SqlParameter("@LoggedInUser", loggedInUser),

                new SqlParameter("@OutStatus", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                }
            };

            _dataManagementService.ExecuteNonQuery(
                    CommonUtils.usp_DeleteUser,
                    CommandType.StoredProcedure,
                    parameters);

            int result = Convert.ToInt32(parameters.Last().Value);

            return new Dictionary<string, object> { { "Output", result } };
        }
    }
}
