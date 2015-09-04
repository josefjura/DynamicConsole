namespace DynamicConsole.Tests
{
    using global::DynamicConsole.Commands.Attributes;

    public class SingleTokenParameterSignature
    {
        [CommandToken(0, "param1")]
        public string Param1 { get; set; }
    }
}