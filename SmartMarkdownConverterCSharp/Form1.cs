using System;
using System.Drawing;
using System.Windows.Forms;
using Markdig;
using ReverseMarkdown;
using SmartMarkdownConverter.Helpers;

namespace SmartMarkdownConverter
{
    public partial class Form1 : Form
    {
        private TextBox txtInput;
        private TextBox txtOutput;
        private ComboBox cmbDirection;
        private Button btnConvert;
        private Button btnCopy;
        private Button btnClear;
        private Button btnSite;
        private Panel topPanel;
        private Panel bottomPanel;
        private Panel containerPanel;
        private Label lblTitle;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel charCountLabel;
        private MenuStrip menuStrip;

        private readonly Color DarkBg = Color.FromArgb(30, 30, 30);
        private readonly Color DarkPanel = Color.FromArgb(45, 45, 45);
        private readonly Color DarkBorder = Color.FromArgb(60, 60, 60);
        private readonly Color DarkText = Color.FromArgb(220, 220, 220);
        private readonly Color DarkInput = Color.FromArgb(37, 37, 38);
        private readonly Color DarkAccent = Color.FromArgb(0, 120, 215);
        private readonly Color DarkTitle = Color.FromArgb(0, 150, 255);

        private readonly MarkdownPipeline _markdownPipeline;

        public Form1()
        {
            _markdownPipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            InitializeComponent();
            SetupUI();
            SetupHotkeys();
        }

        private void SetupUI()
        {
            this.Text = "Smart Markdown Converter";
            this.Size = new Size(1200, 750);
            this.MinimumSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = DarkBg;
            this.Font = new Font("Segoe UI", 10);

            menuStrip = new MenuStrip
            {
                BackColor = Color.FromArgb(25, 25, 25),
                ForeColor = DarkText,
                Font = new Font("Segoe UI", 10),
                Padding = new Padding(10, 2, 0, 2),
                RenderMode = ToolStripRenderMode.Professional,
                Renderer = new DarkMenuRenderer()
            };

            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");

            ToolStripMenuItem newItem = new ToolStripMenuItem("New", null, (s, e) => BtnClear_Click(null, null));
            newItem.ShortcutKeys = Keys.Control | Keys.N;
            newItem.BackColor = Color.FromArgb(25, 25, 25);
            newItem.ForeColor = DarkText;

            ToolStripMenuItem openItem = new ToolStripMenuItem("Open", null, (s, e) => OpenFile());
            openItem.ShortcutKeys = Keys.Control | Keys.O;
            openItem.BackColor = Color.FromArgb(25, 25, 25);
            openItem.ForeColor = DarkText;

            ToolStripMenuItem saveItem = new ToolStripMenuItem("Save", null, (s, e) => SaveFile());
            saveItem.ShortcutKeys = Keys.Control | Keys.S;
            saveItem.BackColor = Color.FromArgb(25, 25, 25);
            saveItem.ForeColor = DarkText;

            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit", null, (s, e) => Application.Exit());
            exitItem.ShortcutKeys = Keys.Alt | Keys.F4;
            exitItem.BackColor = Color.FromArgb(25, 25, 25);
            exitItem.ForeColor = DarkText;

            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { newItem, openItem, saveItem, new ToolStripSeparator(), exitItem });
            fileMenu.BackColor = Color.FromArgb(25, 25, 25);
            fileMenu.ForeColor = DarkText;

            ToolStripMenuItem editMenu = new ToolStripMenuItem("Edit");

            ToolStripMenuItem cutItem = new ToolStripMenuItem("Cut", null, (s, e) => { if (txtInput.Focused) txtInput.Cut(); });
            cutItem.ShortcutKeys = Keys.Control | Keys.X;
            cutItem.BackColor = Color.FromArgb(25, 25, 25);
            cutItem.ForeColor = DarkText;

            ToolStripMenuItem copyItem = new ToolStripMenuItem("Copy", null, (s, e) => { if (txtInput.Focused) txtInput.Copy(); });
            copyItem.ShortcutKeys = Keys.Control | Keys.C;
            copyItem.BackColor = Color.FromArgb(25, 25, 25);
            copyItem.ForeColor = DarkText;

            ToolStripMenuItem pasteItem = new ToolStripMenuItem("Paste", null, (s, e) => { if (txtInput.Focused) txtInput.Paste(); });
            pasteItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteItem.BackColor = Color.FromArgb(25, 25, 25);
            pasteItem.ForeColor = DarkText;

            ToolStripMenuItem selectAllItem = new ToolStripMenuItem("Select All", null, (s, e) => { txtInput.SelectAll(); });
            selectAllItem.ShortcutKeys = Keys.Control | Keys.A;
            selectAllItem.BackColor = Color.FromArgb(25, 25, 25);
            selectAllItem.ForeColor = DarkText;

            editMenu.DropDownItems.AddRange(new ToolStripItem[] { cutItem, copyItem, pasteItem, new ToolStripSeparator(), selectAllItem });
            editMenu.BackColor = Color.FromArgb(25, 25, 25);
            editMenu.ForeColor = DarkText;

            ToolStripMenuItem convertMenu = new ToolStripMenuItem("Convert");

            ToolStripMenuItem convertItem = new ToolStripMenuItem("Convert", null, (s, e) => BtnConvert_Click(null, null));
            convertItem.ShortcutKeys = Keys.Control | Keys.Enter;
            convertItem.BackColor = Color.FromArgb(25, 25, 25);
            convertItem.ForeColor = DarkText;

            ToolStripMenuItem copyResultItem = new ToolStripMenuItem("Copy Result", null, (s, e) => BtnCopy_Click(null, null));
            copyResultItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.C;
            copyResultItem.BackColor = Color.FromArgb(25, 25, 25);
            copyResultItem.ForeColor = DarkText;

            convertMenu.DropDownItems.AddRange(new ToolStripItem[] { convertItem, copyResultItem });
            convertMenu.BackColor = Color.FromArgb(25, 25, 25);
            convertMenu.ForeColor = DarkText;

            ToolStripMenuItem toolsMenu = new ToolStripMenuItem("Tools");

            ToolStripMenuItem createShortcutItem = new ToolStripMenuItem("Create Desktop Shortcut", null, (s, e) => CreateDesktopShortcut());
            createShortcutItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            createShortcutItem.BackColor = Color.FromArgb(25, 25, 25);
            createShortcutItem.ForeColor = DarkText;

            toolsMenu.DropDownItems.Add(createShortcutItem);
            toolsMenu.BackColor = Color.FromArgb(25, 25, 25);
            toolsMenu.ForeColor = DarkText;

            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help");

            ToolStripMenuItem hotkeysItem = new ToolStripMenuItem("Hotkeys", null, (s, e) => ShowHotkeys());
            hotkeysItem.ShortcutKeys = Keys.Control | Keys.H;
            hotkeysItem.BackColor = Color.FromArgb(25, 25, 25);
            hotkeysItem.ForeColor = DarkText;

            ToolStripMenuItem aboutItem = new ToolStripMenuItem("About", null, (s, e) => ShowAbout());
            aboutItem.BackColor = Color.FromArgb(25, 25, 25);
            aboutItem.ForeColor = DarkText;

            helpMenu.DropDownItems.AddRange(new ToolStripItem[] { hotkeysItem, new ToolStripSeparator(), aboutItem });
            helpMenu.BackColor = Color.FromArgb(25, 25, 25);
            helpMenu.ForeColor = DarkText;

            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, editMenu, convertMenu, toolsMenu, helpMenu });

            topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(25, 25, 25),
                Padding = new Padding(20, 10, 20, 10)
            };

            lblTitle = new Label
            {
                Text = "Smart Markdown Converter",
                ForeColor = DarkTitle,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false,
                Height = 40
            };

            Panel directionPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.Transparent
            };

            Label lblDirection = new Label
            {
                Text = "Direction:",
                ForeColor = DarkText,
                Font = new Font("Segoe UI", 11),
                Location = new Point(0, 8),
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            cmbDirection = new ComboBox
            {
                Location = new Point(85, 6),
                Size = new Size(240, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = DarkInput,
                ForeColor = DarkText,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10)
            };
            cmbDirection.Items.AddRange(new object[] { "Markdown -> HTML", "HTML -> Markdown" });
            cmbDirection.SelectedIndex = 0;

            directionPanel.Controls.Add(lblDirection);
            directionPanel.Controls.Add(cmbDirection);

            topPanel.Controls.Add(directionPanel);
            topPanel.Controls.Add(lblTitle);

            containerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0),
                BackColor = DarkBg
            };

            TableLayoutPanel mainTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(15, 10, 15, 10),
                BackColor = DarkBg
            };
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Panel leftPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 0, 10, 0),
                BackColor = DarkBg
            };

            Label lblInput = new Label
            {
                Text = "Input",
                Dock = DockStyle.Top,
                Height = 35,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = DarkText,
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtInput = CreateStyledTextBox("Enter Markdown or HTML...");
            leftPanel.Controls.Add(txtInput);
            leftPanel.Controls.Add(lblInput);

            Panel rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 0, 0, 0),
                BackColor = DarkBg
            };

            Label lblOutput = new Label
            {
                Text = "Result",
                Dock = DockStyle.Top,
                Height = 35,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = DarkText,
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtOutput = CreateStyledTextBox("Result will appear here...", true);
            rightPanel.Controls.Add(txtOutput);
            rightPanel.Controls.Add(lblOutput);

            mainTable.Controls.Add(leftPanel, 0, 0);
            mainTable.Controls.Add(rightPanel, 1, 0);

            bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(25, 25, 25),
                Padding = new Padding(15, 10, 15, 10)
            };

            FlowLayoutPanel flowButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0),
                BackColor = Color.Transparent
            };

            btnConvert = CreateStyledButton("Convert", Color.FromArgb(0, 120, 215), Color.FromArgb(0, 100, 180));
            btnConvert.Click += BtnConvert_Click;

            btnCopy = CreateStyledButton("Copy", Color.FromArgb(46, 204, 113), Color.FromArgb(39, 174, 96));
            btnCopy.Click += BtnCopy_Click;

            btnClear = CreateStyledButton("Clear", Color.FromArgb(200, 60, 50), Color.FromArgb(180, 50, 40));
            btnClear.Click += BtnClear_Click;

            btnSite = CreateStyledButton("Website", Color.FromArgb(155, 89, 182), Color.FromArgb(142, 68, 173));
            btnSite.Click += (s, e) => {
                try
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "https://smartlegionlab.github.io/projects/smart-markdown-converter-cs.html",
                        UseShellExecute = true
                    };
                    System.Diagnostics.Process.Start(psi);
                }
                catch (Exception ex)
                {
                    ShowCustomMessage("Error", $"Failed to open website:\n{ex.Message}", MessageType.Error);
                }
            };

            flowButtons.Controls.Add(btnConvert);
            flowButtons.Controls.Add(btnCopy);
            flowButtons.Controls.Add(btnClear);
            flowButtons.Controls.Add(btnSite);

            bottomPanel.Controls.Add(flowButtons);

            statusStrip = new StatusStrip
            {
                BackColor = Color.FromArgb(25, 25, 25),
                ForeColor = DarkText,
                Font = new Font("Segoe UI", 9),
                RenderMode = ToolStripRenderMode.Professional,
                Renderer = new DarkStatusRenderer()
            };

            statusLabel = new ToolStripStatusLabel("Ready");
            charCountLabel = new ToolStripStatusLabel("Characters: 0");

            statusStrip.Items.Add(statusLabel);
            statusStrip.Items.Add(new ToolStripStatusLabel("│"));
            statusStrip.Items.Add(charCountLabel);
            statusStrip.Items.Add(new ToolStripStatusLabel("│"));
            statusStrip.Items.Add(new ToolStripStatusLabel("Markdown <-> HTML"));

            containerPanel.Controls.Add(mainTable);
            containerPanel.Controls.Add(bottomPanel);
            containerPanel.Controls.Add(topPanel);

            this.Controls.Add(containerPanel);
            this.Controls.Add(statusStrip);
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;

            txtInput.TextChanged += (s, e) => {
                charCountLabel.Text = $"Characters: {txtInput.Text.Length}";
            };

            this.Resize += Form1_Resize;
        }

        public class DarkMenuRenderer : ToolStripProfessionalRenderer
        {
            public DarkMenuRenderer() : base(new DarkMenuColors()) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (e.Item.Selected)
                {
                    Rectangle rect = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
                    using (Brush brush = new SolidBrush(Color.FromArgb(0, 80, 160)))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                    using (Pen pen = new Pen(Color.FromArgb(0, 100, 200), 1))
                    {
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                }
                else if (e.Item.Pressed)
                {
                    Rectangle rect = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
                    using (Brush brush = new SolidBrush(Color.FromArgb(0, 60, 140)))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                }
                else
                {
                    base.OnRenderMenuItemBackground(e);
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                if (e.Item.Selected || e.Item.Pressed)
                {
                    e.TextColor = Color.White;
                }
                else
                {
                    e.TextColor = Color.FromArgb(200, 200, 200);
                }
                base.OnRenderItemText(e);
            }
        }

        public class DarkMenuColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected => Color.FromArgb(0, 80, 160);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(0, 80, 160);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(0, 70, 150);
            public override Color MenuItemPressedGradientBegin => Color.FromArgb(0, 60, 140);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(0, 50, 130);
            public override Color MenuStripGradientBegin => Color.FromArgb(25, 25, 25);
            public override Color MenuStripGradientEnd => Color.FromArgb(25, 25, 25);
            public override Color ImageMarginGradientBegin => Color.FromArgb(25, 25, 25);
            public override Color ImageMarginGradientEnd => Color.FromArgb(25, 25, 25);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(25, 25, 25);
            public override Color ToolStripDropDownBackground => Color.FromArgb(30, 30, 30);
            public override Color MenuBorder => Color.FromArgb(60, 60, 60);
            public override Color SeparatorDark => Color.FromArgb(60, 60, 60);
            public override Color SeparatorLight => Color.FromArgb(40, 40, 40);
        }

        public class DarkStatusRenderer : ToolStripProfessionalRenderer
        {
            public DarkStatusRenderer() : base(new DarkStatusColors()) { }
        }

        public class DarkStatusColors : ProfessionalColorTable
        {
            public override Color StatusStripGradientBegin => Color.FromArgb(25, 25, 25);
            public override Color StatusStripGradientEnd => Color.FromArgb(25, 25, 25);
        }

        private TextBox CreateStyledTextBox(string placeholder, bool readOnly = false)
        {
            TextBox tb = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                Font = new Font("Consolas", 12),
                BackColor = DarkInput,
                ForeColor = DarkText,
                BorderStyle = BorderStyle.FixedSingle,
                Text = placeholder,
                ReadOnly = readOnly,
                ScrollBars = ScrollBars.Both,
                WordWrap = false
            };

            if (!readOnly)
            {
                tb.Enter += (s, e) => {
                    if (tb.Text == placeholder)
                        tb.Text = "";
                    tb.BackColor = Color.FromArgb(50, 50, 52);
                };
                tb.Leave += (s, e) => {
                    if (string.IsNullOrWhiteSpace(tb.Text))
                        tb.Text = placeholder;
                    tb.BackColor = DarkInput;
                };
            }

            return tb;
        }

        private Button CreateStyledButton(string text, Color bgColor, Color hoverColor)
        {
            Button btn = new Button
            {
                Text = text,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = bgColor,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(160, 50),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = hoverColor;

            btn.MouseEnter += (s, e) => {
                btn.BackColor = hoverColor;
            };

            btn.MouseLeave += (s, e) => {
                btn.BackColor = bgColor;
            };

            return btn;
        }

        private void SetupHotkeys()
        {
            this.KeyPreview = true;
            this.KeyDown += (s, e) => {
                if (e.Control && e.KeyCode == Keys.Enter)
                {
                    BtnConvert_Click(null, null);
                    e.Handled = true;
                }
                if (e.Control && e.Shift && e.KeyCode == Keys.C)
                {
                    BtnCopy_Click(null, null);
                    e.Handled = true;
                }
                if (e.Control && e.Shift && e.KeyCode == Keys.S)
                {
                    CreateDesktopShortcut();
                    e.Handled = true;
                }
            };
        }

        private void CreateDesktopShortcut()
        {
            ShellShortcut.CreateDesktopShortcut();
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text) ||
                txtInput.Text == "Enter Markdown or HTML...")
            {
                ShowCustomMessage("Error", "Cannot convert empty input.\nPlease enter some text...", MessageType.Error);
                return;
            }

            try
            {
                string result;

                if (cmbDirection.SelectedIndex == 0)
                {
                    result = Markdown.ToHtml(txtInput.Text, _markdownPipeline);
                }
                else
                {
                    var converter = new ReverseMarkdown.Converter();
                    result = converter.Convert(txtInput.Text);
                }

                txtOutput.Text = result;
                txtOutput.SelectAll();
                statusLabel.Text = $"Converted! {txtInput.Text.Length} -> {result.Length} characters";

                ShowCustomMessage("Success", "Conversion completed successfully!", MessageType.Success);
            }
            catch (Exception ex)
            {
                ShowCustomMessage("Error", $"Conversion failed:\n{ex.Message}", MessageType.Error);
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOutput.Text) &&
                txtOutput.Text != "Result will appear here...")
            {
                Clipboard.SetText(txtOutput.Text);
                statusLabel.Text = "Copied to clipboard!";
                ShowCustomMessage("Success", "Result copied to clipboard!", MessageType.Success);
            }
            else
            {
                ShowCustomMessage("Error", "No data to copy!", MessageType.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtInput.Text = "Enter Markdown or HTML...";
            txtOutput.Text = "Result will appear here...";
            statusLabel.Text = "Cleared";
        }

        private void OpenFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Markdown files (*.md;*.markdown)|*.md;*.markdown|HTML files (*.html;*.htm)|*.html;*.htm|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        txtInput.Text = System.IO.File.ReadAllText(ofd.FileName);
                        statusLabel.Text = $"Loaded: {System.IO.Path.GetFileName(ofd.FileName)}";
                        ShowCustomMessage("Success", $"File {System.IO.Path.GetFileName(ofd.FileName)} loaded!", MessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        ShowCustomMessage("Error", $"Failed to load file:\n{ex.Message}", MessageType.Error);
                    }
                }
            }
        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(txtOutput.Text) || txtOutput.Text == "Result will appear here...")
            {
                ShowCustomMessage("Error", "No data to save!", MessageType.Error);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Markdown files (*.md)|*.md|HTML files (*.html)|*.html|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.IO.File.WriteAllText(sfd.FileName, txtOutput.Text);
                        statusLabel.Text = $"Saved: {System.IO.Path.GetFileName(sfd.FileName)}";
                        ShowCustomMessage("Success", $"File {System.IO.Path.GetFileName(sfd.FileName)} saved!", MessageType.Success);
                    }
                    catch (Exception ex)
                    {
                        ShowCustomMessage("Error", $"Failed to save file:\n{ex.Message}", MessageType.Error);
                    }
                }
            }
        }

        private enum MessageType { Success, Error, Info }

        private void ShowCustomMessage(string title, string message, MessageType type)
        {
            Form dialog = new Form
            {
                Text = title,
                Size = new Size(450, 250),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = DarkBg,
                ForeColor = DarkText,
                Font = this.Font
            };

            Label iconLabel = new Label
            {
                Text = type == MessageType.Success ? "OK" : type == MessageType.Error ? "!" : "i",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = type == MessageType.Success ? Color.FromArgb(46, 204, 113) :
                           type == MessageType.Error ? Color.FromArgb(231, 76, 60) :
                           Color.FromArgb(52, 152, 219),
                Location = new Point(20, 20),
                Size = new Size(80, 80),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = type == MessageType.Success ? Color.FromArgb(46, 204, 113) :
                           type == MessageType.Error ? Color.FromArgb(231, 76, 60) :
                           Color.FromArgb(52, 152, 219),
                Location = new Point(110, 20),
                Size = new Size(300, 35),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label messageLabel = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 11),
                ForeColor = DarkText,
                Location = new Point(110, 60),
                Size = new Size(300, 80),
                TextAlign = ContentAlignment.TopLeft
            };

            Button okButton = new Button
            {
                Text = "OK",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 120, 215),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Location = new Point(320, 160),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.OK
            };
            okButton.FlatAppearance.BorderSize = 0;
            okButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);

            dialog.Controls.AddRange(new Control[] { iconLabel, titleLabel, messageLabel, okButton });
            dialog.ShowDialog();
        }

        private void ShowHotkeys()
        {
            string hotkeys = "Hotkeys:\n\n" +
                            "Ctrl + Enter     - Convert\n" +
                            "Ctrl + Shift + C - Copy Result\n" +
                            "Ctrl + N         - Clear All\n" +
                            "Ctrl + O         - Open File\n" +
                            "Ctrl + S         - Save File\n" +
                            "Ctrl + H         - Show Hotkeys\n" +
                            "Ctrl + Shift + S - Create Desktop Shortcut\n" +
                            "Alt + F4         - Exit";

            Form dialog = new Form
            {
                Text = "Hotkeys",
                Size = new Size(500, 450),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = DarkBg,
                ForeColor = DarkText,
                Font = this.Font
            };

            Label titleLabel = new Label
            {
                Text = "Hotkeys",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                Location = new Point(20, 15),
                Size = new Size(440, 35),
                TextAlign = ContentAlignment.MiddleLeft
            };

            RichTextBox hotkeysBox = new RichTextBox
            {
                Text = hotkeys,
                Font = new Font("Consolas", 11),
                ForeColor = DarkText,
                BackColor = DarkInput,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, 60),
                Size = new Size(445, 300),
                ReadOnly = true,
                ScrollBars = RichTextBoxScrollBars.Vertical,
                WordWrap = false,
                DetectUrls = false
            };

            Button okButton = new Button
            {
                Text = "OK",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 120, 215),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Location = new Point(365, 370),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.OK
            };
            okButton.FlatAppearance.BorderSize = 0;
            okButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);

            dialog.Controls.AddRange(new Control[] { titleLabel, hotkeysBox, okButton });
            dialog.ShowDialog();
        }

        private void ShowAbout()
        {
            Form dialog = new Form
            {
                Text = "About",
                Size = new Size(500, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = DarkBg,
                ForeColor = DarkText,
                Font = this.Font
            };

            Label iconLabel = new Label
            {
                Text = "MD",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                Location = new Point(20, 20),
                Size = new Size(80, 80),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label titleLabel = new Label
            {
                Text = "Smart Markdown Converter",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                Location = new Point(110, 20),
                Size = new Size(350, 35),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label versionLabel = new Label
            {
                Text = "Version 1.0.2",
                Font = new Font("Segoe UI", 11),
                ForeColor = DarkText,
                Location = new Point(110, 55),
                Size = new Size(350, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label devLabel = new Label
            {
                Text = "Developer: Alexander Suvorov",
                Font = new Font("Segoe UI", 11),
                ForeColor = DarkText,
                Location = new Point(110, 80),
                Size = new Size(350, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label companyLabel = new Label
            {
                Text = "Smart Legion Lab",
                Font = new Font("Segoe UI", 11),
                ForeColor = DarkText,
                Location = new Point(110, 105),
                Size = new Size(350, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label copyrightLabel = new Label
            {
                Text = "© 2026 All rights reserved",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(150, 150, 150),
                Location = new Point(110, 130),
                Size = new Size(350, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            LinkLabel linkLabel = new LinkLabel
            {
                Text = "@smartlegionlab",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 150, 255),
                LinkColor = Color.FromArgb(0, 150, 255),
                ActiveLinkColor = Color.FromArgb(0, 200, 255),
                VisitedLinkColor = Color.FromArgb(0, 150, 255),
                Location = new Point(110, 160),
                Size = new Size(350, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };
            linkLabel.LinkClicked += (s, e) => {
                try
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "https://github.com/smartlegionlab/SmartMarkdownConverterCSharp",
                        UseShellExecute = true
                    };
                    System.Diagnostics.Process.Start(psi);
                }
                catch { }
            };

            Button okButton = new Button
            {
                Text = "OK",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(0, 120, 215),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Location = new Point(370, 210),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.OK
            };
            okButton.FlatAppearance.BorderSize = 0;
            okButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 180);

            dialog.Controls.AddRange(new Control[] {
                iconLabel, titleLabel, versionLabel, devLabel,
                companyLabel, copyrightLabel, linkLabel, okButton
            });
            dialog.ShowDialog();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int btnWidth = Math.Max(140, Math.Min(180, this.Width / 5 - 20));
            foreach (Control ctrl in bottomPanel.Controls)
            {
                if (ctrl is FlowLayoutPanel flow)
                {
                    foreach (Control btn in flow.Controls)
                    {
                        if (btn is Button)
                        {
                            btn.Width = btnWidth;
                        }
                    }
                }
            }
        }
    }
}