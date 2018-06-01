using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisMeteoLibrary.Core.Abstract
{
    interface IResult<T> 
    {
        List<T> GetResult();
    }
}
