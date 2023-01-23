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

        private IConfiguration _configuration;
        public UsuarioController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

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
        [Route("Autenticar")]
        public IActionResult Autenticar(Usuario usuario)
        {
            //Autenticação utilizada para logar no website do Sistema de alerta de prazos.
            try
            {
                Usuario user = null;
                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    user = db.Usuarios.Where(x => x.Email == usuario.Email && x.PasswordHash == usuario.PasswordHash).ToList().FirstOrDefault();
                }

                if(user != null)
                {
                    user.PasswordHash = String.Empty;
                    return Ok(user);
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
            //Cria usuário para o sistema de alerta de prazos.
            //Envia e-mail de ativação para caixa de e-mail do novo usuário.
            
            try
            {
                
                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    if (db.Usuarios.Where(x => x.Email == usuario.Email).ToList().Count > 0)
                        return StatusCode(StatusCodes.Status422UnprocessableEntity, "Este e-mail já está sendo usado.");

                    usuario.PasswordHash = "";//A senha será criada após a ativação.
                    usuario.IsActive = false;//Usuário é criado inativo até chegar a requisição de ativação.
                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                }

                //Token
                //Será enviado no link para ativação enviado para a caixa de e-mail do novo usuário.
                //Contém informações necessárias para identificar o usuário na hora da ativação.
                byte[] textoAsBytes = Encoding.ASCII.GetBytes($"{usuario.Id}:{usuario.Nome}:{usuario.Email}");
                string token = System.Convert.ToBase64String(textoAsBytes);
                string linkConfirmacao = _configuration["Configuracoes:UrlFrontEnd"];
                linkConfirmacao += $"/Usuario/Ativar/{token}";

                //Enviar e-mail.
                string assunto = "[FACOM - Sistema de Alerta de Prazos] Confirmação de usuário.";
                string body = @"<html>
                      <body style='font-size: 1.8rem'>
                      <p style='font-size: 2rem'>Olá " + usuario.Nome.ToUpper() + @".</p>
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

        [HttpPost]
        [Route("Editar")]
        public IActionResult Editar(Usuario usuario)
        {
            //Recebe usuário com o hash da nova senha
            //Edita usuário adicionando nova senha e flag de ativação.
            try
            {

                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    Usuario user = db.Usuarios.Where(x => x.Id == usuario.Id).ToList().FirstOrDefault();

                    if (user == null)
                        throw new Exception("Usuário não encontrado.");

                    if(user.IsActive == true)
                        throw new Exception("Usuário já possui senha.");

                    user.PasswordHash = usuario.PasswordHash;
                    user.IsActive = usuario.IsActive;
                    db.Usuarios.Update(user);
                    db.SaveChanges();
                }

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
            //Descifra o token para pegar informações para identificar um usuário
            //Verifica se é um token válido

            try
            {
                byte[] dadosAsBytes = System.Convert.FromBase64String(token);
                string credenciais = System.Text.ASCIIEncoding.ASCII.GetString(dadosAsBytes);
                string userId = string.Empty;
                string userEmail = string.Empty;
                string userNome = string.Empty;
                if (credenciais.Split(':').Length == 3)
                {
                    userId = credenciais.Split(':')[0];
                    userNome = credenciais.Split(':')[1];
                    userEmail = credenciais.Split(':')[2];
                }
                Usuario usuario = new Usuario() { Id = Convert.ToInt32(userId), Nome = userNome, Email = userEmail};

                usuario.PasswordHash = "password";
                usuario.IsActive = true;

                using (var db = new SistemaDeAlertaDePrazosContext())
                {
                    usuario = db.Usuarios.Where(x => x.Id == Convert.ToInt32(userId) && x.Email == usuario.Email && x.Nome.ToLower() == usuario.Nome.ToLower()).ToList().FirstOrDefault();
                }

                if (usuario != null)
                    return Ok(usuario);
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
