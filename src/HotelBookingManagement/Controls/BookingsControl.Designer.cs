using System.Drawing;
using System.Windows.Forms;

namespace HotelBookingManagement.Controls
{
	partial class BookingsControl
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

        #region Component Designer generated code

        private void InitializeComponent()
        {
            lblHeader = new Label();
            lblSub = new Label();
            panelContainer = new Panel();
            SuspendLayout();
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeader.ForeColor = Color.FromArgb(24, 30, 54);
            lblHeader.Location = new Point(22, 15);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(322, 25);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "📅 ĐẶT PHÒNG (HOTEL BOOKING)";
            // 
            // lblSub
            // 
            lblSub.AutoSize = true;
            lblSub.Font = new Font("Segoe UI", 10F);
            lblSub.ForeColor = Color.Gray;
            lblSub.Location = new Point(24, 41);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(507, 19);
            lblSub.TabIndex = 1;
            lblSub.Text = "Module do TV03 phụ trách. Tìm phòng trống theo ngày, tính tiền và tạo đơn đặt.";
            // 
            // panelContainer
            // 
            panelContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelContainer.BackColor = Color.White;
            panelContainer.BorderStyle = BorderStyle.FixedSingle;
            panelContainer.Location = new Point(22, 71);
            panelContainer.Margin = new Padding(3, 2, 3, 2);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(700, 346);
            panelContainer.TabIndex = 2;
            panelContainer.Paint += panelContainer_Paint;
            // 
            // BookingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 246, 249);
            Controls.Add(panelContainer);
            Controls.Add(lblSub);
            Controls.Add(lblHeader);
            Margin = new Padding(3, 2, 3, 2);
            Name = "BookingsControl";
            Size = new Size(744, 435);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHeader;
		private Label lblSub;
		private Panel panelContainer;
	}
}
