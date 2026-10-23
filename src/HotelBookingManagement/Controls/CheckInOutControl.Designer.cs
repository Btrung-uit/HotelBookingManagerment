using System.Drawing;
using System.Windows.Forms;

namespace HotelBookingManagement.Controls
{
	partial class CheckInOutControl
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
			lblHeader.Location = new Point(25, 20);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(470, 32);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "🔑 NHẬN VÀ TRẢ PHÒNG (CHECK-IN / OUT)";
			// 
			// lblSub
			// 
			lblSub.AutoSize = true;
			lblSub.Font = new Font("Segoe UI", 10F);
			lblSub.ForeColor = Color.Gray;
			lblSub.Location = new Point(28, 55);
			lblSub.Name = "lblSub";
			lblSub.Size = new Size(610, 23);
			lblSub.TabIndex = 1;
			lblSub.Text = "Module do TV03 phụ trách. Xác nhận khách nhận phòng và chuyển trạng thái phòng.";
			// 
			// panelContainer
			// 
			panelContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			panelContainer.BackColor = Color.White;
			panelContainer.BorderStyle = BorderStyle.FixedSingle;
			panelContainer.Location = new Point(25, 95);
			panelContainer.Name = "panelContainer";
			panelContainer.Size = new Size(800, 460);
			panelContainer.TabIndex = 2;
			// 
			// CheckInOutControl
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(244, 246, 249);
			Controls.Add(panelContainer);
			Controls.Add(lblSub);
			Controls.Add(lblHeader);
			Name = "CheckInOutControl";
			Size = new Size(850, 580);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label lblHeader;
		private Label lblSub;
		private Panel panelContainer;
	}
}
