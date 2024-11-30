using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Mvc;
using AmecysAPI.Domain.Models;
using System.Net.Mail;

namespace AffiliatesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AffiliatesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AffiliatesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult SendForm([FromBody] AffiliatesForm form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var emailMessage = new MimeMessage();

                    emailMessage.From.Add(new MailboxAddress(
                        _configuration["SmtpSettings:SenderName"],
                        _configuration["SmtpSettings:SenderEmail"]
                    ));
                    emailMessage.To.Add(new MailboxAddress("Destino", "luchoocseke123@gmail.com"));
                    emailMessage.Subject = "Formulario de Afiliación";
                    emailMessage.Body = new TextPart("plain")
                    {
                        Text = $"Nombre: {form.Name}\n" +
                               $"Correo: {form.Email}\n" +
                               $"Teléfono: {form.Phone}\n" +
                               $"Tipo de Afiliación: {form.ServiceType}"
                    };

                    // MailKit
                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect(
                            _configuration["SmtpSettings:Server"],
                            int.Parse(_configuration["SmtpSettings:Port"]),
                            MailKit.Security.SecureSocketOptions.StartTls
                        );

                        client.Authenticate(
                            _configuration["SmtpSettings:Username"],
                            _configuration["SmtpSettings:Password"]
                        );

                        client.Send(emailMessage);
                        client.Disconnect(true);
                    }

                    return Ok(new { message = "Formulario enviado exitosamente." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error al enviar el correo: {ex.Message}");
                }
            }

            return BadRequest("Datos del formulario inválidos.");
        }
    }
}
