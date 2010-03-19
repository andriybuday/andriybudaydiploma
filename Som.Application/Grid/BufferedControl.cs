using System;
using System.Drawing;
using System.Windows.Forms;

namespace Som.Application.Grid
{
    [ToolboxBitmap("Som.Application.Grid.BufferedControl.ico")]
    public class BufferedControl : UserControl
    {
        protected IBufferedControlController Controller { get; set; }

        private bool _dirty;
        private BufferedGraphicsContext BufferContext;
        private BufferedGraphics Buffer;

        public BufferedControl()
        {
        }

        public BufferedControl(IBufferedControlController controller)
        {
            Controller = controller;
            BufferContext = new BufferedGraphicsContext();
            SizeGraphicsBuffer();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.DoubleBuffer, false);
        }

        private void SizeGraphicsBuffer()
        {
            if (Buffer != null)
            {
                Buffer.Dispose();
                Buffer = null;
            }

            if (BufferContext == null)
                return;

            if (DisplayRectangle.Width <= 0)
                return;

            if (DisplayRectangle.Height <= 0)
                return;

            using (Graphics graphics = CreateGraphics())
                Buffer = BufferContext.Allocate(graphics, DisplayRectangle);

            Dirty = true;
        }

        public bool Dirty
        {
            get { return _dirty; }
            set
            {
                if (!value)
                    return;

                _dirty = true;
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        { /* Do Nothing */ }

        protected override void OnSizeChanged(EventArgs e)
        {
            SizeGraphicsBuffer();
            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Buffer == null)
            {
                Draw(e.Graphics);
                return;
            }

            if (_dirty)
            {
                _dirty = false;
                Draw(Buffer.Graphics);
            }

            Buffer.Render(e.Graphics);
        }

        public virtual void Draw(Graphics graphics)
        {
            if (Controller == null || ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0) return;

            Controller.Draw(graphics, ClientRectangle.Width, ClientRectangle.Height);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Buffer != null)
                {
                    Buffer.Dispose();
                    Buffer = null;
                }

                if (BufferContext != null)
                {
                    BufferContext.Dispose();
                    BufferContext = null;
                }

                if (_TextFont != null)
                {
                    _TextFont.Dispose();
                    _TextFont = null;
                }
            }
            base.Dispose(disposing);
        }

        /* Code below here is just for the ability to
         * drag around the string.
         */

        private Rectangle _CurrentTextRectangle = new Rectangle(10, 10, 0, 0);
        private Font _TextFont = new Font("Tahoma", 12);
        private Point _OrigMousePoint = Point.Empty;
        private Point _OrigTextPoint = Point.Empty;
        private bool _Moving = false;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left)
                return;

            _Moving = false;
            if (!_CurrentTextRectangle.Contains(e.Location))
                return;

            _OrigMousePoint = e.Location;
            _OrigTextPoint = _CurrentTextRectangle.Location;
            _Moving = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button != MouseButtons.Left)
                return;

            _Moving = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!_Moving)
            {
                Cursor = _CurrentTextRectangle.Contains(e.Location) ?
                    Cursors.Hand : Cursors.Default;
                return;
            }

            _CurrentTextRectangle.Location = new Point(
              _OrigTextPoint.X + (e.X - _OrigMousePoint.X),
              _OrigTextPoint.Y + (e.Y - _OrigMousePoint.Y));
            Dirty = true;
        }
    }
}
