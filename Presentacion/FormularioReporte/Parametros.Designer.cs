﻿namespace Capas
{
    partial class Parametros
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
            this.chkBoxVistaPrevia = new System.Windows.Forms.CheckBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkBoxVistaPrevia
            // 
            this.chkBoxVistaPrevia.AutoSize = true;
            this.chkBoxVistaPrevia.Location = new System.Drawing.Point(626, 421);
            this.chkBoxVistaPrevia.Name = "chkBoxVistaPrevia";
            this.chkBoxVistaPrevia.Size = new System.Drawing.Size(81, 17);
            this.chkBoxVistaPrevia.TabIndex = 0;
            this.chkBoxVistaPrevia.Text = "Vista previa";
            this.chkBoxVistaPrevia.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(713, 415);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // Parametros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.chkBoxVistaPrevia);
            this.Name = "Parametros";
            this.Text = "FormParametrosReporte";
            this.Load += new System.EventHandler(this.FormParametrosReporte_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkBoxVistaPrevia;
        private System.Windows.Forms.Button btnAceptar;
    }
}