using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ZeikomiColorPicker.Models
{
    public class RGBManagerM : ModelBase
    {
        #region 背景色[BgColor]プロパティ
        /// <summary>
        /// 背景色[BgColor]プロパティ用変数
        /// </summary>
        SolidColorBrush _BgColor = new SolidColorBrush();
        /// <summary>
        /// 背景色[BgColor]プロパティ
        /// </summary>
        public SolidColorBrush BgColor
        {
            get
            {
                return _BgColor;
            }
            set
            {
                if (_BgColor == null || !_BgColor.Equals(value))
                {
                    _BgColor = value;
					NotifyPropertyChanged("BgColor");
                    NotifyPropertyChanged("RGBText");
                    NotifyPropertyChanged("RGBHex");
                    NotifyPropertyChanged("R");
                    NotifyPropertyChanged("G");
                    NotifyPropertyChanged("B");
				}
            }
        }
        #endregion

        #region RGB文字列
        /// <summary>
        /// RGB文字列
        /// </summary>
        public string RGBText
        {
            get
            {
                string text = $"({this.BgColor.Color.R.ToString()}, {this.BgColor.Color.G.ToString()}, {this.BgColor.Color.B.ToString()})";
                return text;

            }
        }
        #endregion

        #region RGBをHEXで表したもの
        /// <summary>
        /// RGBをHEXで表したもの
        /// </summary>
        public string RGBHex
        {
            get
            {
                byte[] array = { R, G, B };
                string text = $"{BgColor}";
                return text;

            }
        }
		#endregion
		#region R[R]プロパティ
		/// <summary>
		/// R[R]プロパティ
		/// </summary>
		public byte R
		{
			get
			{
				return this.BgColor.Color.R;
			}
			set
			{
				if (!this.BgColor.Color.R.Equals(value))
				{
					BgColor.Color = System.Windows.Media.Color.FromArgb(BgColor.Color.A, value, BgColor.Color.G, BgColor.Color.B);
					NotifyPropertyChanged("R");
					NotifyPropertyChanged("BgColor");
					NotifyPropertyChanged("RGBText");
					NotifyPropertyChanged("RGBHex");
				}
			}
		}
		#endregion

		#region G[G]プロパティ
		/// <summary>
		/// G[G]プロパティ
		/// </summary>
		public byte G
		{
			get
			{
				return this.BgColor.Color.G;
			}
			set
			{
				if (!this.BgColor.Color.G.Equals(value))
				{
					BgColor.Color = System.Windows.Media.Color.FromArgb(BgColor.Color.A, BgColor.Color.R, value, BgColor.Color.B);
					NotifyPropertyChanged("G");
					NotifyPropertyChanged("BgColor");
					NotifyPropertyChanged("RGBText");
					NotifyPropertyChanged("RGBHex");
				}
			}
		}
		#endregion

		#region B[B]プロパティ
		/// <summary>
		/// B[B]プロパティ
		/// </summary>
		public byte B
		{
			get
			{
				return this.BgColor.Color.B;
			}
			set
			{
				if (!this.BgColor.Color.B.Equals(value))
				{
					BgColor.Color = System.Windows.Media.Color.FromArgb(BgColor.Color.A, BgColor.Color.R, BgColor.Color.G, value);
					NotifyPropertyChanged("B");
					NotifyPropertyChanged("BgColor");
					NotifyPropertyChanged("RGBText");
					NotifyPropertyChanged("RGBHex");
				}
			}
		}
		#endregion


	}
}
