using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace StartupCentral.Models
{
    public class Log
    {
        private const string INSERT_ACTION = "Insert";
        private const string UPDATE_ACTION = "Update";
        private const string DELETE_ACTION = "Delete";

        public Log()
        {
            this.Date = DateTime.Now;
        }

        public int Id { get; set; }

        public string Action { get; set; }

        public string OriginalValues { get; set; }

        public string NewValues { get; set; }

        public DateTime Date { get; set; }

        public static Log CreateInsertLog(object newEntity)
        {
            Log log = new Log();

            log.Action = INSERT_ACTION;
            log.OriginalValues = null;
            log.NewValues = Serialize(newEntity);

            return log;
        }

        public static Log CreateDeleteLog(object newEntity)
        {
            Log log = new Log();

            log.Action = DELETE_ACTION;
            log.OriginalValues = Serialize(newEntity);
            log.NewValues = null;

            return log;
        }

        public static Log CreateUpdateLog(object originalEntity, object newEntity)
        {

            Log log = new Log();

            log.Action = UPDATE_ACTION;
            log.OriginalValues = Serialize(originalEntity);
            log.NewValues = Serialize(newEntity);

            return log;
        }

        private static string Serialize(object obj)
        {

            return SerializeJson(obj);
            //return SerializeXml(obj);
        }

        private static string SerializeXml(object obj)
        {

            XmlSerializer xs = new XmlSerializer(obj.GetType());
            using (MemoryStream buffer = new MemoryStream())
            {
                xs.Serialize(buffer, obj);
                return ASCIIEncoding.ASCII.GetString(buffer.ToArray());
            }
        }

        private static string SerializeJson(object obj)
        {

            using (MemoryStream buffer = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());
                ser.WriteObject(buffer, obj);
                return ASCIIEncoding.ASCII.GetString(buffer.ToArray());
            }
        }
    }
}