using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;
using Color = System.Windows.Media.Color;

namespace ZeikomiColorPicker.Models
{
    public class ColorNameM : ModelBase
    {
		#region 色オブジェクト[ColorObject]プロパティ
		/// <summary>
		/// 色オブジェクト[ColorObject]プロパティ用変数
		/// </summary>
		Color _ColorObject = new Color();
		/// <summary>
		/// 色オブジェクト[ColorObject]プロパティ
		/// </summary>
		public Color ColorObject
		{
			get
			{
				return _ColorObject;
			}
			set
			{
				if (!_ColorObject.Equals(value))
				{
					_ColorObject = value;
					NotifyPropertyChanged("ColorObject");
					NotifyPropertyChanged("BgColor");
					NotifyPropertyChanged("Decimal");
					NotifyPropertyChanged("Hex");
				}
			}
		}
		#endregion

		#region 背景色
		/// <summary>
		/// 背景色
		/// </summary>
		public SolidColorBrush BgColor
        {
            get
            {
				return new SolidColorBrush(this.ColorObject);
			}
		}
		#endregion

		#region Webで定義されている名称[NameWeb]プロパティ
		/// <summary>
		/// Webで定義されている名称[NameWeb]プロパティ用変数
		/// </summary>
		string _NameWeb = string.Empty;
		/// <summary>
		/// Webで定義されている名称[NameWeb]プロパティ
		/// </summary>
		public string NameWeb
		{
			get
			{
				return _NameWeb;
			}
			set
			{
				if (_NameWeb == null || !_NameWeb.Equals(value))
				{
					_NameWeb = value;
					NotifyPropertyChanged("NameWeb");
				}
			}
		}
		#endregion

		#region 16進数[Hex]プロパティ
		/// <summary>
		/// 16進数[Hex]プロパティ
		/// </summary>
		public string Hex
		{
			get
			{
				byte[] array = { this.ColorObject.R, this.ColorObject.G, this.ColorObject.B };
				string text = $"{Convert.ToHexString(array)}";
				return text;
			}
		}
		#endregion

		#region 10進数[Decimal]プロパティ
		/// <summary>
		/// 10進数[Decimal]プロパティ
		/// </summary>
		public string Decimal
		{
			get
			{
				return $"({ColorObject.R},{ColorObject.G},{ColorObject.B})";
			}

		}
		#endregion

		#region 和名[NameJP]プロパティ
		/// <summary>
		/// 和名[NameJP]プロパティ用変数
		/// </summary>
		string _NameJP = string.Empty;
		/// <summary>
		/// 和名[NameJP]プロパティ
		/// </summary>
		public string NameJP
		{
			get
			{
				return _NameJP;
			}
			set
			{
				if (_NameJP == null || !_NameJP.Equals(value))
				{
					_NameJP = value;
					NotifyPropertyChanged("NameJP");
				}
			}
		}
		#endregion

		#region 英名[NameEN]プロパティ
		/// <summary>
		/// 英名[NameEN]プロパティ用変数
		/// </summary>
		string _NameEN = string.Empty;
		/// <summary>
		/// 英名[NameEN]プロパティ
		/// </summary>
		public string NameEN
		{
			get
			{
				return _NameEN;
			}
			set
			{
				if (_NameEN == null || !_NameEN.Equals(value))
				{
					_NameEN = value;
					NotifyPropertyChanged("NameEN");
				}
			}
		}
		#endregion

		#region 色の登録
		/// <summary>
		/// 色の登録
		/// </summary>
		/// <param name="color">色</param>
		public void SetColor(Color color)
        {
			this.ColorObject = color;
        }

		/// <summary>
		/// 色の登録
		/// </summary>
		/// <param name="color">色</param>
		public void SetColor(SolidColorBrush color)
		{
			SetColor(color.Color);
		}
		#endregion
	}
}
