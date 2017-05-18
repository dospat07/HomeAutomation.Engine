using HomeAutomation.Engine.CQRS;

namespace HomeAutomation.Engine.Commands
{
    public class DeleteScheduleCommand:ICommand
    {
        public int ID { get; set; }
    }
}