namespace CowBoy.WF
{
    partial class frmDettaglio
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.grafMesi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.grafMesi)).BeginInit();
            this.SuspendLayout();
            // 
            // grafMesi
            // 
            chartArea2.Name = "ChartArea1";
            this.grafMesi.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.grafMesi.Legends.Add(legend2);
            this.grafMesi.Location = new System.Drawing.Point(61, 40);
            this.grafMesi.Name = "grafMesi";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.grafMesi.Series.Add(series2);
            this.grafMesi.Size = new System.Drawing.Size(607, 251);
            this.grafMesi.TabIndex = 0;
            this.grafMesi.Text = "chart1";
            // 
            // frmDettaglio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 472);
            this.Controls.Add(this.grafMesi);
            this.Font = new System.Drawing.Font("Arial", 8.25F);
            this.Name = "frmDettaglio";
            this.Text = "frmDettaglio";
            this.Load += new System.EventHandler(this.frmDettaglio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grafMesi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart grafMesi;
    }
}