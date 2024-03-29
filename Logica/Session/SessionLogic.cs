﻿using DTOs;
using Entidades;
using Logica.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Stores;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Logica.Session
{
    public class SessionLogic : ISessionLogic
    {
        private readonly IUserStore _userStore;
        private readonly ISessionStore _sessionStore;
        private readonly IUserLogic _userLogic;
        private readonly IConfiguration _configuration;

        public SessionLogic(
            IUserStore userStore,
            ISessionStore sessionStore,
            IUserLogic userLogic,
            IConfiguration configuration
        )
        {
            _userStore = userStore;
            _sessionStore = sessionStore;
            _userLogic = userLogic;
            _configuration = configuration;
        }

        LoginResponse ISessionLogic.Login(LoginDto login)
        {
            if (String.IsNullOrWhiteSpace(login.Username))
            {
                throw new ArgumentNullException(nameof(login.Username));
            }

            if (String.IsNullOrWhiteSpace(login.Password))
            {
                throw new ArgumentNullException(nameof(login.Password));
            }

            UserItem? user = _userLogic.GetForUsername(login.Username);
            if (user == null)
            {
                throw new ArgumentException("The user with that username does not exist.");
            }

            if (!PasswordHash.Verify(login.Password, user.PasswordHash))
            {
                throw new ArgumentException("Password incorrect.");
            }

            SessionItem session = new SessionItem();
            session.UserId = user.Id;
            session.OpenedAt = DateTime.Now;

            _sessionStore.InsertSession(session);

            /* Clase 9: En esta sección comienza la creación del JWT
             * 
             * Recordatorio: para usar esta característica hay que usar la dependencia: Microsoft.AspNetCore.Authentication.JwtBearer
             */

            // Clase 9: Primero se recuperan los datos de configuración para el JWT.
            Jwt? jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            //Jwt? jwt = (Jwt?)_configuration.GetSection("Jwt");//Tommy way
            if (jwt == null
                || string.IsNullOrEmpty(jwt.Key)
                || string.IsNullOrEmpty(jwt.Subject)
            )
            {
                throw new Exception("No JWT configuration.");
            }

            // Clase 9: Se crea la clave de cifrado simétrico a partir de la contraseña.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            // Clase 9: Se agrega la información del token, se puede omitir y se geenra un token minimalista
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim("sessionId", session.Id.ToString()),
                new Claim("role", user.Role ?? ""),
            };

            // Clase 9: Se firma el token con la clave simétrica creada
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.Add(jwt.ValidityTime),
                signingCredentials: singIn
            );

            /* Clase 9: Fin de la creación del JWT */

            return new LoginResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
