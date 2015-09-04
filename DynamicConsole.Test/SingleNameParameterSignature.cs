namespace DynamicConsole.Tests
{
    using global::DynamicConsole.Commands.Attributes;

    public class SingleNameParameterSignature
    {
        [CommandParameter("param1")]
        public string Param1 { get; set; }
    }
}