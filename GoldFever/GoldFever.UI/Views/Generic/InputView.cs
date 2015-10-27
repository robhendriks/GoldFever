using System;

namespace GoldFever.UI.Views.Generic
{
    public class InputView : View
    {
        #region Constants

        private const int MinSize = 8,
                          MaxSize = 32;

        #endregion


        #region Properties

        private int _size;

        public int Size
        {
            get { return _size; }
            set
            {
                var tmp = _size;

                if (value < MinSize)
                    _size = MinSize;
                else if (value > MaxSize)
                    _size = MaxSize;
                else
                    _size = value;

                if(_size != tmp)
                    OnSizeChanged();
            }
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set
            {
                var tmp = _value;

                if (value == null)
                    _value = string.Empty;
                else if (value.Length > _size)
                    _value = value.Substring(0, _size);
                else
                    _value = value;

                if (_value != tmp)
                    OnValueChanged();
            }
        }

        #endregion


        #region Constructors

        public InputView(string title, string text)
            : base(title, text)
        {
            _size = MinSize;
            _value = "";
        }

        #endregion


        #region Methods

        private void AddToValue(char c)
        {
            if (_value.Length == _size)
                return;

            _value += c;
        }

        private void RemoveFromValue()
        {
            if (_value.Length == 0)
                return;

            _value = _value.Substring(0, _value.Length - 1);
        }

        private bool HandleKey(ConsoleKeyInfo info)
        {
            var c = info.KeyChar;

            if(char.IsLetterOrDigit(c))
            {
                AddToValue(c);
                return true;
            }
            else if(info.Key == ConsoleKey.Backspace)
            {
                RemoveFromValue();
                return true;
            }

            return false;
        }

        public override void Update(ConsoleKeyInfo info)
        {
            if (!HandleKey(info))
                base.Update(info);

            Invalidate();
        }

        public override void Draw()
        {
            base.Draw();

            Console.Write("\n");

            var str = _value.PadRight(_size, '.');
            WriteLine(str);
        }

        #endregion


        #region Events

        protected void OnSizeChanged()
        {
            if (SizeChanged != null)
                SizeChanged(this);

            Invalidate();
        }

        protected void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this);

            Invalidate();
        }

        public event SizeChangedHandler SizeChanged;
        public event ValueChangedHandler ValueChanged;

        public delegate void SizeChangedHandler(object sender);
        public delegate void ValueChangedHandler(object sender);

        #endregion
    }
}
