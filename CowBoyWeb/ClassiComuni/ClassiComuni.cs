
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace CowBoyWeb
{
    public class ClassiComuni
    {
        private string _connectStringUniversal;

        public string ConnectCbUniversal
        {
            get
            {
                ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["CBConnect"];
                if (string.IsNullOrEmpty(mySetting?.ConnectionString))
                {
                    _connectStringUniversal = null;
                }

                if (mySetting != null) _connectStringUniversal = mySetting.ConnectionString;
                else //creo erroe
                    throw new System.ArgumentException("Stringa di connessione non trovata DB DBP", ConnectCbUniversal);

                return _connectStringUniversal;
            }
            set => _connectStringUniversal = value;
        }

        //public string TrovaPercorsoFotoUtente(string utenteFoto)

        //{
        //    try
        //    {
        //        var percorsof = PercorsoFotoUtente; // PERCORSO PREDEFINITO PER LE IMMAGINI
        //        string pathfile = Path.Combine(percorsof, utenteFoto.Trim()); // PERCORSO E NOME DEL FILE DI IMMAGINE

        //        if (!(File.Exists(pathfile)))  // VERIFICA SE ESISTE LA FOTO  }
        //        {
        //            pathfile = Path.Combine(percorsof, "NessunaF.JPG"); // LA REDEFINISCE CON UNA CERTAMENTE ESISTENTE
        //        }
        //        return pathfile;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
       

        private bool ValidateEmails(string emails)
        {
            var res = emails.Split(';');
            return res.All(IsValidEmail);
        }

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                string matchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                           + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                           + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

                if (!string.IsNullOrEmpty(emailaddress)) return Regex.IsMatch(emailaddress, matchEmailPattern);
                else return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }


    }

   
}