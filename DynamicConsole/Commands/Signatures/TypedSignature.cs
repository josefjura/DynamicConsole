namespace DynamicConsole.Commands.Signatures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Attributes;
    using Errors;
    using Input;
    using IO.Base;

    public class TypedSignature<TData> : CommandSignature
        where TData : class, new()
    {
        public delegate bool CommandSignatureHitCallbackGeneric<in TOut>(
            TOut data,
            IOutput output,
            IList<CommandError> errors);

        #region Fields

        private readonly CommandSignatureHitCallbackGeneric<TData> _callback;

        #endregion

        #region Constructors

        public TypedSignature(string description, CommandSignatureHitCallbackGeneric<TData> callback)
            : base(description)
        {
            this._callback = callback;
            this.TypeMap = this.GetTypeMap();
        }

        #endregion

        #region Properties

        public Dictionary<PropertyInfo, CommandParameterAttribute> TypeMap { get; set; }

        #endregion

        public override bool Run(CommandInput ci, IOutput output, IList<CommandError> errors)
        {
            return this._callback?.Invoke(this.ParseType(ci), output, errors) ?? false;
        }

        private TData ParseType(CommandInput ci)
        {
            var instance = new TData();

            foreach (var p in this.TypeMap)
            {
                var param = ci.Parameters.SingleOrDefault(x => x.Name == p.Value.Id || x.Index == p.Value.Index);
                if (param == null)
                {
                    throw new ApplicationException("Parameters are out of sync with data class");
                }

                p.Key.SetValue(instance, Convert.ChangeType(param.Value, p.Value.Type));
            }

            return instance;
        }

        private Dictionary<PropertyInfo, CommandParameterAttribute> GetTypeMap()
        {
            var toReturn = new Dictionary<PropertyInfo, CommandParameterAttribute>();

            var props = from p in typeof(TData).GetProperties()
                        let attr = p.GetCustomAttributes(typeof(CommandParameterAttribute), true)
                        where attr.Length == 1
                        select new { Property = p, Attribute = attr.First() as CommandParameterAttribute };
            foreach (var prop in props)
            {
                toReturn[prop.Property] = prop.Attribute;
            }

            return toReturn;
        }

        public override bool CanRun(CommandInput ci)
        {
            foreach (var p in this.TypeMap)
            {
                var result = ci.Parameters.Any(x => x.Conforms(p.Value));

                if (!result)
                {
                    return false;
                }
            }

            return true;
        }

        public override string GetHelp()
        {
            var sb = new StringBuilder();

            var parameters = this.TypeMap.OrderBy(x => x.Value.Index);

            foreach (var par in parameters)
            {
                if (string.IsNullOrWhiteSpace(par.Value.Value))
                {
                    sb.Append($" <{par.Value.Id}:{par.Value.Type}>");
                }
                else
                {
                    sb.Append($" \"{par.Value.Value}\"");
                }
            }

            return sb.ToString();
        }
    }
}