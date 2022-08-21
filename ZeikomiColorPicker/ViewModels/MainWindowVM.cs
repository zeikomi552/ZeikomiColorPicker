using MVVMCore.BaseClass;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MVVMCore.Common.Utilities;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Windows.Media;
using ZeikomiColorPicker.Models;
using System.Windows.Media.Imaging;
using ZeikomiColorPicker.Common.Commands;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;

namespace ZeikomiColorPicker.ViewModels
{
	internal class MainWindowVM : ViewModelBase
	{
        /// <summary>
        ///  Escapeのコマンドオブジェクト
        /// </summary>
        public DelegateCommand TimerstopCommand { get; private set; }

        /// <summary>
        /// Insertのコマンドオブジェクト
        /// </summary>
        public DelegateCommand ColorInsertCommand { get; private set; }

        #region カーソル位置管理クラス[CursorPosManager]プロパティ
        /// <summary>
        /// カーソル位置管理クラス[CursorPosManager]プロパティ用変数
        /// </summary>
        CursorPosManagerM _CursorPosManager = new CursorPosManagerM();
        /// <summary>
        /// カーソル位置管理クラス[CursorPosManager]プロパティ
        /// </summary>
        public CursorPosManagerM CursorPosManager
        {
            get
            {
                return _CursorPosManager;
            }
            set
            {
                if (_CursorPosManager == null || !_CursorPosManager.Equals(value))
                {
                    _CursorPosManager = value;
                    NotifyPropertyChanged("CursorPosManager");
                }
            }
        }
        #endregion

        #region ビットマップイメージ[Image]プロパティ
        /// <summary>
        /// ビットマップイメージ[Image]プロパティ用変数
        /// </summary>
        BitmapSource? _Image;
        /// <summary>
        /// ビットマップイメージ[Image]プロパティ
        /// </summary>
        public BitmapSource? Image
        {
            get
            {
                return _Image;
            }
            set
            {
                if (_Image == null || !_Image.Equals(value))
                {
                    _Image = value;
                    NotifyPropertyChanged("Image");
                }
            }
        }
        #endregion

        #region RGBをテキスト表示するクラス[RGBManager]プロパティ
        /// <summary>
        /// RGBをテキスト表示するクラス[RGBManager]プロパティ用変数
        /// </summary>
        RGBManagerM _RGBManager = new RGBManagerM();
        /// <summary>
        /// RGBをテキスト表示するクラス[RGBManager]プロパティ
        /// </summary>
        public RGBManagerM RGBManager
        {
            get
            {
                return _RGBManager;
            }
            set
            {
                if (_RGBManager == null || !_RGBManager.Equals(value))
                {
                    _RGBManager = value;
                    NotifyPropertyChanged("RGBManager");
                }
            }
        }
        #endregion

        #region カラーパレット[ColorPallet]プロパティ
        /// <summary>
        /// カラーパレット[ColorPallet]プロパティ用変数
        /// </summary>
        ColorNamePalletM _ColorPallet = new ColorNamePalletM();
        /// <summary>
        /// カラーパレット[ColorPallet]プロパティ
        /// </summary>
        public ColorNamePalletM ColorPallet
        {
            get
            {
                return _ColorPallet;
            }
            set
            {
                if (_ColorPallet == null || !_ColorPallet.Equals(value))
                {
                    _ColorPallet = value;
                    NotifyPropertyChanged("ColorPallet");
                }
            }
        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowVM()
        {
            // コマンドオブジェクトと呼び出されるメソッドを関連付けます。
            TimerstopCommand = new DelegateCommand(TimerstopCommandBody);

            // 色の保存処理
            ColorInsertCommand = new DelegateCommand(ColorInsertCommandBody);
        }


        /// <summary>
        /// Escapeが押された時に呼び出されます
        /// </summary>
        /// <param name="sender"></param>
        private void TimerstopCommandBody(object sender)
        {
            if (this.CursorPosManager.IsExecute)
            {
                this.CursorPosManager.TimerStop();
            }
            else
            {
                this.CursorPosManager.TimerStart();
            }
        }

        /// <summary>
        /// Insertが押された時に呼び出されます
        /// </summary>
        /// <param name="sender"></param>
        private void ColorInsertCommandBody(object sender)
        {
            try
            {
                var tmp = new ColorNameM();
                tmp.SetColor(this.RGBManager.BgColor.Color);

                var target = (from x in this.ColorPallet.ColorList.Items
                              where x.Hex.Equals(tmp.Hex)
                              select x).ToList();

                var already_regist = target.Count > 0;

                // 未登録の場合
                if (!already_regist)
                {
                    this.ColorPallet.ColorList.Items.Add(tmp);
                    this.ColorPallet.ColorList.SelectedItem = this.ColorPallet.ColorList.Items.Last();

                    this._Dg!.ScrollIntoView(this._Dg!.Items[this._Dg!.Items.Count - 1]); // スクロール
                    this._Dg!.UpdateLayout();
                }
                // 既に登録されている場合
                else
                {
                    this.ColorPallet.ColorList.SelectedItem = target.First();
                    this._Dg!.ScrollIntoView(this.ColorPallet.ColorList.SelectedItem); // スクロール
                    this._Dg!.UpdateLayout();
                }

            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }

        DataGrid? _Dg;
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Init(object sender, EventArgs ev)
		{
            try
            {
                var wnd = sender as MainWindow;

                // nullチェック（nullのケースはない）
                if (wnd != null)
                {
                    this._Dg = wnd.ColorPalletDataGrid;
                }


                this.CursorPosManager.SetupTimer((s, e) =>
                {
                    this.RGBManager.BgColor = new SolidColorBrush(ColorPickManagerM.GetPixelColor(this.CursorPosManager.CursorPos));
                    this.Image = ColorPickManagerM.GetCursorBgImage(this.CursorPosManager.CursorPos);
                });
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }

        /// <summary>
        /// 10進数でコピー
        /// </summary>
        public void CopyRGB10()
        {
            try
            {
                Clipboard.SetData(DataFormats.Text, this.RGBManager.RGBText);
                ShowMessage.ShowNoticeOK("クリップボードにコピーしました。", "通知");
            }
            catch(Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }

        /// <summary>
        /// 16進数でコピー
        /// </summary>
        public void CopyRGB16()
        {
            try
            {
                Clipboard.SetData(DataFormats.Text, this.RGBManager.RGBHex);
                ShowMessage.ShowNoticeOK("クリップボードにコピーしました。", "通知");
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }


        /// <summary>
        /// 画面を閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		public override void Close(object sender, EventArgs e)
		{
            this.CursorPosManager.DisposeTimer();
        }

        /// <summary>
        /// パレットの保存処理
        /// </summary>
        public void SavePallet()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new SaveFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "カラーパレットファイル (*.zplt)|*.zplt";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    this.ColorPallet.Save(dialog.FileName);
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }

        /// <summary>
        /// スライダー変更時の処理
        /// </summary>
        public void SliderValueChanged()
        {
            try
            {
                //System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
                Console.WriteLine("test");
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }

        /// <summary>
        /// パレットのロード処理
        /// </summary>
        public void LoadPallet()
        {
            try
            {
                // ダイアログのインスタンスを生成
                var dialog = new OpenFileDialog();

                // ファイルの種類を設定
                dialog.Filter = "カラーパレットファイル (*.zplt)|*.zplt";

                // ダイアログを表示する
                if (dialog.ShowDialog() == true)
                {
                    this.ColorPallet.Load(dialog.FileName);
                }
            }
            catch (Exception e)
            {
                ShowMessage.ShowErrorOK(e.Message, "Error");
            }
        }
    }
}
