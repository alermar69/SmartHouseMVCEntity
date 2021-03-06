﻿

namespace SmartHouseDll
{
    public class ClimatConditionerCommand : ICommand
    {
        public ClimatConditionerCommand()
        {
            Name = "cl-cond";
            Inform = "Вкл/Выкл кондиционер климат-контроля в ручном режиме";
        }

        public string Name { get; private set; }
        public string Inform { get; private set; }

        public void Execute(IDataCommand dataCommand)
        {
            ClimatControl control = dataCommand.GetDevice() as ClimatControl;
            if (control != null)
            {
                control.OnOffCond();
            }
        }
        public void Undo(IDataCommand device)
        {
            Execute(device);
        }
    }
}