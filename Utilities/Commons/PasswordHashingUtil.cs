using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Commons
{
    public class PasswordHashingUtil
    {
        public String generateSalt()
        {
            var salt = "";

            for (int i=0;i<7;i++)
            {
                Random random = new Random();

                int randomNumber = i%2==0? random.Next(97, 123): random.Next(64, 91);
                char randomCharacter = (char)randomNumber;

                salt = salt+randomCharacter.ToString();
            }
            
            return salt;
        }

        public String generateHashPassword(String rawPassword)
        {
            
            var  salt = generateSalt();
            
            var combinedString = rawPassword + salt;
            var hasher = new PasswordHasher<object>();
            string passwordHash = hasher.HashPassword(null!, combinedString);
            var password = "passwordhasher" + "$" + salt + "$" + passwordHash;
            return password;
        }

        public bool verifyHashedPassword(string storedHash,string saltPass)
        {
            var hasher = new PasswordHasher<object>();
            var result = hasher.VerifyHashedPassword(null,storedHash,saltPass);
            if(result== PasswordVerificationResult.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }



    }
}
