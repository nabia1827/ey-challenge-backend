using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Transversal.Common
{
    public static class EmailGenerator
    {
        public static string ResetPasswordConfirmationBody()
        {
            string body = @$"<!DOCTYPE html>
                            <html lang='es'>
                                <meta charset='UTF-8'>
                            <body style='text-align: center;'>
                                <h1 style='font-family: Arial, sans-serif;
                                        font-size: 38px;
                                        font-weight: 900;
                                        color: #144C63;
                                        text-transform: uppercase;'>
                                SPK
                                </h1>
                                <h1>Cambio de Contraseña Exitoso</h1>    
                                <p>Tu contraseña ha sido cambiada exitosamente.</p>
                                <p >Si no fuiste tú quien realizó este cambio, por favor, comunícate de inmediato a este correo 
                                <br/>
                                <b><a href='mailto:team.digital.krcp@kmmp.com.pe'>team.digital.krcp@kmmp.com.pe</a>.</p></b>
                            </body>
                            </html>";

            return body;
        }

        public static string SendOTPBody(string otpCode)
        {
            string body = @$"
                            <!DOCTYPE html> 
                            <html>
                                <body style='text-align: center;'>
                                <b><p>Su código de verificación es:</p></b>
                                <div style='padding: 10px 40px; 
                                            display: inline-block;
                                            border-radius: 15px;
                                            background-color: #144C63;
                                            color: white;
                                            font-weight: bold;
                                            font-size: 30px;
                                            letter-spacing: 10px;
                                '>
                                    {otpCode}
                                </div>
                                </body>
                            </html>";

            return body;
        }

        

    }
}
