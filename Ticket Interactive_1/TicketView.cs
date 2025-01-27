namespace Ticket_Interactive_1
{
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    public class TicketView : Dialog
    {
        public TicketView(IEngine engine) : base(engine)
        {
            this.Title = "Ticket";
            AddWidget(new Label("Guid"), 0, 0);
            AddWidget(Guid, 0, 1);
            AddWidget(new Label("ID"), 1, 0);
            AddWidget(ID, 1, 1);
            AddWidget(new Label("Name"), 2, 0);
            AddWidget(Name, 2, 1);
            AddWidget(new Label("Description"), 3, 0);
            AddWidget(Description, 3, 1);
            AddWidget(new Label("Type"), 4, 0);
            AddWidget(Type, 4, 1);
            AddWidget(new Label("Status"), 5, 0);
            AddWidget(Status, 5, 1);
            AddWidget(new Label("Priority"), 6, 0);
            AddWidget(Priority, 6, 1);
            AddWidget(new Label("Severity"), 7, 0);
            AddWidget(Severity, 7, 1);
            AddWidget(new Label("RequestedResolutionDate"), 8, 0);
            AddWidget(RequestedResolutionDate, 8, 1);
            AddWidget(new Label("ExpectedResolutionDate"), 9, 0);
            AddWidget(ExpectedResolutionDate, 9, 1);
            AddWidget(new Label("LastModified"), 10, 0);
            AddWidget(LastModified, 10, 1);
            AddWidget(new Label("LastModifiedBy"), 11, 0);
            AddWidget(LastModifiedBy, 11, 1);
            AddWidget(new Label("CreatedAt"), 12, 0);
            AddWidget(CreatedAt, 12, 1);
            AddWidget(new Label("CreatedBy"), 13, 0);
            AddWidget(CreatedBy, 13, 1);
            AddWidget(CancelButton, 14, 0);
            AddWidget(SaveButton, 14, 1);
        }

        public TextBox Guid { get; } = new TextBox() { IsEnabled = false };

        public TextBox ID { get; } = new TextBox() { IsEnabled = false };

        public TextBox Name { get; } = new TextBox();

        public TextBox Description { get; } = new TextBox();

        public Numeric Enabled { get; } = new Numeric();

        public DropDown Type { get; } = new DropDown { IsSorted = true, IsDisplayFilterShown = true };

        public DropDown Status { get; } = new DropDown { IsSorted = true, IsDisplayFilterShown = true };

        public DropDown Priority { get; } = new DropDown { IsSorted = true, IsDisplayFilterShown = true };

        public DropDown Severity { get; } = new DropDown { IsSorted = true, IsDisplayFilterShown = true };

        public DateTimePicker RequestedResolutionDate { get; } = new DateTimePicker();

        public DateTimePicker ExpectedResolutionDate { get; } = new DateTimePicker();

        public TextBox LastModified { get; } = new TextBox() { IsEnabled = false };

        public TextBox LastModifiedBy { get; } = new TextBox() { IsEnabled = false };

        public TextBox CreatedAt { get; } = new TextBox() { IsEnabled = false };

        public TextBox CreatedBy { get; } = new TextBox() { IsEnabled = false };

        public Button CancelButton { get; } = new Button("Cancel");

        public Button SaveButton { get; } = new Button("Save");
    }
}
