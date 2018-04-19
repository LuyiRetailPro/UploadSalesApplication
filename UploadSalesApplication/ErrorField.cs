using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UploadSalesApplication
{
    class ErrorField
    {
        public int row { get; set; }
        public string field_name { get; set; }
        public string field_value { get; set; }

        public ErrorField(int row, string field_name, string field_value)
        {
            this.row = row;
            this.field_value = field_value;
            this.field_name = field_name;
        }

        public override string ToString()
        {
            if (row > 0)
                return string.Format("Row {0}- Field {1} has invalid value: {2}", row, field_name, field_value);
            else
                return string.Format(field_name + ": " + field_value);
        }
    }
}
