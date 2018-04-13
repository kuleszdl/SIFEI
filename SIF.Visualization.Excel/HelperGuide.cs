using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIF.Visualization.Excel
{
    public partial class HelperGuide : Form
    {
        public HelperGuide()
        {
            InitializeComponent();
        }

        private void userguid_Click(object sender, EventArgs e)
        {

        }
        private void insertIMage(String path)
        {
            path = System.AppDomain.CurrentDomain.BaseDirectory + "../../" + path;
            Label imgLabel = new Label();
            imgLabel.Image = Image.FromFile(path);
            imgLabel.AutoSize = false;
            imgLabel.Size = imgLabel.Image.Size;
            imgLabel.ImageAlign = ContentAlignment.MiddleCenter;
            imgLabel.Text = "";
            imgLabel.BackColor = Color.Transparent;
            imgLabel.Parent = richTextBox1;
            // pick a location where it won't get in the way too much
            imgLabel.Location = new Point(richTextBox1.ClientSize.Width - imgLabel.Image.Width, 0);
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name == "Prüfen")
            {
                richTextBox1.Text = @"<h1>Prüfen ist die eigentliche Hauptfunktion des Tools und prüft das Arbeitsblatt auf konfigurierte Regeln oder definierte Szenarien.
Alle Befunde können in der Sidebar anzgezeigt werde.</h1>";
            }
            if (e.Node.Name == "config_rules")
            {
                richTextBox1.Text = "Auswahl der zu Prüfenden Konfigurationen ";
            }
            if (e.Node.Name == "Seitenleiste")
            {
                richTextBox1.Text = "Anzeige einer Übersicht über die gefundenen Mängel";
            }
            if (e.Node.Name == "Testszenario")
            {
                richTextBox1.Text = "Mit Scenarien lassen sich benutzerdefinierte Tests erstellen, um Soll- und Istwerte zu prüfen";
            }
            if (e.Node.Name == "defin_cell")
            {
                richTextBox1.Text = "Anzeige einer Übersicht über die gefundenen Mängel";
            }
            if (e.Node.Name == "globalsettings")
            {
                richTextBox1.Text = "Einstellungen um den Server zu erreichen. ";
            }
            if (e.Node.Name == "dataRules")
            {
                richTextBox1.Text = "Hier werden benutzerdefninierte Regeln definiert. ";
            }
            if (e.Node.Name == "complexity")
            {
                richTextBox1.Text = @"Es wird geprüft ob die Formelkomplexität einen
gewissen Grad überschreitet 
(gemessen an der Schachtelungstiefe und der Anzahl von Operatoren)
Zu komplexe Formeln sind bei der Wartung sehr fehleranfällig. Komplexe Formeln sollten in weniger komplexe Formeln gebrochen werden.";
            }
            if (e.Node.Name == "gleicheVerweise")
            {
                richTextBox1.Text = "Es wird geprüft ob eine Formel mehrere äquivalente aufeinanderfolgende Verweise enthält. Mehrere Verweise auf die gleiche Zelle sind meist unbeabsichtigt. Prüfen Sie bei einem Befund ob die Verweise auf die gleiche Zelle beabsichtigt sind.";
            }
            if (e.Node.Name == "formelkonstante")
            {
                richTextBox1.Text = "Es wird geprüft ob Formeln Konstanten enthalten. Bei Änderungen der Werte wird es in der Wartung sehr aufwändig alle Formeln anzupassen. Extrahieren Sie die Formel in eine eigene Zelle und erstellen Sie einen Verweis auf diese.";

            }
            if (e.Node.Name == "ohneBezug")
            {
                richTextBox1.Text = "Es wird geprüft ob Werte ohne Bezug aufzufinden sind. Nutzen oder entfernen Sie diese Werte.";
            }
            if (e.Node.Name == "zellennachbarschaft")
            {
                richTextBox1.Text = "Es wird geprüft ob sich in der Umgebung eines Werts andere Werte mit dem gleichen Typ befinden." +
                    "Einzelne Werte mit anderem Typ als die Umgebungszellen können auf einen Fehler hinweisen. Stellen Sie sicher dass der Typ des Zellenwerts korrekt ist. ";
            }
            if (e.Node.Name == "Leserichtung")
            {
                richTextBox1.Text = "Prüft ob  ob Zellen mit Formeln nur auf Zellen links oder oberhalb verweisen. Zellen mit Formeln sollten nur auf Zellen links oder oberhalb von sich verweisen, weil das die übliche westliche Leserichtung ist." +
                    "Verschieben Sie die Zelle mit der Formel oder die Zellen, auf die sich die Zelle bezieht.";
            }
            if (e.Node.Name == "leerezellen")
            {
                richTextBox1.Text = "Prüft ob Formeln auf leere Zellen verweisen. Verweise auf leere Zellen sind oft Fehler. Stellen Sie sicher der Zellenwert gültig ist.";
            }
            if (e.Node.Name == "levenstein")
            {
                richTextBox1.Text = "Es wird die Levenshtein-Distanz analysiert. Dieser prüft ob mögliche Schreibfehler enthalten sind. ";
            }
            if (e.Node.Name == "cell")
            {
                richTextBox1.Text = "Es wird geprüft ob der Wert einer Zelle ungültig ist.";
            }
            if (e.Node.Name == "Eingabezellen")
            {
                richTextBox1.Text = "Es werden die Zellen markiert und benutzerdefinierte Werte übergeben";
            }
            if (e.Node.Name == "Ergebniszelle")
            {
                richTextBox1.Text = "Es wird das zu erwartende Ergebnis übergeben";
            }
            if (e.Node.Name == "Zwischenergebniszelle")
            {
                richTextBox1.Text = "Es wird das zu erwartende Zwischenergebnis übergeben";
            }
            if (e.Node.Name == "Regex")
            {
                richTextBox1.Text = "Die markierten Zellen sind nur dann gültig wenn der Regex auf den Wert matcht.";
            }
            if (e.Node.Name == "leer")
            {
                richTextBox1.Text = "Die Zeile wird als leere Zelle definiert";
            }
            if (e.Node.Name == "zeichen")
            {
                richTextBox1.Text = "Der Zellwert darf maximal aus n Zeichen bestehen ";
            }
            if (e.Node.Name == "zahlen")
            {
                richTextBox1.Text = "Der Zellwert darf lediglich aus Zahlen bestehen";
            }
            if (e.Node.Name == "komma1")
            {
                richTextBox1.Text = "Erlaubt ist nur eine Nachkommastelle";
            }
            if (e.Node.Name == "komma2")
            {
                richTextBox1.Text = "Erlaubt sind nur 2 Nachkommastellen";
            }
        }
    }
}
