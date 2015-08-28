using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicConsole.Commands.Attributes
{
    public class CommandTokenAttribute : CommandParameterAttribute
    {
        public CommandTokenAttribute(int index, string value)
            : base("token", TypeCode.String)
        {
            this.Value = value;
            this.Index = index;
        }
    }
}
