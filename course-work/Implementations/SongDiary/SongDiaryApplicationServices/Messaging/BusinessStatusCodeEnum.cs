using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongDiaryApplicationServices.Messaging
{
    public enum BusinessStatusCodeEnum
    {
        None,
        Success,
        MissingObject,
        InternalServerError,
    }
}
