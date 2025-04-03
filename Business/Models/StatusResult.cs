using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models;

public class StatusResult<T> : ServiceResult
{
    

    public T? Result { get; set; }
}

public class StatusResult: ServiceResult
{

}
