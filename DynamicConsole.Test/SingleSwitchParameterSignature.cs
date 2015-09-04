namespace DynamicConsole.Tests
{
    using global::DynamicConsole.Commands.Attributes;

    public class SingleSwitchParameterSignature
    {
        [CommandSwitch("param1")]
        public bool Param1 { get; set; }
    }
}