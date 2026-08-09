using Administration.Entities.DTOs;
using Administration.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using Utilities.Commons;
using Utilities.DataManagementService;
using Utilities.Extensions;

namespace Administration.Services
{
    public class LoginRepo : ILoginRepo
    {
        private readonly IDataManagementService _dataManagementService;
        private readonly PasswordHashingUtil _passwordHashingUtil;

        public LoginRepo(IDataManagementService dataManagementService, PasswordHashingUtil passwordHashingUtil)
        {
            _dataManagementService = dataManagementService;
            _passwordHashingUtil = passwordHashingUtil;
        }

        public Dictionary<string, object> loginUser(LoginDTO loginDTO)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@CustomerId", loginDTO.userName)
            };
            var dt = _dataManagementService.ExecuteDataTable(
                    CommonUtils.usp_getUserByID,
                    CommandType.StoredProcedure,
                    parameters);

            var lstOfUsers = dt.convtListOfDictionary();

            if (lstOfUsers.Count <= 0)
            {
                throw new Exception("No user found");
            }

            var userInfo =  lstOfUsers.First();

            var hashedPassword = userInfo["PasswordHash"].ToString();

            var passwordArray = hashedPassword!.Split("$");

            var inPass = loginDTO.password + passwordArray[1];

            if (_passwordHashingUtil.verifyHashedPassword(passwordArray[2], inPass))
            {
                return new Dictionary<string, object> { { "status", 1 }, { "userInfo", userInfo } };
            }
            else
            {
                return new Dictionary<string, object> { { "status", 0 }, { "userInfo", null } };
            }
            
        }
    }
}
