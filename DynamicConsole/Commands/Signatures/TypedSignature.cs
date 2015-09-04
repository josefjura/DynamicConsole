namespace DynamicConsole.Commands.Signatures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using global::DynamicConsole.Commands.Attributes;
    using global::DynamicConsole.Commands.Errors;
    using global::DynamicConsole.Commands.Input;

    using Microsoft.Practices.ObjectBuilder2;

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

            IList<KeyValuePair<PropertyInfo, CommandParameterAttribute>> processed = new List<KeyValuePair<PropertyInfo, CommandParameterAttribute>>();

            foreach (var par in ci.Parameters)
            {
                var results = this.TypeMap.Where(x => par.Conforms(x.Value)).ToList();

                if (!results.Any())
                {
                    throw new UnidentifiedParameterException(par);
                }

                if (results.Count > 1)
                {
                    throw new AmbiguousParameterException(results);
                }

                var result = results.Single();

                result.Value.Process(instance, result.Key, par);

                processed.Add(result);
            }

            var unprocessedMandatory = TypeMap.Except(processed).Where(x => x.Value.IsMandatory).Select(x => x.Value).ToList();

            if (unprocessedMandatory.Any())
            {
                throw new MissingParametersException(unprocessedMandatory);
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

            IList<KeyValuePair<PropertyInfo, CommandParameterAttribute>> processed = new List<KeyValuePair<PropertyInfo, CommandParameterAttribute>>();

            foreach (var par in ci.Parameters)
            {
                var results = this.TypeMap.Where(x => par.Conforms(x.Value)).ToList();

                if (!results.Any())
                {
                    return false;
                }

                if (results.Count > 1)
                {
                    throw new AmbiguousParameterException(results);
                }

                var result = results.Single();


                processed.Add(result);
            }

            if (TypeMap.Except(processed).Any(x => x.Value.IsMandatory)) return false;

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

    public class MissingParametersException : Exception
    {
        private readonly List<CommandParameterAttribute> _unprocessedMandatory;

        public MissingParametersException(List<CommandParameterAttribute> unprocessedMandatory) : base()
        {
            this._unprocessedMandatory = unprocessedMandatory;
        }
    }

    public class AmbiguousParameterException : Exception
    {
        public AmbiguousParameterException(List<KeyValuePair<PropertyInfo, CommandParameterAttribute>> result) : base()
        {

        }
    }

    public class UnidentifiedParameterException : Exception
    {
        private readonly Parameter _par;

        public UnidentifiedParameterException(Parameter par) : base("Parameter couldn\'t be identified")
        {
            this._par = par;
        }
    }
}