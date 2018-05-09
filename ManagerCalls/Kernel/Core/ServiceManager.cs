using Kernel.Context;
using Kernel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Core
{
    /// <summary>
    /// Класс для работы с сервисом
    /// </summary>
    public class ServiceManager
    {
        private Service service;

        public ServiceManager(Service service)
        {
            this.service = service ?? throw new Exception("Параметр не может быть null");
        }

        /// <summary>
        /// Статический метод для регистрации нового сервиса
        /// </summary>
        /// <param name="newService">Принимает параметр типа Service</param>
        public static void Add(Service newService)
        {
            if (newService == null) throw new Exception("Параметр newService не может быть null");
            if (newService.Id == 0) InsertEntity.Insert(newService);
            else throw new Exception("Ошибка при инициализации типа Employee Service");
        }

       
    }
}
