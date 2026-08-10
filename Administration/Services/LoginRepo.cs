using Administration.Entities.DTOs;
using Administration.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using Utilities.Commons;
using Utilities.DataManagementService;
using Utilities.Extensions;
using Utilities.JWTServices;

namespace Administration.Services
{
    public class LoginRepo : ILoginRepo
    {
        private readonly IDataManagementService _dataManagementService;
        private readonly PasswordHashingUtil _passwordHashingUtil;
        private readonly JwtService _jwtService;

        public LoginRepo(IDataManagementService dataManagementService, PasswordHashingUtil passwordHashingUtil, JwtService jwtService)
        {
            _dataManagementService = dataManagementService;
            _passwordHashingUtil = passwordHashingUtil;
            _jwtService = jwtService;
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

            if (bool.Parse(userInfo["LoginStatus"].ToString()))
            {
                throw new Exception("User is Locked!!");
            }

            var hashedPassword = userInfo["PasswordHash"].ToString();

            var passwordArray = hashedPassword!.Split("$");

            var inPass = loginDTO.password + passwordArray[1];

            if (_passwordHashingUtil.verifyHashedPassword(passwordArray[2], inPass))
            {
                var token = _jwtService.generateToken(userInfo["UserId"].ToString(), userInfo["Email"].ToString());
                var refreshToken = _jwtService.generateRefreshToken();


                //Call SP
                SqlParameter[] parametersLogin =
                    {
                        new SqlParameter("@UserId", userInfo["UserId"].ToString()),
                        new SqlParameter("@JWTToken", token),
                        new SqlParameter("@RefreshToken", refreshToken),
                        new SqlParameter("@Output", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            }
                    };

                _dataManagementService.ExecuteNonQuery(
                    CommonUtils.usp_ManageLogin,
                    CommandType.StoredProcedure,
                    parametersLogin);

                return new Dictionary<string, object> { { "status", 1 }, { "userInfo", userInfo },
                 { "token",token}, { "refreshToken",refreshToken} };
            }
            else
            {
                return new Dictionary<string, object> { { "status", 0 }, { "userInfo", null } };
            }
            
        }
    }
}
