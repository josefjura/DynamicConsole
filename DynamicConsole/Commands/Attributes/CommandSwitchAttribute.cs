using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicConsole.Commands.Attributes
{
    using System.Reflection;

    using global::DynamicConsole.Commands.Exceptions;
    using global::DynamicConsole.Commands.Input;

    [AttributeUsage(AttributeTargets.Property)]
    public class CommandSwitchAttribute : CommandParameterAttribute
    {
        public CommandSwitchAttribute(string id) : base(id, TypeCode.Boolean)
        {
            this.IsMandatory = false;
        }

        public override void Process<T>(T instance, PropertyInfo key, Parameter par)
        {
            if (key.PropertyType != typeof(bool)) throw new CommandSwitchNotBoolException();
            key.SetValue(instance, true);
        }
    }
}
