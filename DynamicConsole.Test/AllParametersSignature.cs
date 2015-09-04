namespace DynamicConsole.Tests
{
    using global::DynamicConsole.Commands.Attributes;

    public class AllParametersSignature
    {
        [CommandToken(0, "param1")]
        public string Param1 { get; set; }

        [CommandParameter("param2", Index = 1)]
        public string Param2 { get; set; }

        [CommandParameter("param3")]
        public string Param3 { get; set; }

        [CommandSwitch("param4")]
        public bool Param4 { get; set; }
    }
}