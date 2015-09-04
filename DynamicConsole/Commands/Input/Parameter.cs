namespace DynamicConsole.Commands.Input
{
    using System;

    using global::DynamicConsole.Commands.Attributes;

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

        public bool IsIndexed
        {
            get
            {
                return Index != -1;
            }
        }

        public string Name { get; set; }

        public string Value { get; set; }

        #endregion

        public bool Conforms(CommandParameterAttribute attr)
        {
            var name = this.IsNamed && attr.Id == this.Name;
            var index = this.IsIndexed && attr.Index == this.Index;
            var value = string.IsNullOrEmpty(attr.Value) || attr.Value == this.Value;

            var type = false;
            try
            {
                if (this.Value != null)
                {
                    var test = Convert.ChangeType(this.Value, attr.Type);
                }
                type = true;
            }
            catch (Exception)
            {
            }

            return (!IsNamed || name) && (!IsIndexed || index) && value && type;
        }
    }
}