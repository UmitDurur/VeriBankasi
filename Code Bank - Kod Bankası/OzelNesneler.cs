using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace VeriBankasi
{
    public partial class OzelListbox : ListBox
    {
        public event EventHandler<IndexEventArgs> DisabledItemSelected;
        protected virtual void OnDisabledItemSelected(object sender, IndexEventArgs e)
        {
            if (DisabledItemSelected != null)
            {
                DisabledItemSelected(sender, e);
            }
        }
        public OzelListbox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            DrawMode = DrawMode.OwnerDrawFixed;
            disabledIndices = new DisabledIndexCollection(this);
        }

        private int originalHeight = 0;
        private bool fontChanged = false;

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            fontChanged = true;
            this.ItemHeight = FontHeight;
            this.Height = GetPreferredHeight();
            fontChanged = false;

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!fontChanged)
                this.originalHeight = this.Height;
        }

        public void DisableItem(int index)
        {
            disabledIndices.Add(index);
        }

        public void EnableItem(int index)
        {
            disabledIndices.Remove(index);
        }


        /*protected override void OnSelectedIndexChanged(EventArgs e)
        {
            int currentSelectedIndex = SelectedIndex;
            List<int> selectedDisabledIndices = new List<int>();

            for (int i = 0; i < SelectedIndices.Count; i++)
            {
                if (disabledIndices.Contains(SelectedIndices[i]))
                {
                    selectedDisabledIndices.Add(SelectedIndices[i]);
                    SelectedIndices.Remove(SelectedIndices[i]);
                }
            }
            foreach (int index in selectedDisabledIndices)
            {
                IndexEventArgs args = new IndexEventArgs(index);
                OnDisabledItemSelected(this, args);
            }
            if (currentSelectedIndex == SelectedIndex)
                base.OnSelectedIndexChanged(e);
        }*/


        private int GetPreferredHeight()
        {
            if (!IntegralHeight)
                return this.Height;

            int currentHeight = this.originalHeight;
            int preferredHeight = PreferredHeight;
            if (currentHeight < preferredHeight)
            {
                // Calculate how many items currentheigh can hold.
                int number = currentHeight / ItemHeight;

                if (number < Items.Count)
                {
                    preferredHeight = number * ItemHeight;
                    int delta = currentHeight - preferredHeight;
                    if (ItemHeight / 2 <= delta)
                    {
                        preferredHeight += ItemHeight;
                    }
                    preferredHeight += (SystemInformation.BorderSize.Height * 4) + 3;
                }
                else
                {
                    preferredHeight = currentHeight;
                }
            }
            else
                preferredHeight = currentHeight;

            return preferredHeight;

        }
        public class Nesne
        {
            private int Id = -1;
            public int id { get { return Id; } set { Id = value; } }

            private string name = "";
            public string isim { get { return name; } set { name = value; } }

            public int katman { get { return Katman; } set { Katman = value; } }
            private int Katman;

            public int is_klasor { get { return Is_klasor; } set { Is_klasor = value; } }
            private int Is_klasor;

            public string icerik { get { return Icerik; } set { Icerik = value; } }
            private string Icerik = "";

            public bool acik { get { return Acik; } set { Acik = value; } }
            private bool Acik;

            public char sifreli { get { return Sifreli; } set { Sifreli = value; } }
            private char Sifreli;

            public char durum { get; set; }
        }
        Nesne globalnesne;
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (DesignMode && Items.Count == 0)
            {
                return;
            }
            if (e.Index != OzelListbox.NoMatches)
            {
                if (Items[e.Index] is Nesne)
                {
                    Nesne a = (Nesne)Items[e.Index];
                    Image itemImage, itemLocked=null;
                    if (a.durum == 'v')
                    {
                        globalnesne = a;
                        TextBox tt = new TextBox();
                        tt.Size = new Size(e.Bounds.Size.Width - 50, e.Bounds.Size.Height);
                        tt.Location = new Point(e.Bounds.Location.X + 17, e.Bounds.Location.Y);
                        tt.Text = a.isim;
                        a.durum = 'a';
                        tt.Name = e.Index.ToString();
                        tt.KeyDown += new KeyEventHandler(textdegis);
                        for (int i = 0; i < this.Items.Count; i++)
                            this.DisableItem(i);
                        this.Controls.Add(tt);
                        tt.Focus();
                    }
                    if (a.is_klasor == 1 && a.acik == false)
                    {
                        itemImage = Properties.Resources.dosya_kapali;
                    }
                    else if (a.is_klasor == 1 && a.acik == true)
                        itemImage = Properties.Resources.dosya_acik;
                    else if (a.is_klasor == 0) { itemImage = Properties.Resources.alt_klasor; }
                    else itemImage = null;

                    if (a.sifreli == '1') itemLocked = Properties.Resources.kilitkapali;
                    else if (a.sifreli == '2') itemLocked = Properties.Resources.kilitacik;

                    int gnslk = (Convert.ToInt16(a.katman) - 1) * 10;
                    if (disabledIndices.Contains(e.Index))
                    {
                        e.Graphics.FillRectangle(SystemBrushes.InactiveBorder, e.Bounds);
                        if (!string.IsNullOrEmpty(a.isim)) TextRenderer.DrawText(e.Graphics, a.isim, e.Font, new Rectangle(ItemHeight + gnslk + 2, e.Bounds.Top, e.Bounds.Width - (ItemHeight + 2), ItemHeight), SystemColors.GrayText, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                    }
                    else
                    {
                        if (SelectionMode == System.Windows.Forms.SelectionMode.None)
                        {
                            e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                            if (!string.IsNullOrEmpty(a.isim)) TextRenderer.DrawText(e.Graphics, a.isim, e.Font, new Rectangle(ItemHeight + gnslk + 2, e.Bounds.Top, e.Bounds.Width - (ItemHeight + 2), ItemHeight), SystemColors.WindowText, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                        }
                        else
                        {
                            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                            {
                                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                                e.DrawFocusRectangle();
                                if (!string.IsNullOrEmpty(a.isim)) TextRenderer.DrawText(e.Graphics, a.isim, e.Font, new Rectangle(ItemHeight + gnslk + 2, e.Bounds.Top, e.Bounds.Width - (ItemHeight + 2), ItemHeight), SystemColors.HighlightText, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                            }
                            else
                            {
                                e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                                if (!string.IsNullOrEmpty(a.isim)) TextRenderer.DrawText(e.Graphics, a.isim, e.Font, new Rectangle(ItemHeight + gnslk + 2, e.Bounds.Top, e.Bounds.Width - (ItemHeight + 2), ItemHeight), SystemColors.WindowText, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                            }
                        }
                    }
                    if (itemImage != null) e.Graphics.DrawImage(itemImage, e.Bounds.Left + gnslk, e.Bounds.Top, ItemHeight, ItemHeight);
                    if (itemLocked != null) e.Graphics.DrawImage(itemLocked, e.Bounds.Right - itemLocked.Width - 2, e.Bounds.Top, ItemHeight, ItemHeight);

                }
                else TextRenderer.DrawText(e.Graphics, Items[e.Index].ToString(), e.Font, e.Bounds, e.ForeColor);
            }
        }
        protected void textdegis(object sender, KeyEventArgs e)
        {
            var kopya = (TextBox)sender;
            if (e.KeyCode == Keys.Escape)
            {
                foreach (TextBox tx in this.Controls)
                {
                    globalnesne = null;
                    tx.Dispose();
                }
                for (int i = 0; i < this.Items.Count; i++)
                    this.EnableItem(i);
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.Items.RemoveAt(Convert.ToInt16(kopya.Name));

                foreach (TextBox tx in this.Controls)
                {
                    globalnesne.isim = tx.Text;
                    this.Items.Insert(Convert.ToInt16(kopya.Name), globalnesne);
                    Program.Anaform.adguncelle(globalnesne);
                    globalnesne = null;
                    tx.Dispose();
                }
                for (int i = 0; i < this.Items.Count; i++)
                    this.EnableItem(i);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int i = 0; i < Items.Count; i++)
            {
                Rectangle itemRect = GetItemRectangle(i);
                if (e.ClipRectangle.IntersectsWith(itemRect))
                {
                    if (SelectedIndices.Contains(i))
                    {
                        DrawItemEventArgs diea = new DrawItemEventArgs(e.Graphics, this.Font, itemRect, i, DrawItemState.Selected);
                        OnDrawItem(diea);
                    }
                    else
                    {
                        DrawItemEventArgs diea = new DrawItemEventArgs(e.Graphics, this.Font, itemRect, i, DrawItemState.None);
                        OnDrawItem(diea);
                    }
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button != MouseButtons.None) this.Invalidate(true);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.Invalidate(true);
            if (e.Button == MouseButtons.Right)
            {
                int index = this.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    if (!disabledIndices.Contains(index))
                    {
                        this.SetSelected(index,true);
                    }
                    else this.ClearSelected();
                }
                else this.ClearSelected();
            }
        }

        private DisabledIndexCollection disabledIndices;

        public DisabledIndexCollection DisabledIndices
        {
            get { return disabledIndices; }
        }

        public class DisabledIndexCollection : IList, ICollection, IEnumerable
        {
            // Fields
            private ListBox owner;
            private List<int> innerList = new List<int>();


            // Methods
            public DisabledIndexCollection(ListBox owner)
            {
                this.owner = owner;
            }

            public void Add(int index)
            {
                if (((this.owner != null) && (this.owner.Items != null)) && ((index != -1) && !this.Contains(index)))
                {
                    innerList.Add(index);
                    this.owner.SetSelected(index, false);
                }
            }

            public void Clear()
            {
                if (this.owner != null)
                {
                    innerList.Clear();
                }
            }

            public bool Contains(int selectedIndex)
            {
                return (this.IndexOf(selectedIndex) != -1);
            }

            public void CopyTo(Array destination, int index)
            {
                int count = this.Count;
                for (int i = 0; i < count; i++)
                {
                    destination.SetValue(this[i], (int)(i + index));
                }
            }

            public IEnumerator GetEnumerator()
            {
                return new SelectedIndexEnumerator(this);
            }

            public int IndexOf(int selectedIndex)
            {
                if ((selectedIndex >= 0) && (selectedIndex < this.owner.Items.Count))
                {
                    for (int index = 0; index < innerList.Count; index++)
                    {
                        if (innerList[index] == selectedIndex)
                            return index;
                    }
                }
                return -1;
            }

            public void Remove(int index)
            {
                if (((this.owner != null) && (this.owner.Items != null)) && ((index != -1) && this.Contains(index)))
                {
                    innerList.Remove(index);
                    this.owner.SetSelected(index, false);
                }
            }

            int IList.Add(object value)
            {
                throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
            }

            void IList.Clear()
            {
                throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
            }

            bool IList.Contains(object selectedIndex)
            {
                return ((selectedIndex is int) && this.Contains((int)selectedIndex));
            }

            int IList.IndexOf(object selectedIndex)
            {
                if (selectedIndex is int)
                {
                    return this.IndexOf((int)selectedIndex);
                }
                return -1;
            }

            void IList.Insert(int index, object value)
            {
                throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
            }

            void IList.Remove(object value)
            {
                throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
            }

            void IList.RemoveAt(int index)
            {
                throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
            }

            // Properties
            [Browsable(false)]
            public int Count
            {
                get
                {
                    return this.innerList.Count;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public int this[int index]
            {
                get
                {
                    return IndexOf(index);
                }
            }

            bool ICollection.IsSynchronized
            {
                get
                {
                    return true;
                }
            }

            object ICollection.SyncRoot
            {
                get
                {
                    return this;
                }
            }

            bool IList.IsFixedSize
            {
                get
                {
                    return true;
                }
            }

            object IList.this[int index]
            {
                get
                {
                    return this[index];
                }
                set
                {
                    throw new NotSupportedException("ListBoxSelectedIndexCollectionIsReadOnly");
                }
            }

            // Nested Types
            private class SelectedIndexEnumerator : IEnumerator
            {
                // Fields
                private int current;
                private OzelListbox.DisabledIndexCollection items;

                // Methods
                public SelectedIndexEnumerator(OzelListbox.DisabledIndexCollection items)
                {
                    this.items = items;
                    this.current = -1;
                }

                bool IEnumerator.MoveNext()
                {
                    if (this.current < (this.items.Count - 1))
                    {
                        this.current++;
                        return true;
                    }
                    this.current = this.items.Count;
                    return false;
                }

                void IEnumerator.Reset()
                {
                    this.current = -1;
                }

                // Properties
                object IEnumerator.Current
                {
                    get
                    {
                        if ((this.current == -1) || (this.current == this.items.Count))
                        {
                            throw new InvalidOperationException("ListEnumCurrentOutOfRange");
                        }
                        return this.items[this.current];
                    }
                }
            }
        }

        public new void SetSelected(int index, bool value)
        {
            int num = (this.Items == null) ? 0 : this.Items.Count;
            if ((index < 0) || (index >= num))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (this.SelectionMode == SelectionMode.None)
            {
                throw new InvalidOperationException("ListBoxInvalidSelectionMode");
            }
            if (!disabledIndices.Contains(index))
            {
                if (!value)
                {
                    if (SelectedIndices.Contains(index))
                        SelectedIndices.Remove(index);
                }
                else
                {
                    base.SetSelected(index, value);
                }
            }
            // Selected index deoes not change, however we should redraw the disabled item.
            else
            {
                if (!value)
                {
                    // Remove selected item if it is in the list of selected indices.
                    if (SelectedIndices.Contains(index))
                        SelectedIndices.Remove(index);
                }

            }
            Invalidate(GetItemRectangle(index));
        }
    }
    public class IndexEventArgs : EventArgs
    {
        private int index;
        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }
        public IndexEventArgs(int index)
        {
            Index = index;
        }
    }


    class TransparanPanel : Panel
    {
     
        public bool drag = false;
        public bool enab = false;
        private int m_opacity = 100;

        private int alpha;
        public TransparanPanel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
        }

        public int Opacity
        {
            get
            {
                if (m_opacity > 100)
                {
                    m_opacity = 100;
                }
                else if (m_opacity < 1)
                {
                    m_opacity = 1;
                }
                return this.m_opacity;
            }
            set
            {
                this.m_opacity = value;
                if (this.Parent != null)
                {
                    Parent.Invalidate(this.Bounds, true);
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            Color frmColor = this.Parent.BackColor;
            Brush bckColor = default(Brush);

            alpha = (m_opacity * 255) / 100;

            if (drag)
            {
                Color dragBckColor = default(Color);

                if (BackColor != Color.Transparent)
                {
                    int Rb = BackColor.R * alpha / 255 + frmColor.R * (255 - alpha) / 255;
                    int Gb = BackColor.G * alpha / 255 + frmColor.G * (255 - alpha) / 255;
                    int Bb = BackColor.B * alpha / 255 + frmColor.B * (255 - alpha) / 255;
                    dragBckColor = Color.FromArgb(Rb, Gb, Bb);
                }
                else
                {
                    dragBckColor = frmColor;
                }

                alpha = 255;
                bckColor = new SolidBrush(Color.FromArgb(alpha, dragBckColor));
            }
            else
            {
                bckColor = new SolidBrush(Color.FromArgb(alpha, this.BackColor));
            }

            if (this.BackColor != Color.Transparent | drag)
            {
                g.FillRectangle(bckColor, bounds);
            }

            bckColor.Dispose();
            g.Dispose();
            base.OnPaint(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (this.Parent != null)
            {
                Parent.Invalidate(this.Bounds, true);
            }
            base.OnBackColorChanged(e);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnParentBackColorChanged(e);
        }
    }

    class TransparanLabel : Panel
    {
        public bool drag = false;
        public bool enab = false;
        private int m_opacity = 100;
        private int x = 0, y = 0;
        private string text = "Text girin";
        public TransparanLabel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
        }

        public int Opacity
        {
            get
            {
                if (m_opacity > 100)
                {
                    m_opacity = 100;
                }
                else if (m_opacity < 1)
                {
                    m_opacity = 1;
                }
                return this.m_opacity;
            }
            set
            {
                this.m_opacity = value;
                if (this.Parent != null)
                    Parent.Invalidate(this.Bounds, true);
            }
        }

        public string Textmessage
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                if (this.Parent != null)
                {
                    this.Invalidate();
                }
            }
        }

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            /*Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            Color frmColor = this.Parent.BackColor;
            Brush bckColor = default(Brush);

            alpha = (m_opacity * 255) / 100;

            if (drag)
            {
                Color dragBckColor = default(Color);

                if (BackColor != Color.Transparent)
                {
                    int Rb = BackColor.R * alpha / 255 + frmColor.R * (255 - alpha) / 255;
                    int Gb = BackColor.G * alpha / 255 + frmColor.G * (255 - alpha) / 255;
                    int Bb = BackColor.B * alpha / 255 + frmColor.B * (255 - alpha) / 255;
                    dragBckColor = Color.FromArgb(Rb, Gb, Bb);
                }
                else
                {
                    dragBckColor = frmColor;
                }

                alpha = 255;
                bckColor = new SolidBrush(Color.FromArgb(alpha, dragBckColor));
            }
            else
            {
                bckColor = new SolidBrush(Color.FromArgb(alpha, this.BackColor));
            }

            if (this.BackColor != Color.Transparent | drag)
            {
                g.FillRectangle(bckColor, bounds);
            }

            bckColor.Dispose();
            g.Dispose();
            base.OnPaint(e);*/

            Font font = new Font("Microsoft Sans Serif", 12F,
                           System.Drawing.FontStyle.Bold,
                           System.Drawing.GraphicsUnit.Point,
                           ((byte)(0)));
            SizeF stringSize = new SizeF();
            String str = text;
            stringSize = e.Graphics.MeasureString(str, font, (int)stringSize.Height);
            x = (int)stringSize.Height;
            y = (int)stringSize.Width;
            g.DrawString(str, font, System.Drawing.Brushes.Black, new Point(0, 0));
            this.Height = x;
            this.Width = y;

            base.OnPaint(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Invalidate();
            }
            base.OnBackColorChanged(e);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            if (this.Parent != null)
            {
                this.Invalidate();
            }
            base.OnTextChanged(e);
        }
        protected override void OnParentBackColorChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnParentBackColorChanged(e);
        }

        void guncelle()
        {
            this.BackColor = Color.Transparent;

        }
    }
}