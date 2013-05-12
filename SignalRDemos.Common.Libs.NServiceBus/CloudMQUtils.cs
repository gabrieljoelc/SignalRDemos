using System;
using System.Configuration;
using System.Text.RegularExpressions;

namespace SignalRDemos.Common.Libs.NServiceBus
{
    public class CloudMQUtils
    {
        const string UrlPattern = @"^(amqp|amqs):\/\/(?<username>\w*):(?<password>\w*)\@(?<host>.*)\/(?<virtualHost>\w*)$";
        const string NSBConnStrReplacement = "virtualHost=${virtualHost};username=${username};host=${host};password=${password}";

        public static string ConvertUrlToTransportConnectionString(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "host=localhost";
            }
            try
            {
                var connectionString = Regex.Replace(url, UrlPattern, NSBConnStrReplacement);
                return connectionString;

            }
            catch (Exception parseException)
            {
                throw new Exception(string.Format("Cloud AMQP URL could not be parsed into NServiceBus connection string with exception {0}", parseException.Message));
            }
        }

        public static string UseAppHarborAppSetting()
        {
            return ConvertUrlToTransportConnectionString(ConfigurationManager.AppSettings["CLOUDAMQP_URL"]);
        }
    }
}
