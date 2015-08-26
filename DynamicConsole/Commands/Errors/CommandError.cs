namespace DynamicConsole.Commands.Errors
{
    public class CommandError
    {
        #region Constructors

        public CommandError(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Properties

        public string Description { get; set; }

        public string Name { get; set; }

        #endregion

        public override string ToString()
        {
            return $"{this.Name}: {this.Description}";
        }
    }
}