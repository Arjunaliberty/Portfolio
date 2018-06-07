using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Core.Abstract
{
    /// <summary>
    /// Описывает методы для работы с ресурсом
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResource
    {
        string Load();
    }
}
