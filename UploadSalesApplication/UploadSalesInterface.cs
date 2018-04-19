using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UploadSalesApplication
{
    interface UploadSalesInterface
    {
        void updateMessage(string message, bool keep);
        void workDone();
    }
}
