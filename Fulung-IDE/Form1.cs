using System;
using System.Drawing;
using System.Windows.Forms;
using ScintillaNET;

namespace Fulung_IDE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             
            Scintilla scintilla = CreateScintillaEditor();
            tabPage1.Controls.Add(scintilla);
        }

        private Scintilla CreateScintillaEditor()
        {
            Scintilla scintilla = new Scintilla();
            scintilla.Dock = DockStyle.Fill;

             
            scintilla.Lexer = Lexer.Cpp;
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Cpp.Default].Font = "Consolas";
            scintilla.Styles[Style.Cpp.Default].Size = 12;
            scintilla.StyleClearAll();

             
            scintilla.Styles[Style.Cpp.Comment].ForeColor = Color.Green;
            scintilla.Styles[Style.Cpp.CommentLine].ForeColor = Color.Green;
            scintilla.Styles[Style.Cpp.Number].ForeColor = Color.Orange;
            scintilla.Styles[Style.Cpp.String].ForeColor = Color.Brown;
            scintilla.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
             
            scintilla.SetKeywords(0, "using namespace class void int string public private protected static if else switch case break for while do return");

          
            scintilla.Margins[0].Width = 30;

            return scintilla;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            TabPage newTab = new TabPage("New File");
            Scintilla newEditor = CreateScintillaEditor();
            newTab.Controls.Add(newEditor);
            tabControl1.TabPages.Add(newTab);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string content = System.IO.File.ReadAllText(filePath);

                
                TabPage newTab = new TabPage(System.IO.Path.GetFileName(filePath));
                Scintilla editor = CreateScintillaEditor();
                editor.Text = content;
                newTab.Controls.Add(editor);
                tabControl1.TabPages.Add(newTab);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    Scintilla editor = (Scintilla)tabControl1.SelectedTab.Controls[0];
                    System.IO.File.WriteAllText(filePath, editor.Text);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetActiveEditor() != null)
                GetActiveEditor().Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetActiveEditor() != null)
                GetActiveEditor().Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetActiveEditor() != null)
                GetActiveEditor().Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetActiveEditor() != null)
                GetActiveEditor().Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GetActiveEditor() != null)
                GetActiveEditor().Paste();
        }

        private Scintilla GetActiveEditor()
        {
            if (tabControl1.SelectedTab != null && tabControl1.SelectedTab.Controls.Count > 0)
                return tabControl1.SelectedTab.Controls[0] as Scintilla;
            return null;
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
