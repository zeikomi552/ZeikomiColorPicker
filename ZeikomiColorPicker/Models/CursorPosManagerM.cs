using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ZeikomiColorPicker.Models
{
    public class CursorPosManagerM : ModelBase
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        /// <summary>
        /// タイマー処理
        /// </summary>
        DispatcherTimer? _Timer = null;

        #region カーソル位置[CursorPos]プロパティ
        /// <summary>
        /// カーソル位置[CursorPos]プロパティ用変数
        /// </summary>
        Point _CursorPos = new Point();
        /// <summary>
        /// カーソル位置[CursorPos]プロパティ
        /// </summary>
        public Point CursorPos
        {
            get
            {
                return _CursorPos;
            }
            set
            {
                if (!_CursorPos.Equals(value))
                {
                    _CursorPos = value;
                    NotifyPropertyChanged("CursorPos");
                }
            }
        }
        #endregion

        /// <summary>
        /// カーソルの現在位置（スクリーン座標）を取得する
        /// </summary>
        /// <returns>スクリーン座標</returns>
        public static Point GetMousePosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        /// <summary>
        /// Timer処理の開始
        /// </summary>
        public void TimerStart()
        {
            if (_Timer != null)
            {
                _Timer.Start();
                _Timer.Tag = true;
            }
        }

        /// <summary>
        /// Timer処理の停止
        /// </summary>
        public void TimerStop()
        {
            if (_Timer != null)
            {
                _Timer.Stop();
                _Timer.Tag = false;
            }
        }

        /// <summary>
        /// 実行中かどうかの判定
        /// </summary>
        public bool IsExecute
        {
            get
            {
                if (_Timer != null)
                {
                    return (bool)_Timer.Tag;
                }
                else
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// タイマーの初期化処理
        /// </summary>
        public void SetupTimer(System.EventHandler func)
        {
            if (_Timer == null)
            {
                // タイマのインスタンスを生成
                _Timer = new DispatcherTimer(DispatcherPriority.Normal)
                {
                    // インターバルを設定
                    Interval = TimeSpan.FromSeconds(0.1),
                };

                // タイマメソッドを設定（ラムダ式で記述）
                _Timer.Tick += (s, e) =>
                {
                    this.CursorPos = CursorPosManagerM.GetMousePosition();
                };

                // イベントの追加
                _Timer.Tick += func;

                // タイマを開始
                _Timer.Start();
                _Timer.Tag = true;  // タイマーの実行停止をtrue/falseで保持する
            }
        }
        public void DisposeTimer()
        {
            if (_Timer != null)
            {
                _Timer.Stop();
                _Timer = null;
            }
        }
    }
}
