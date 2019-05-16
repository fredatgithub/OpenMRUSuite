﻿using System;
using System.Windows.Forms;
using MRUGuiCore.ViewInterfaces;
using MRUCore.Interfaces;
using System.IO;
using MRUGuiCore;

namespace MRUGuiWin
{
    public partial class MRUItemControl : UserControl, IMRUItemView
    {
        public event Action<string> PinItemRequested;
        public event Action<string> DeleteItemRequested;
        public event Action<string> ItemSelected;

        public void Initialize(IMRUItem item, MRUGuiItemLocalization localization)
        {
            this.item = item;
            FileInfo fileInfo = new FileInfo(item.FilePath);
            labelFileName.Text = fileInfo.Name;
            labelPath.Text = fileInfo.DirectoryName;
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pictureBoxRemove, localization.DeleteItemLabel);
            if (item.Pinned)
            {
                tt.SetToolTip(this.pictureBoxPin, localization.UnpinItemLabel);
            } else
            {
                tt.SetToolTip(this.pictureBoxPin, localization.PinItemLabel);
            }
            
        }

        public MRUItemControl()
        {
            InitializeComponent();
        }

        private IMRUItem item;

        private void MRUItemControl_Click(object sender, EventArgs e)
        {
            ItemSelected?.Invoke(item.FilePath);
        }

        private void pictureBoxRemove_Click(object sender, EventArgs e)
        {
            DeleteItemRequested?.Invoke(item.FilePath);
        }

        private void pictureBoxPin_Click(object sender, EventArgs e)
        {
            PinItemRequested?.Invoke(item.FilePath);
        }


        private void MRUItemControl_MouseEnter(object sender, EventArgs e)
        {
            ApplySelection();
        }

        private void MRUItemControl_MouseLeave(object sender, EventArgs e)
        {
            DiscardSelection();
        }

        private void ApplySelection()
        {
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            panelActions.Visible = true;
        }

        private void DiscardSelection()
        {
            if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                this.BackColor = System.Drawing.SystemColors.Control;
                panelActions.Visible = false;
            }
        }

    }
}
