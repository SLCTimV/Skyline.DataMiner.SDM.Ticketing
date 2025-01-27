/*
****************************************************************************
*  Copyright (c) 2025,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

15/01/2025	1.0.0.1		AMA, Skyline	Initial version
****************************************************************************
*/

namespace Ticket_Interactive_1
{
	using System;
    using System.Collections.Generic;
    using System.Linq;

    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.SDM.Ticketing.Exposers;
    using Skyline.DataMiner.SDM.Ticketing.Models;
    using Skyline.DataMiner.SDM.Ticketing.Storage;
    using Skyline.DataMiner.Utils.InteractiveAutomationScript;

    /// <summary>
    /// Represents a DataMiner Automation script.
    /// engine.ShowUI();
    /// </summary>
    public class Script
	{
		/// <summary>
		/// The script entry point.
		/// </summary>
		/// <param name="engine">Link with SLAutomation process.</param>
		public void Run(IEngine engine)
		{
			try
			{
				RunSafe(engine);
			}
			catch (ScriptAbortException)
			{
				// Catch normal abort exceptions (engine.ExitFail or engine.ExitSuccess)
				throw; // Comment if it should be treated as a normal exit of the script.
			}
			catch (ScriptForceAbortException)
			{
				// Catch forced abort exceptions, caused via external maintenance messages.
				throw;
			}
			catch (ScriptTimeoutException)
			{
				// Catch timeout exceptions for when a script has been running for too long.
				throw;
			}
			catch (InteractiveUserDetachedException)
			{
				// Catch a user detaching from the interactive script by closing the window.
				// Only applicable for interactive scripts, can be removed for non-interactive scripts.
				throw;
			}
			catch (Exception e)
			{
				engine.ExitFail("Run|Something went wrong: " + e);
			}
		}

		private void RunSafe(IEngine engine)
		{
            var controller = new InteractiveController(engine);
            var context = new ScriptContext(engine);
            var provider = new TicketStorageProvider(engine.GetUserConnection());
            var view = new TicketView(engine);
            var presenter = new TicketPresenter(view, provider);

            presenter.Cancel += (sender, e) => engine.ExitSuccess("User canceled");
            presenter.Save += (sender, e) => engine.ExitSuccess("Saved ticket");

            var ticket = LoadTicket(context, provider);
            presenter.LoadFromModel(ticket);

            controller.Run(view);
		}

        private Ticket LoadTicket(ScriptContext context, IStorageProvider<Ticket> provider)
        {
            if (String.IsNullOrEmpty(context.TicketGuid) || !Guid.TryParse(context.TicketGuid, out var ticketGuid))
            {
                return new Ticket();
            }

            var ticket = provider.Read(TicketExposers.Guid.Equal(ticketGuid)).FirstOrDefault();
            if (ticket == default)
            {
                throw new KeyNotFoundException($"Could not find ticket with ID: '{context.TicketGuid}'");
            }

            return ticket;
        }
	}
}