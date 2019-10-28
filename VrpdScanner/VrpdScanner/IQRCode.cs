using System;
using System.Collections.Generic;
using System.Text;

namespace VrpdScanner
{
    public interface IQRData
    {
        DateTime Created { get; set; }

        byte[] Key { get; set; }

        string UserID { get; set; }
    }
}
