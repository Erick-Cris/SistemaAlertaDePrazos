using AlertaDePrazosLibrary.Entities.AlertaDePrazos;
using AlertaDePrazosLibrary.Utils;
using ApiAlertaDePrazos.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ApiAlertaDePrazos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        [HttpGet]
        [Route("BuscarPorId")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario usuario = null;
                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    usuario = db.Usuarios.Where(x => x.Id == id).ToList().FirstOrDefault();
                    usuario.PasswordHash = string.Empty;
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        [Route("AutenticarUsuario")]
        public IActionResult AutenticarUsuario(string email, string passwordHash)
        {
            try
            {
                Usuario usuario = null;
                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    usuario = db.Usuarios.Where(x => x.Email == email && x.PasswordHash == passwordHash).ToList().FirstOrDefault();
                }

                if(usuario != null)
                {
                    usuario.PasswordHash = String.Empty;
                    return Ok(usuario);
                }
                else
                    return StatusCode(StatusCodes.Status401Unauthorized, "Usuário ou senha inválidos.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Criar")]
        public IActionResult Criar(Usuario usuario)
        {
            try
            {
                // Generate a 128-bit salt using a sequence of
                // cryptographically strong random bytes.
                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
                Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

                // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: usuario.PasswordHash!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    if (db.Usuarios.Where(x => x.Email == usuario.Email).ToList().Count > 0)
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Este e-mail já está sendo usado.");

                    usuario.PasswordHash = hashed;
                    usuario.IsActive = false;
                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                }

                //Token
                byte[] textoAsBytes = Encoding.ASCII.GetBytes($"{usuario.Id}:{usuario.Email}");
                string token = System.Convert.ToBase64String(textoAsBytes);

                string linkConfirmacao = $"https://localhost:7241/Usuario/Ativar/{token}";

                string assunto = "[FACOM - Sistema de Alerta de Prazos] Confirmação de usuário.";
                string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Olá " + usuario.Nome + @".</p>
                      <p>Para confirmar o cadastro do seu usuário no sistema de Alerta de Prazos da FACOM, clique no linik abaixo.</p>
                      <p><a href='" + linkConfirmacao + @"'>Clique aqui</a></p>
                       <br/>
                      <p>Caso você não esteja tentando criar uma conta no sistema de alerta de prazos, por favor desconsidere o e-mail.</p>

                        <br/><br/>
                    <img src='https://www.gov.br/participamaisbrasil/blob/ver/15491?w=0&h=0' class='media - object  img - responsive img - thumbnail' >
                               </body>
                      </html>
                     ";

                EmailClient.EnviarEmail(assunto, body, usuario.Email);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("Ativar")]
        public IActionResult Ativar(string token)
        {
            try
            {
                byte[] dadosAsBytes = System.Convert.FromBase64String(token);
                string credenciais = System.Text.ASCIIEncoding.ASCII.GetString(dadosAsBytes);
                string userId = string.Empty;
                string userEmail = string.Empty;
                if (credenciais.Split(':').Length == 2)
                {
                    userId = credenciais.Split(':')[0];
                    userEmail = credenciais.Split(':')[1];
                }
                Usuario usuario = null;
                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    usuario = db.Usuarios.Where(x => x.Id == Convert.ToInt32(userId)).ToList().FirstOrDefault();
                }

                if (usuario != null)
                    return Ok();
                else
                    throw new Exception("Token Inválido");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
