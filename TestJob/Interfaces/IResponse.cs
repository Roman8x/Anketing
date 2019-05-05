using System;
using System.Collections.Generic;
using System.Text;
using TestJob.Model;

namespace TestJob.Interfaces
{
    /// <summary>
    /// Ответ на команду
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Ответ на команду может содержать профиль
        /// </summary>
        Profile Bag { get;  }
        ResponseType ResponseType { get; } 
    }


    /// <summary>
    /// Тип ответа
    /// </summary>
    public enum ResponseType {
        /// <summary>
        /// Ok 200
        /// </summary>
        Ok = 0 ,
        /// <summary>
        /// Ответ содержит сущности
        /// </summary>
        Entity =1,
        /// <summary>
        /// Не указана параметры команды ( или не верные  параметры )
        /// </summary>
        BadParameters = 2,
        NoSessionContent = 3
    }
}
