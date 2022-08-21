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
                byte[] array = { this.BgColor.Color.R, this.BgColor.Color.G, this.BgColor.Color.B };
                string text = $"{BgColor}";
                return text;

            }
        }
        #endregion
    }
}
