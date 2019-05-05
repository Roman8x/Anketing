using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TestJob.Helpers;
using TestJob.Model;

namespace TestJob.Repository
{
    /// <summary>
    /// Сериализует объект в текст 
    /// </summary>
    // Сначала сериализуются 
    public class EntityTextFormatter<E> : IFormatter where E : Entity, new()
    {
        public SerializationBinder Binder { get; set; }
        public StreamingContext Context { get; set; }
        public ISurrogateSelector SurrogateSelector { get; set; }

        public object Deserialize(Stream serializationStream)
        {
            var entity = new E();
            var properties = PropertiesWorker.GetDisplayProperties(typeof(E));

            using (var fr = new StreamReader(serializationStream))
            {
                foreach (PropertyInfo propertyInfo in properties)
                {
                    var strvalue = fr.ReadLine().Split(": ")[1];
                    SetValue(ref entity, propertyInfo, strvalue);
                }

                fr.ReadLine();

                properties = PropertiesWorker.GetNotDisplayProperties(typeof(E));
                foreach (PropertyInfo propertyInfo in properties)
                {
                    var strvalue = fr.ReadLine().Split(": ")[1];
                    SetValue(ref entity, propertyInfo, strvalue);
                }
            }

            return entity;
        }

        private void SetValue(ref E entity, PropertyInfo propertyInfo, string strvalue)
        {
            if (propertyInfo.GetType() != typeof(DateTime))
                propertyInfo.SetValue(entity, Convert.ChangeType(strvalue, propertyInfo.PropertyType));
            else
                propertyInfo.SetValue(entity, DateTime.ParseExact(strvalue, "dd.MM.yyyy", null));
        }

        public void Serialize(Stream serializationStream, object graph)
        {
            var sw = new StreamWriter(serializationStream);
            var properties = PropertiesWorker.GetDisplayProperties(graph.GetType());
            foreach (var propertyInfo in properties)
            {
                var strvalue = propertyInfo.GetValue(graph).ToString();
                DisplayAttribute display = PropertiesWorker.GetDisplayAttribute(propertyInfo);

                sw.WriteLine($"{display.Order}. {display.Name}: {strvalue}");
            }

            sw.WriteLine();
            sw.WriteLine($"Анкета заполнена: {((Entity)graph).DateCreated.ToString("dd.MM.yyyy")}");
            sw.Flush();
        }
    }
}
