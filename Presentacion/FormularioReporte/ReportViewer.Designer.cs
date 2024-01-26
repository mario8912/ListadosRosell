

namespace Capas
{
    partial class ReportViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.visorReporte = new CrystalDecisions.Windows.Forms.CrystalReportViewer();

            this.SuspendLayout();
            // 
            // visorReporte
            // 
            this.visorReporte.ActiveViewIndex = -1;
            this.visorReporte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.visorReporte.Cursor = System.Windows.Forms.Cursors.Default;
            this.visorReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visorReporte.Location = new System.Drawing.Point(0, 0);
            this.visorReporte.Name = "visorReporte";
            this.visorReporte.Size = new System.Drawing.Size(854, 509);
            this.visorReporte.TabIndex = 0;
            // 
            // ReportViewer
            // 
            this.ClientSize = new System.Drawing.Size(854, 509);
            this.Controls.Add(this.visorReporte);
            this.Name = "ReportViewer";
            this.Load += new System.EventHandler(this.ReportViewer_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer visorReporte;
    }
}