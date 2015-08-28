namespace DynamicConsole.Commands.Signatures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using global::DynamicConsole.Commands.Attributes;
    using global::DynamicConsole.Commands.Errors;
    using global::DynamicConsole.Commands.Input;

    using RandomR.Main;

    public class TypedSignature<TData> : CommandSignature
        where TData : class, new()
    {
        public delegate bool CommandSignatureHitCallbackGeneric<in TOut>(TOut data, IList<CommandError> errors);

        #region Fields

        private readonly CommandSignatureHitCallbackGeneric<TData> _callback;

        #endregion

        #region Constructors

        public TypedSignature(string keyword, string description, CommandSignatureHitCallbackGeneric<TData> callback)
            : base(keyword, description)
        {
            this._callback = callback;
            this.TypeMap = this.GetTypeMap();
        }

        #endregion

        #region Properties

        public Dictionary<PropertyInfo, CommandParameterAttribute> TypeMap { get; set; }

        #endregion

        public override bool Run(CommandInput ci, IList<CommandError> errors)
        {
            return this._callback?.Invoke(this.ParseType(ci), errors) ?? false;
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
            if (ci.Keyword != Keyword)
            {
                return false;
            }

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

        public override CommandInput GenerateRandomInput(string keyword)
        {
            var toReturn = new CommandInput();

            toReturn.Keyword = keyword;

            var i = 0;
            foreach (var item in TypeMap)
            {
                var value = string.IsNullOrEmpty(item.Value.Value)
                                ? RandomResolver.GetRandomValue(item.Key.PropertyType).ToString()
                                : item.Value.Value;

                toReturn.Parameters.Add(new Parameter { Value = value, Index = i++ });
            }

            return toReturn;
        }
    }
}