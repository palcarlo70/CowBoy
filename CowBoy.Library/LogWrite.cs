//using System.ServiceModel;
using System;
using System.Configuration;
using System.IO;

namespace CowBoy.Library
{
    public class LogWrite
    {
        public static void ScriviLog(string message, string stackTrace) //string percorso,
        {
            try
            {
                var dominio = ConfigurationManager.AppSettings["Domain"];
                var utente = ConfigurationManager.AppSettings["User"];
                var pass = ConfigurationManager.AppSettings["Password"];
                var percorso = ConfigurationManager.AppSettings["PercorsoLogError"];


                using (new ImpersonateUser(utente, dominio, pass))
                {
                    var percFile = Path.Combine(percorso, string.Format("Log{0:yyyyHHdd}.txt", DateTime.Now));
                    if (!Directory.Exists(percorso)) //verifica dell'esistenza del percorso 
                    {
                        Directory.CreateDirectory(percorso);
                    }

                    if (!File.Exists(percFile)) //verifica dell'esistenza del file di log giornaliero
                    {
                        using (StreamWriter sw = File.CreateText(percFile))
                        {
                            sw.WriteLine(string.Format("{0:dd-MM-yy HH:mm:ss}, {1}, {2}", DateTime.Now, stackTrace,
                                message));
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(percFile))
                        {
                            sw.WriteLine(string.Format("{0:dd-MM-yy HH:mm:ss}, {1}, {2}", DateTime.Now, stackTrace,
                                message));
                        }
                    }
                }
            }
            catch (Exception )
            {
                // ScriptAlert.GetAlert(null, ex.Message);
            }
        }

    }
}
