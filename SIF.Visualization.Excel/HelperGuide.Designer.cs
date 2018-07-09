namespace SIF.Visualization.Excel
{
    partial class HelperGuide
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Prüfen");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Zellen auf Fehler überprüfen");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Formelkomplexität überprüfen");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Formeln auf gleiche Verweise überprüfen");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Formeln auf Konstante überprüfen");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Konstanten ohne Bezug suchen");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Zellennachbarschaft untersuchen");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Leserichtung überprüfen");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Verweise auf leere Zellen suchen");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Levenstein Distanz berechnen");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Regeln konfigurieren", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Seitenleiste");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Scenario");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Zwischenergebniszelle festlegen");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Eingabezellen festlegen");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Ergebniszelle festlegen");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Zellen festlegen", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15,
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Globale Einstellungen");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Regex");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Leere Zelle");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Gesamtanzahl Zeichen");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Nur Zahlen");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Eine Nachkommastelle");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("2 Nachkommastellen");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Neue Datenregeln erstellen", new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23,
            treeNode24});
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.bez_button = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.bef_desc = new System.Windows.Forms.TabPage();
            this.userguid = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.bez_button.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.bez_button);
            this.tabControl1.Controls.Add(this.bef_desc);
            this.tabControl1.Location = new System.Drawing.Point(1, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(819, 384);
            this.tabControl1.TabIndex = 0;
            // 
            // bez_button
            // 
            this.bez_button.Controls.Add(this.splitContainer1);
            this.bez_button.Location = new System.Drawing.Point(4, 22);
            this.bez_button.Name = "bez_button";
            this.bez_button.Padding = new System.Windows.Forms.Padding(3);
            this.bez_button.Size = new System.Drawing.Size(811, 358);
            this.bez_button.TabIndex = 0;
            this.bez_button.Text = "User Guid";
            this.bez_button.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer1.Size = new System.Drawing.Size(805, 352);
            this.splitContainer1.SplitterDistance = 268;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.treeView1.Location = new System.Drawing.Point(4, -3);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Prüfen";
            treeNode1.Text = "Prüfen";
            treeNode2.Name = "cell";
            treeNode2.Text = "Zellen auf Fehler überprüfen";
            treeNode3.Name = "complexity";
            treeNode3.Text = "Formelkomplexität überprüfen";
            treeNode4.Name = "gleicheVerweise";
            treeNode4.Text = "Formeln auf gleiche Verweise überprüfen";
            treeNode5.Name = "formelkonstante";
            treeNode5.Text = "Formeln auf Konstante überprüfen";
            treeNode6.Name = "ohneBezug";
            treeNode6.Text = "Konstanten ohne Bezug suchen";
            treeNode7.Name = "zellennachbarschaft";
            treeNode7.Text = "Zellennachbarschaft untersuchen";
            treeNode8.Name = "Leserichtung ";
            treeNode8.Text = "Leserichtung überprüfen";
            treeNode9.Name = "leerezellen";
            treeNode9.Text = "Verweise auf leere Zellen suchen";
            treeNode10.Name = "levenstein";
            treeNode10.Text = "Levenstein Distanz berechnen";
            treeNode11.Name = "config_rules";
            treeNode11.Text = "Regeln konfigurieren";
            treeNode12.Name = "Seitenleiste";
            treeNode12.Text = "Seitenleiste";
            treeNode13.Name = "Scenario";
            treeNode13.Text = "Scenario";
            treeNode14.Name = "Zwischenergebniszelle";
            treeNode14.Text = "Zwischenergebniszelle festlegen";
            treeNode15.Name = "Eingabezellen";
            treeNode15.Text = "Eingabezellen festlegen";
            treeNode16.Name = "Ergebniszelle";
            treeNode16.Text = "Ergebniszelle festlegen";
            treeNode17.Name = "defin_cell";
            treeNode17.Text = "Zellen festlegen";
            treeNode18.Name = "globalsettings";
            treeNode18.Text = "Globale Einstellungen";
            treeNode19.Name = "Regex";
            treeNode19.Text = "Regex";
            treeNode20.Name = "leer";
            treeNode20.Text = "Leere Zelle";
            treeNode21.Name = "zeichen";
            treeNode21.Text = "Gesamtanzahl Zeichen";
            treeNode22.Name = "zahlen";
            treeNode22.Text = "Nur Zahlen";
            treeNode23.Name = "komma1";
            treeNode23.Text = "Eine Nachkommastelle";
            treeNode24.Name = "komma2";
            treeNode24.Text = "2 Nachkommastellen";
            treeNode25.Name = "dataRules";
            treeNode25.Text = "Neue Datenregeln erstellen";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode17,
            treeNode18,
            treeNode25});
            this.treeView1.Size = new System.Drawing.Size(261, 346);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(537, 352);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // bef_desc
            // 
            this.bef_desc.Location = new System.Drawing.Point(4, 22);
            this.bef_desc.Name = "bef_desc";
            this.bef_desc.Padding = new System.Windows.Forms.Padding(3);
            this.bef_desc.Size = new System.Drawing.Size(811, 358);
            this.bef_desc.TabIndex = 1;
            this.bef_desc.Text = "Befundsbeschreibung";
            this.bef_desc.UseVisualStyleBackColor = true;
            // 
            // userguid
            // 
            this.userguid.AutoSize = true;
            this.userguid.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F);
            this.userguid.Location = new System.Drawing.Point(306, 13);
            this.userguid.Name = "userguid";
            this.userguid.Size = new System.Drawing.Size(166, 36);
            this.userguid.TabIndex = 1;
            this.userguid.Text = "User Guide";
            this.userguid.Click += new System.EventHandler(this.userguid_Click);
            // 
            // HelperGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(832, 445);
            this.Controls.Add(this.userguid);
            this.Controls.Add(this.tabControl1);
            this.Name = "HelperGuide";
            this.Text = "Helper";
            this.tabControl1.ResumeLayout(false);
            this.bez_button.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage bez_button;
        private System.Windows.Forms.TabPage bef_desc;
        private System.Windows.Forms.Label userguid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}