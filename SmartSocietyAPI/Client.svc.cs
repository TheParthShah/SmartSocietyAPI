﻿using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace SmartSocietyAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Client : IClient
    {
        private Boolean Mail(string Email, string subject, string body)
        {
            try
            {
                MailMessage Msg = new MailMessage("riteshdlab@gmail.com", Email);
                Msg.Subject = subject;
                Msg.Body = body;
                Msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                //smtp.Host = "relay-hosting.secureserver.net";
                smtp.Port = 587;
                //smtp.Port = 25;
                smtp.UseDefaultCredentials = false;
                //smtp.EnableSsl = false;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                NetworkCredential MyCredentials = new NetworkCredential("riteshdlab@gmail.com", "Ritesh202$");
                smtp.Credentials = MyCredentials;
                smtp.Send(Msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string CheckLogin(string Username, string Password)
        {
            var DC = new DataClassesDataContext();
            var ObjLogin = (from ob in DC.tblLogins
                            where (ob.PhoneNo == Username || ob.Email == Username)
                            && ob.Password == Password
                            && ob.IsBlocked == false
                            select ob);
            if (ObjLogin.Count() == 1)
            {
                return JsonConvert.SerializeObject(ObjLogin.Single());
            }
            else
            {
                return "False";
            }
        }

        public string ForgotPassword(string Username)
        {
            var DC = new DataClassesDataContext();
            var ObjForgotPass = (from ob in DC.tblLogins
                                 where (ob.PhoneNo == Username || ob.Email == Username)
                                 && ob.IsBlocked == false
                                 select ob);
            if (ObjForgotPass.Count() == 1)
            {
                var ObjData = ObjForgotPass.Single();
                Random Number = new Random();
                string Code = Number.Next(100000, 999999).ToString();
                if (Mail(ObjData.Email, "Smart Society: Reset Password", "Verification Code: " + Code + "<br>Regards,<br>Smart Society(Society Management System)"))
                {
                    ObjData.VerificationCode = Code;
                    return "True";
                }
                else
                {
                    return "MailFalse";
                }
            }
            else
            {
                return "False";
            }
        }

        public string ResetPassword(string Username, string VerificationCode, string Password)
        {
            var DC = new DataClassesDataContext();
            var ObjReset = (from ob in DC.tblLogins
                            where (ob.PhoneNo == Username || ob.Email == Username)
                            && ob.VerificationCode == VerificationCode
                            && ob.IsBlocked == false
                            select ob);
            if (ObjReset.Count() == 1)
            {
                var ObjUser = ObjReset.Single();
                ObjUser.Password = Password;
                DC.SubmitChanges();
                return "True";
            }
            else
            {
                return "False";
            }
        }
    }
}
