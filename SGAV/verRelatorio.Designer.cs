namespace SGAV
{
    partial class verRelatorio
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bunifuFlatButton1 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuDatepicker_para = new Bunifu.Framework.UI.BunifuDatepicker();
            this.bunifuDatepicker_de = new Bunifu.Framework.UI.BunifuDatepicker();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(304, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 24);
            this.label2.TabIndex = 29;
            this.label2.Text = "Até:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 24);
            this.label1.TabIndex = 28;
            this.label1.Text = "Vendas de:";
            // 
            // bunifuFlatButton1
            // 
            this.bunifuFlatButton1.Active = false;
            this.bunifuFlatButton1.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuFlatButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuFlatButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuFlatButton1.BorderRadius = 0;
            this.bunifuFlatButton1.ButtonText = "Visualizar";
            this.bunifuFlatButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuFlatButton1.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuFlatButton1.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButton1.Iconimage = null;
            this.bunifuFlatButton1.Iconimage_right = null;
            this.bunifuFlatButton1.Iconimage_right_Selected = null;
            this.bunifuFlatButton1.Iconimage_Selected = null;
            this.bunifuFlatButton1.IconMarginLeft = 0;
            this.bunifuFlatButton1.IconMarginRight = 0;
            this.bunifuFlatButton1.IconRightVisible = true;
            this.bunifuFlatButton1.IconRightZoom = 0D;
            this.bunifuFlatButton1.IconVisible = true;
            this.bunifuFlatButton1.IconZoom = 60D;
            this.bunifuFlatButton1.IsTab = false;
            this.bunifuFlatButton1.Location = new System.Drawing.Point(207, 202);
            this.bunifuFlatButton1.Name = "bunifuFlatButton1";
            this.bunifuFlatButton1.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuFlatButton1.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.bunifuFlatButton1.OnHoverTextColor = System.Drawing.Color.White;
            this.bunifuFlatButton1.selected = false;
            this.bunifuFlatButton1.Size = new System.Drawing.Size(150, 33);
            this.bunifuFlatButton1.TabIndex = 27;
            this.bunifuFlatButton1.Text = "Visualizar";
            this.bunifuFlatButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuFlatButton1.Textcolor = System.Drawing.Color.White;
            this.bunifuFlatButton1.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButton1.Click += new System.EventHandler(this.VisualizarRel);
            // 
            // bunifuDatepicker_para
            // 
            this.bunifuDatepicker_para.BackColor = System.Drawing.Color.SeaGreen;
            this.bunifuDatepicker_para.BorderRadius = 0;
            this.bunifuDatepicker_para.ForeColor = System.Drawing.Color.White;
            this.bunifuDatepicker_para.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.bunifuDatepicker_para.FormatCustom = null;
            this.bunifuDatepicker_para.Location = new System.Drawing.Point(308, 114);
            this.bunifuDatepicker_para.Name = "bunifuDatepicker_para";
            this.bunifuDatepicker_para.Size = new System.Drawing.Size(186, 33);
            this.bunifuDatepicker_para.TabIndex = 26;
            this.bunifuDatepicker_para.Value = new System.DateTime(2019, 8, 26, 18, 48, 14, 744);
            // 
            // bunifuDatepicker_de
            // 
            this.bunifuDatepicker_de.BackColor = System.Drawing.Color.SeaGreen;
            this.bunifuDatepicker_de.BorderRadius = 0;
            this.bunifuDatepicker_de.ForeColor = System.Drawing.Color.White;
            this.bunifuDatepicker_de.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.bunifuDatepicker_de.FormatCustom = null;
            this.bunifuDatepicker_de.Location = new System.Drawing.Point(74, 114);
            this.bunifuDatepicker_de.Name = "bunifuDatepicker_de";
            this.bunifuDatepicker_de.Size = new System.Drawing.Size(186, 33);
            this.bunifuDatepicker_de.TabIndex = 25;
            this.bunifuDatepicker_de.Value = new System.DateTime(2019, 8, 26, 18, 48, 14, 744);
            // 
            // verRelatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 322);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bunifuFlatButton1);
            this.Controls.Add(this.bunifuDatepicker_para);
            this.Controls.Add(this.bunifuDatepicker_de);
            this.Name = "verRelatorio";
            this.Text = "Ver relatorio";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuFlatButton bunifuFlatButton1;
        private Bunifu.Framework.UI.BunifuDatepicker bunifuDatepicker_para;
        private Bunifu.Framework.UI.BunifuDatepicker bunifuDatepicker_de;
    }
}