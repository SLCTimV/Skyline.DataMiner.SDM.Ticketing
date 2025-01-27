namespace Ticket_Interactive_1
{
    using System;

    using Skyline.DataMiner.SDM.Ticketing.Models;
    using Skyline.DataMiner.SDM.Ticketing.Storage;

    public class TicketPresenter
    {
        private readonly TicketView view;
        private readonly IStorageProvider<Ticket> provider;

        private Ticket model;

        public TicketPresenter(TicketView view, IStorageProvider<Ticket> provider)
        {
            this.view = view;
            this.provider = provider;

            view.SaveButton.Pressed += SaveButton_Pressed;
            view.CancelButton.Pressed += CancelButton_Pressed;
        }

        public event EventHandler<EventArgs> Cancel;

        public event EventHandler<EventArgs> Save;

        public void LoadFromModel(Ticket ticket)
        {
            view.Status.SetOptions(Enum.GetNames(typeof(TicketStatus)));
            view.Priority.SetOptions(Enum.GetNames(typeof(TicketPriority)));
            view.Severity.SetOptions(Enum.GetNames(typeof(TicketSeverity)));

            if (ticket is null)
            {
                model = new Ticket();
                return;
            }
            
            model = ticket;
            view.Guid.Text = model.Guid.ToString();
            view.ID.Text = model.ID;
            view.Name.Text = model.Name;
            view.Description.Text = model.Description;

            view.Status.Selected = String.Empty;
            view.Priority.Selected = String.Empty;
            view.Severity.Selected = String.Empty;

            view.RequestedResolutionDate.DateTime = model.RequestedResolutionDate;
            view.ExpectedResolutionDate.DateTime = model.ExpectedResolutionDate;

            view.LastModified.Text = model.LastModified.ToString("yyyy-MM-dd HH:mm:ss.fff");
            view.LastModifiedBy.Text = model.LastModifiedBy;
            view.CreatedAt.Text = model.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff");
            view.CreatedBy.Text = model.CreatedBy;
        }

        private void StoreToModel()
        {
            model.ID = view.ID.Text;
            model.Name = view.Name.Text;
            model.Description = view.Description.Text;

            model.Status = (TicketStatus)Enum.Parse(typeof(TicketStatus), view.Status.Selected);
            model.Priority = (TicketPriority)Enum.Parse(typeof(TicketPriority), view.Priority.Selected);
            model.Severity = (TicketSeverity)Enum.Parse(typeof(TicketSeverity), view.Severity.Selected);

            model.RequestedResolutionDate = view.RequestedResolutionDate.DateTime;
            model.ExpectedResolutionDate = view.ExpectedResolutionDate.DateTime;

            if (model.Guid == Guid.Empty)
            {
                provider.Create(model);
            }
            else
            {
                provider.Update(model);
            }
        }

        private void SaveButton_Pressed(object sender, EventArgs e)
        {
            StoreToModel();

            Save?.Invoke(this, EventArgs.Empty);
        }

        private void CancelButton_Pressed(object sender, EventArgs e)
        {
            Cancel?.Invoke(this, EventArgs.Empty);
        }
    }
}
