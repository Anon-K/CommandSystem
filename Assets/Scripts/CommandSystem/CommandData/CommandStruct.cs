using System;

namespace AnonCommandSystem
{
    /// <summary>
    /// Abstract base class for all commands
    /// </summary>
    public abstract class CommandStruct
    {
        private ParsingData parsingData;
        public event Action<ParsingData, CommandStruct> onExecute;
        /// <summary>
        /// command name,Used for prefix parsing
        /// </summary>
        public string command;
        /// <summary>
        /// describe for this command
        /// </summary>
        public string expound;
        /// <summary>
        /// Parameter list
        /// </summary>
        public string[] parameters;
        /// <summary>
        ///  analyze and execute
        /// </summary>
        /// <param name="inputCommand"></param>
        /// <returns>result</returns>
        public abstract string Execute(ParsingData data);
        public abstract void InitCommand();
        public virtual string ExecuteParsing()
        {
            var data = CommandUtil.DefaultExecute(this, parsingData);
            onExecute?.Invoke(data, this);
            return Execute(data);
        }

        /// <summary>
        /// pre input compared
        /// </summary>
        /// <param name="preInput">input string</param>
        /// <returns>string after color processing</returns>
        public virtual ReturnCommandData CompareToInput(string preInput, ExecutionTarget target = null)
        {
            var result = CommandUtil.DefaultAnalysis(this, preInput, target);
            this.parsingData = result?.parsingData;
            return result;
        }
    }

}