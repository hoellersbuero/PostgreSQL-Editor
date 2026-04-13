using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Controls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Expander))] // optional, zeigt Icon im Toolbox
    [Designer(typeof(ParentControlDesigner))] // erlaubt Drag&Drop von Child Controls im Designer
    public partial class Expander : UserControl
    {
        private int expandedContentHeight;
        private int targetHeight;
        private bool isAnimating;

        public Expander()
        {
            DoubleBuffered = true;
            InitializeComponent();

            IsExpanded = true;
            AnimationEnabled = true;
            AnimationInterval = 15;
            HeaderHeight = 28;
            BorderColor = SystemColors.ControlDark;
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            if (!isAnimating) return;

            int current = this.Height;
            int diff = targetHeight - current;
            int step = Math.Sign(diff) * Math.Max(1, Math.Abs(diff) / 6);

            int next = current + step;
            // Falls wir überschießen korrigieren
            if ((step > 0 && next >= targetHeight) || (step < 0 && next <= targetHeight))
                next = targetHeight;

            this.Height = next;

            // Content-Panel wird automatisch angepasst durch Dock = Fill
            if (this.Height == targetHeight)
            {
                StopAnimation();
            }
        }

        private void StopAnimation()
        {
            isAnimating = false;
            animTimer.Stop();
            UpdateChevron();
            OnToggled(EventArgs.Empty);
        }

        [Category("Layout"), Description("Panel zum Ablegen von Inhalten")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new Panel ContentPanel => contentPanel;

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded == value) return;
                _isExpanded = value;

                if (DesignMode)
                {
                    PerformExpandCollapseImmediate();
                    return;
                }

                if (AnimationEnabled)
                {
                    StartAnimation();
                }
                else
                {
                    PerformExpandCollapseImmediate();
                    OnToggled(EventArgs.Empty);
                }
            }
        }

        private void StartAnimation()
        {
            expandedContentHeight = GetContentPreferredHeight();
            int headerH = headerPanel.Height;
            targetHeight = IsExpanded ? headerH + expandedContentHeight : headerH;

            // Falls die Control momentan grösser ist (z. B. beim Expandieren), behalte aktuellen Height als Start
            isAnimating = true;
            animTimer.Interval = AnimationInterval;
            animTimer.Start();
        }

        private void PerformExpandCollapseImmediate()
        {
            int headerH = headerPanel.Height;
            if (IsExpanded)
            {
                this.Height = headerH + GetContentPreferredHeight();
            }
            else
            {
                this.Height = headerH;
            }
            UpdateChevron();
        }

        private int GetContentPreferredHeight()
        {
            int h = 0;
            foreach (Control c in contentPanel.Controls)
            {
                h = Math.Max(h, c.Bottom + c.Margin.Bottom);
            }
            // fallback wenn leer
            if (h == 0) h = 100;
            return h;
        }

        public string HeaderText
        {
            get => headerLabel.Text;
            set => headerLabel.Text = value;
        }

        private Color _borderColor;
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        private int _headerHeight;
        public int HeaderHeight
        {
            get => _headerHeight;
            set { _headerHeight = value; headerPanel.Height = value; Invalidate(); }
        }

        public bool AnimationEnabled { get; set; }
        public int AnimationInterval { get; set; }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            UpdateChevron();

            // Initialhöhe des Controls korrekt setzen
            if (!DesignMode)
            {
                if (IsExpanded)
                    this.Height = headerPanel.Height + GetContentPreferredHeight();
                else
                    this.Height = headerPanel.Height;
            }
        }

        private void Toggle()
        {
            IsExpanded = !IsExpanded;
        }

        private void UpdateChevron()
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                Point[] pts;
                if (IsExpanded)
                {
                    pts = new[] { new Point(4, 10), new Point(8, 6), new Point(12, 10) };
                }
                else
                {
                    pts = new[] { new Point(6, 4), new Point(10, 8), new Point(6, 12) };
                }
                using (Pen p = new Pen(ForeColor, 2))
                {
                    p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.DrawLines(p, pts);
                }
            }
            chevron.Image = bmp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var p = new Pen(BorderColor))
            {
                Rectangle r = ClientRectangle;
                r.Width -= 1; r.Height -= 1;
                e.Graphics.DrawRectangle(p, r);
            }
        }

        protected virtual void OnToggled(EventArgs e)
        {
            Toggled?.Invoke(this, e);
        }

        public event EventHandler Toggled;

        // Designer-gebundene Wrapper-Handler
        private void HeaderPanel_Click(object sender, EventArgs e) => Toggle();
        private void HeaderLabel_Click(object sender, EventArgs e) => Toggle();
        private void Chevron_Click(object sender, EventArgs e) => Toggle();
    }
}