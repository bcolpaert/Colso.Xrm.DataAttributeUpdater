using Colso.Xrm.DataAttributeUpdater.AppCode;
using Colso.Xrm.DataAttributeUpdater.Forms;
using Colso.Xrm.DataAttributeUpdater.Models;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace Colso.Xrm.DataAttributeUpdater
{
    public partial class DataUpdater : PluginControlBase, IXrmToolBoxPluginControl, IGitHubPlugin, IHelpPlugin, IStatusBarMessenger, IPayPalPlugin
    {
        #region Variables

        /// <summary>
        /// Information panel
        /// </summary>
        private Panel informationPanel;

        /// <summary>
        /// Dynamics CRM 2011 organization service
        /// </summary>
        private IOrganizationService service;

        private bool workingstate = false;
        private Guid organisationid;
        private Settings settings;

        // keep list of listview items 
        List<ListViewItem> Entities = new List<ListViewItem>();

        private delegate void AddAttributeDelegate(params object[] values);

        #endregion Variables

        public DataUpdater()
        {
            SettingFileHandler.GetConfigData(out settings);
            InitializeComponent();
        }

        #region XrmToolbox

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public Image PluginLogo
        {
            get { return null; }
        }

        public string HelpUrl
        {
            get
            {
                return "https://github.com/MscrmTools/Colso.Xrm.DataAttributeUpdater/wiki";
            }
        }

        public string RepositoryName
        {
            get
            {
                return "Colso.Xrm.DataAttributeUpdater";
            }
        }

        public string UserName
        {
            get
            {
                return "MscrmTools";
            }
        }

        public string DonationDescription
        {
            get
            {
                return "Donation for Data Attribute Updater Tool - XrmToolBox";
            }
        }

        public string EmailAccount
        {
            get
            {
                return "bramcolpaert@outlook.com";
            }
        }

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail connectionDetail, string actionName = "", object parameter = null)
        {
            organisationid = connectionDetail.ConnectionId.Value;
            service = newService;
            // init buttons -> value based on connection
            InitFilter();
            // Save settings file
            SettingFileHandler.SaveConfigData(settings);
            // Load entities when source connection changes
            PopulateEntities();
        }

        public string GetCompany()
        {
            return GetType().GetCompany();
        }

        public string GetMyType()
        {
            return GetType().FullName;
        }

        public string GetVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }

        #endregion XrmToolbox

        #region Form events

        private void tsbCloseThisTab_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbRefreshEntities_Click(object sender, EventArgs e)
        {
            PopulateEntities();
        }

        private void tsbTransferData_Click(object sender, EventArgs e)
        {
            ExecuteAction(false);
        }

        private void btnPreviewTransfer_Click(object sender, EventArgs e)
        {
            ExecuteAction(true);
        }

        private void lvEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAttributes();
        }

        private void lvEntities_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SetListViewSorting(lvEntities, e.Column);
        }

        private void grdAttributes_ColumnClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //SetListViewSorting(grdAttributes, e.ColumnIndex);
        }

        private void grdAttributes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (0 <= e.ColumnIndex && e.ColumnIndex < grdAttributes.Columns.Count && 0 <= e.RowIndex && e.RowIndex < grdAttributes.Rows.Count)
            {
                var col = grdAttributes.Columns[e.ColumnIndex];
                var row = grdAttributes.Rows[e.RowIndex];

                switch (col.Name)
                {
                    case "chkUpdate":
                        // Update label
                        var selectcount = grdAttributes.Rows.Cast<DataGridViewRow>().Where(r => (bool)r.Cells[0].Value == true).Count();
                        lblNrSelected.Text = string.Format("{0} selected", selectcount);
                        break;
                    case "attValue":
                        var value = (string)row.Cells[4].Value;
                        if (!string.IsNullOrEmpty(value)) row.Cells[0].Value = true;
                        break;
                }
            }
        }

        private void grdAttributes_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (0 <= e.ColumnIndex && e.ColumnIndex < grdAttributes.Columns.Count && 0 <= e.RowIndex && e.RowIndex < grdAttributes.Rows.Count)
            {
                var col = grdAttributes.Columns[e.ColumnIndex];
                var row = grdAttributes.Rows[e.RowIndex];

                if (col.Name == "attValue")
                {
                    var type = (string)row.Cells[3].Value;
                    var value = (string)e.FormattedValue;
                    string format = null;
                    if (!ValidateValue(type, value, out format))
                    {
                        row.ErrorText = string.Format("'{0}' is not a valid value for the type {1}. Expected format: '{2}'", value, type, format);
                        e.Cancel = true;
                    }
                }
            }
        }

        private void txtEntityFilter_TextChanged(object sender, EventArgs e)
        {
            SetListViewFilter(lvEntities, Entities, txtEntityFilter.Text);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                var entityitem = lvEntities.SelectedItems[0];

                if (entityitem != null && entityitem.Tag != null)
                {
                    var filter = string.Empty;
                    var entity = (EntityMetadata)entityitem.Tag;
                    var filterDialog = new FilterEditor(filter = settings[entity.LogicalName].Filter);
                    filterDialog.ShowDialog(ParentForm);
                    settings[entity.LogicalName].Filter = filterDialog.Filter;
                    InitFilter();
                }
            }
        }

        #endregion Form events

        #region Methods

        private void InitFilter()
        {
            string filter = null;

            if (lvEntities.SelectedItems.Count > 0)
            {
                var entityitem = lvEntities.SelectedItems[0];

                if (entityitem != null && entityitem.Tag != null)
                {
                    var entity = (EntityMetadata)entityitem.Tag;
                    filter = settings[entity.LogicalName].Filter;
                }
            }

            btnFilter.ForeColor = string.IsNullOrEmpty(filter) ? Color.Black : Color.Blue;
        }

        private void ManageWorkingState(bool working)
        {
            workingstate = working;
            Cursor = working ? Cursors.WaitCursor : Cursors.Default;
            // Set controls
            lvEntities.Enabled = !working;
            //grdAttributes.Enabled = !working;
        }

        private bool CheckConnection()
        {
            if (service == null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "Load", Control = this };
                RaiseRequestConnectionEvent(args);


                return false;
            }
            else
            {
                return true;
            }
        }

        private void PopulateEntities()
        {
            if (!CheckConnection())
                return;

            if (!workingstate)
            {
                // Reinit other controls
                grdAttributes.Rows.Clear();
                ManageWorkingState(true);

                informationPanel = InformationPanel.GetInformationPanel(this, "Loading entities...", 340, 150);

                // Launch treatment
                var bwFill = new BackgroundWorker();
                bwFill.DoWork += (sender, e) =>
                {
                    // Retrieve 
                    List<EntityMetadata> sourceList = MetadataHelper.RetrieveEntities(service);

                    // Prepare list of items
                    Entities.Clear();

                    foreach (EntityMetadata entity in sourceList)
                    {
                        var name = entity.DisplayName.UserLocalizedLabel == null ? string.Empty : entity.DisplayName.UserLocalizedLabel.Label;
                        var item = new ListViewItem(name);
                        item.Tag = entity;
                        item.SubItems.Add(entity.LogicalName);

                        if (!entity.IsCustomizable.Value)
                        {
                            item.ForeColor = Color.Gray;
                            item.SubItems.Add("This entity has not been defined as customizable");
                        }

                        Entities.Add(item);
                    }

                    e.Result = Entities;
                };
                bwFill.RunWorkerCompleted += (sender, e) =>
                {
                    informationPanel.Dispose();

                    if (e.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                    else
                    {
                        var items = (List<ListViewItem>)e.Result;
                        if (items.Count == 0)
                            MessageBox.Show(this, "The system does not contain any entities", "Warning", MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        else
                            SetListViewFilter(lvEntities, items, txtEntityFilter.Text);
                    }

                    ManageWorkingState(false);
                };
                bwFill.RunWorkerAsync();
            }
        }

        private void PopulateAttributes()
        {
            if (!workingstate)
            {
                // Reinit other controls
                grdAttributes.Rows.Clear();
                InitFilter();

                if (lvEntities.SelectedItems.Count > 0)
                {
                    var entityitem = lvEntities.SelectedItems[0];

                    if (entityitem != null && entityitem.Tag != null)
                    {
                        ManageWorkingState(true);

                        var entity = (EntityMetadata)entityitem.Tag;

                        informationPanel = InformationPanel.GetInformationPanel(this, "Loading attributes...", 340, 150);

                        // Launch treatment
                        var bwFill = new BackgroundWorker();
                        bwFill.DoWork += (sender, e) =>
                        {
                            // Retrieve 
                            var entitymeta = MetadataHelper.RetrieveEntity(entity.LogicalName, service);

                            // Get attribute checked settings
                            var attributevalues = settings[entity.LogicalName].AttributeValues;

                            // Prepare list of items
                            //var sourceAttributesList = new List<DataGridViewRow>();

                            // Only use create/editable attributes && properties which are valid for read
                            var attributes = entitymeta.Attributes
                                .Where(a => (a.IsValidForCreate != null && a.IsValidForCreate.Value) || (a.IsValidForUpdate != null && a.IsValidForUpdate.Value))
                                .Where(a => a.IsValidForRead != null && a.IsValidForRead.Value)
                                .Where(a => a.DisplayName.UserLocalizedLabel != null)
                                .OrderBy(a => a.DisplayName.UserLocalizedLabel.Label)
                                .ToArray();

                            this.grdAttributes.NotifyCurrentCellDirty(true);
                            grdAttributes.BeginEdit(false);
                            foreach (AttributeMetadata attribute in attributes)
                            {
                                // Skip "statecode", "statuscode" (should be updated via SetStateRequest)/ "ownerid" via assign
                                if (!attribute.LogicalName.Equals("statecode")
                                && !attribute.LogicalName.Equals("statuscode")
                                && !attribute.LogicalName.Equals("ownerid"))
                                {
                                    // Only add attributes which are valid for update
                                    if (attribute.IsValidForUpdate != null && attribute.IsValidForUpdate.Value)
                                    {
                                        var name = attribute.DisplayName.UserLocalizedLabel.Label;
                                        var logicalname = attribute.LogicalName;
                                        var typename = attribute.AttributeTypeName == null ? string.Empty : attribute.AttributeTypeName.Value;
                                        if (typename.EndsWith("Type")) typename = typename.Substring(0, typename.LastIndexOf("Type"));

                                        bool update = false;
                                        string value = null;
                                        // Set stored value
                                        if (attributevalues.ContainsKey(attribute.LogicalName))
                                        {
                                            update = true;
                                            value = attributevalues[attribute.LogicalName];
                                        }

                                        //var row = new DataGridViewRow();
                                        //row.SetValues(update, name, logicalname, typename, value);
                                        //sourceAttributesList.Add(row);
                                        this.BeginInvoke(
                                            new AddAttributeDelegate(AddAttributeToGrid),
                                            new object[] {
                                                new object[] { update, name, logicalname, typename, value }
                                            });
                                    }
                                }
                            }

                            //e.Result = sourceAttributesList;
                        };
                        bwFill.RunWorkerCompleted += (sender, e) =>
                        {
                            informationPanel.Dispose();

                            if (e.Error != null)
                            {
                                MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                            }
                            else
                            {
                                //var items = (List<DataGridViewRow>)e.Result;
                                //if (items.Count == 0)
                                if (grdAttributes.Rows.Count == 0)
                                {
                                    MessageBox.Show(this, "The entity does not contain any attributes", "Warning", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);
                                }
                                //grdAttributes.Sort(grdAttributes.Columns[1], ListSortDirection.Ascending);
                                grdAttributes.EndEdit();
                                //else
                                //{
                                //    grdAttributes.Rows.AddRange(items.ToArray());
                                //}
                            }

                            ManageWorkingState(false);
                        };
                        bwFill.RunWorkerAsync();
                    }
                }
            }
        }

        private void ExecuteAction(bool preview)
        {
            if (service == null)
            {
                MessageBox.Show("There is no active connection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UpdateAttributes(preview);
        }

        private void UpdateAttributes(bool preview)
        {
            if (lvEntities.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select at least one entity to be updated", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Good time to save the attributes
            ManageWorkingState(true);

            informationPanel = InformationPanel.GetInformationPanel(this, "Updating records...", 340, 150);
            SendMessageToStatusBar(this, new StatusBarMessageEventArgs("Start updating records..."));

            var bwTransferData = new BackgroundWorker { WorkerReportsProgress = true };
            bwTransferData.DoWork += (sender, e) =>
            {
                var worker = (BackgroundWorker)sender;
                var entities = (List<EntityMetadata>)e.Argument;
                var errors = new List<Item<string, string>>();

                for (int i = 0; i < entities.Count; i++)
                {
                    var entitymeta = entities[i];
                    var attributes = grdAttributes.Rows
                        .Cast<DataGridViewRow>()
                        .Where(v => (bool)v.Cells[0].Value == true)
                        .Select(v => new KeyValuePair<string, object>((string)v.Cells[2].Value, ParseValue((string)v.Cells[3].Value, (string)v.Cells[4].Value)))
                        .ToList();
                    var entity = new AppCode.EntityRecord(entitymeta, attributes, service);
                    worker.ReportProgress((i / entities.Count), string.Format("{1} entity '{0}'...", entity.Name, (preview ? "Previewing" : "Updating")));

                    try
                    {
                        entity.Filter = settings[entitymeta.LogicalName].Filter;
                        entity.OnStatusMessage += Transfer_OnStatusMessage;
                        entity.Update();
                        errors.AddRange(entity.Messages.Select(m => new Item<string, string>(entity.Name, m)));
                    }
                    catch (FaultException<OrganizationServiceFault> error)
                    {
                        errors.Add(new Item<string, string>(entity.Name, error.Message));
                    }
                }

                e.Result = errors;
            };
            bwTransferData.RunWorkerCompleted += (sender, e) =>
            {
                Controls.Remove(informationPanel);
                informationPanel.Dispose();
                //SendMessageToStatusBar(this, new StatusBarMessageEventArgs(string.Empty)); // keep showing transfer results afterwards
                ManageWorkingState(false);

                var errors = (List<Item<string, string>>)e.Result;

                if (errors.Count > 0)
                {
                    var errorDialog = new ErrorList((List<Item<string, string>>)e.Result);
                    errorDialog.ShowDialog(ParentForm);
                }
            };
            bwTransferData.ProgressChanged += (sender, e) =>
            {
                InformationPanel.ChangeInformationPanelMessage(informationPanel, e.UserState.ToString());
                SendMessageToStatusBar(this, new StatusBarMessageEventArgs(e.UserState.ToString()));
            };
            bwTransferData.RunWorkerAsync(lvEntities.SelectedItems.Cast<ListViewItem>().Select(v => (EntityMetadata)v.Tag).ToList());
        }
        private void AddAttributeToGrid(params object[] values)
        {
            //his.grdAttributes.Rows.Add(update, name, logicalname, typename, value);
            this.grdAttributes.Rows.Add(values);
        }

        private void Transfer_OnStatusMessage(object sender, EventArgs e)
        {
            SendMessageToStatusBar(this, new StatusBarMessageEventArgs(((StatusMessageEventArgs)e).Message));
        }

        private void SetListViewSorting(ListView listview, int column)
        {
            var setting = settings.Sortcolumns.Where(s => s.Key == listview.Name).FirstOrDefault();
            if (setting == null)
            {
                setting = new Item<string, int>(listview.Name, -1);
                settings.Sortcolumns.Add(setting);
            }

            if (setting.Value != column)
            {
                setting.Value = column;
                listview.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listview.Sorting == SortOrder.Ascending)
                    listview.Sorting = SortOrder.Descending;
                else
                    listview.Sorting = SortOrder.Ascending;
            }

            listview.ListViewItemSorter = new ListViewItemComparer(column, listview.Sorting);
        }

        private void SetListViewFilter(ListView listview, List<ListViewItem> items, string filter)
        {
            workingstate = true;

            listview.Items.Clear(); // clear list items before adding 
            // filter the items match with search key and add result to list view 
            listview.Items.AddRange(items.Where(i => string.IsNullOrEmpty(filter) || ContainsText(i, filter)).ToArray());

            workingstate = false;
        }

        private bool ContainsText(ListViewItem item, string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;

            // Check everything lowercase
            text = text.ToLower();

            // Check item text
            if (item.Text.ToLower().Contains(text))
                return true;

            // Check subitems text
            foreach (ListViewItem.ListViewSubItem sitem in item.SubItems)
                if (sitem.Text.ToLower().Contains(text))
                    return true;

            // No matches found
            return false;
        }

        private static bool ValidateValue(string type, string value, out string format)
        {
            var result = true;
            format = string.Empty;

            if (!string.IsNullOrEmpty(value))
            {
                switch (type.ToLower())
                {
                    case "integer":
                        // Value should be an integer
                        format = "#0";
                        int ti;
                        if (!int.TryParse(value, out ti))
                            result = false;
                        break;
                    case "picklist":
                        // value should be a valid integer -> available in options?
                        format = "#0";
                        int tp;
                        if (!int.TryParse(value, out tp))
                            result = false;
                        break;
                    case "string":
                    case "memo":
                        // Value should be text... no validation
                        break;
                    case "money":
                    case "double":
                        // Value should be a double
                        format = "#0.#";
                        double td;
                        if (!double.TryParse(value, out td))
                            result = false;
                        break;
                    case "boolean":
                        // true or false
                        format = "true/false";
                        bool tb;
                        if (!bool.TryParse(value, out tb))
                            result = false;
                        break;
                    case "owner":
                    case "lookup":
                        // Value should be a GUID
                        format = Guid.Empty.ToString();
                        Guid tg;
                        if (!Guid.TryParse(value, out tg))
                            result = false;
                        break;
                    case "datetime":
                        // Valid date
                        format = "yyyy-MM-dd HH:mm";
                        DateTime tdt;
                        if (!DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out tdt))
                            result = false;
                        break;
                }
            }

            return result;
        }

        private static object ParseValue(string type, string value)
        {
            object result = null;

            if (!string.IsNullOrEmpty(value))
            {
                switch (type.ToLower())
                {
                    case "integer":
                        // Value should be an integer
                        int ti;
                        if (int.TryParse(value, out ti))
                            result = ti;
                        break;
                    case "picklist":
                        // value should be a valid integer -> available in options?
                        int tp;
                        if (int.TryParse(value, out tp))
                            result = new OptionSetValue(tp);
                        break;
                    case "string":
                    case "memo":
                        // Value should be text... no validation
                        result = value;
                        break;
                    case "double":
                        // Value should be a double
                        double td;
                        if (double.TryParse(value, out td))
                            result = td;
                        break;
                    case "money":
                        // Value should be a double
                        decimal tm;
                        if (!decimal.TryParse(value, out tm))
                            result = new Money(tm);
                        break;
                    case "boolean":
                        // true or false
                        bool tb;
                        if (bool.TryParse(value, out tb))
                            result = tb;
                        break;
                    case "lookup":
                        // Value should be a GUID
                        Guid tg;
                        if (!Guid.TryParse(value, out tg))
                            result = new EntityReference("", tg);
                        break;
                    case "datetime":
                        // Valid date
                        DateTime tdt;
                        if (!DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out tdt))
                            result = tdt;
                        break;
                }
            }

            return result;
        }

        #endregion Methods

    }
}