using Colso.Xrm.DataAttributeUpdater.AppCode;
using Colso.Xrm.DataAttributeUpdater.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Colso.Xrm.DataAttributeUpdater.Forms
{
    public partial class ErrorList : Form
    {
        private List<Item<string, string>> errors;

        public ErrorList(List<Item<string, string>> errors)
        {
            this.errors = errors;
            InitializeComponent();
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void ErrorListLoad(object sender, EventArgs e)
        {
            foreach (var error in errors)
            {
                var item = new ListViewItem(error.Key);
                item.SubItems.Add(error.Value);

                lvErrors.Items.Add(item);
            }
        }
    }
}