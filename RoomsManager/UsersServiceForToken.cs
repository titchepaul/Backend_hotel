using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoomsManager.JWT;
using RoomsManager.Models;
using RoomsManager.MonServives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoomsManager
{
    public class UsersServiceForToken : UsersService
    {
        // variable permettant d' executer des opérations de type SQL 
        private readonly DefaultContext _context;

        // Permet de récuperer des valeurs de mon fichier appSettigs
        private readonly IOptions<JWTParam> _appSettings;

        public UsersServiceForToken(DefaultContext context, IOptions<JWTParam> app_settings)
        {
            this._context = context;
            this._appSettings = app_settings;
        }
        /*
         * source: https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api#users-controller-cs
         * header // tête du token 258
         * payload //represente les infos
         * signature
        */
        public SecurityToken createToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor();
            var key = _appSettings.Value.keyJWT;

            Debug.WriteLine("Valeur Clé Secrete : " + _appSettings.Value.keyJWT);


            List<Claim> claimslist = new List<Claim>();
            claimslist.Add(new Claim("Email", user.UserEmail));
            claimslist.Add(new Claim("Role", user.UserRole));          

            tokenDescriptor.Subject = new ClaimsIdentity(claimslist);
            tokenDescriptor.SigningCredentials = generateCertificat(key); ;
            tokenDescriptor.Expires = DateTime.Now.AddDays(1);
            //tokenDescriptor.Expires = DateTime.Now.AddMinutes(3);

            return tokenHandler.CreateToken(tokenDescriptor);
        }
        /// <summary>
        /// Validation de la date (duré de vie du token)
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public bool dateValid(DateTime dt1, DateTime dt2)
        {

            int valeurDate = dt1.CompareTo(dt2);
            Debug.WriteLine("Date 1 : " + dt1 + " vs Date 2 : " + dt2);
            //dt1 est > à dt2 donc date Tjrs valide
            if (valeurDate > 0)
            {

                return true;
            }
            else if (valeurDate == 0)
            {
                return true;
            }
            else
            {
                //dt1 est < à dt2 donc date déja passer
                return false;
            }
        }

        public SigningCredentials generateCertificat(string secretKey)
        {
            var key = Encoding.UTF8.GetBytes(secretKey);
            return new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        }

        /*
         * Source : https://www.sha256.fr/questions
         */
         // cryptage du password mais pas utilisé à cause du cryptage niveau frontend @angular
        public static string sha256(string password)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        public Users login(string username, string password)
        {
            Users user = _context.Users.Find(username);
            var tokenHandler = new JwtSecurityTokenHandler();

            if (user == null || user.Password.Equals(password) == false)
            {
                Debug.WriteLine("Nom d'utilisateur ou mot de passe incorrect");
                return null;
            }

            // Si le token est null donc user n' a pas eu de token 
            if (user.Token == null)
            {
                Debug.WriteLine("Token  est à null");
                var token = createToken(user);
                user.Token = tokenHandler.WriteToken(token);
                _context.Users.Update(user);
                _context.SaveChanges();

            }
            // Si le token et encore valide on renvoie le token 
            if (tokenValidate(user.Token) == true)
            {
                Debug.WriteLine("Token toujours valide");
                return user;
            }
            //Si le token n' est pas valide on regenere un autre
            if (tokenValidate(user.Token) == false)
            {

                Debug.WriteLine("Token info n'est pas valide : ");
                var token = createToken(user);
                user.Token = tokenHandler.WriteToken(token); //on génère un nouveau token
                _context.Users.Update(user); //on le mets a jour pour le user
                _context.SaveChanges(); //et on enrégistre le changement dans la bd

            }
            return user;
        }

        public bool tokenValidate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenInfo = tokenHandler.ReadJwtToken(token);

            // Recupere la date d' expiration du token 

            DateTime tokenDate = tokenInfo.ValidTo.AddHours(2);
            Debug.WriteLine("Token date parse : " + tokenInfo.ValidTo);
            Debug.WriteLine("Token date : " + tokenDate + " Boolean Date heure " + dateValid(tokenDate, DateTime.Now));

            return dateValid(tokenDate, DateTime.Now);
        }
    }
}
