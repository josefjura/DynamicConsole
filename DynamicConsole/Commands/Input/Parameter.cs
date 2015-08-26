namespace DynamicConsole.Commands.Input
{
    using System;

    using Attributes;

    public class Parameter
    {
        #region Properties

        public int Index { get; set; }

        public bool IsNamed
        {
            get
            {
                return !string.IsNullOrEmpty(this.Name);
            }
        }

        public string Name { get; set; }

        public string Value { get; set; }

        #endregion

        public bool Conforms(CommandParameterAttribute attr)
        {
            var index = attr.Index == this.Index;
            var name = attr.Id == this.Name;
            var value = string.IsNullOrEmpty(attr.Value) || attr.Value == this.Value;

            var type = false;
            try
            {
                var typeValue = Convert.ChangeType(this.Value, attr.Type);
                type = true;
            }
            catch (Exception)
            {
            }

            return (index || name) && value && type;
        }
    }
}