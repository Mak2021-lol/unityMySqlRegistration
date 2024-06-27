using System.Net.Mail;
using System;

namespace Flyen.Email
{
    public static class EmailUtility
    {
        public static bool ValidateEmail(string _email)
        {
            try
            {
                MailAddress mail = new MailAddress(_email);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
