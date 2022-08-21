using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeikomiColorPicker.Models
{
    public class ColorNamePalletM : ModelBase
    {
        #region カラーリスト[ColorList]プロパティ
        /// <summary>
        /// カラーリスト[ColorList]プロパティ用変数
        /// </summary>
        ModelList<ColorNameM> _ColorList = new ModelList<ColorNameM>();
        /// <summary>
        /// カラーリスト[ColorList]プロパティ
        /// </summary>
        public ModelList<ColorNameM> ColorList
        {
            get
            {
                return _ColorList;
            }
            set
            {
                if (_ColorList == null || !_ColorList.Equals(value))
                {
                    _ColorList = value;
                    NotifyPropertyChanged("ColorList");
                }
            }
        }
        #endregion

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <param name="filename">ファイル名</param>
        public void Save(string filename)
        {
            XMLUtil.Seialize<ModelList<ColorNameM>>(filename, this.ColorList);
        }

        /// <summary>
        /// ロード処理
        /// </summary>
        /// <param name="filename">ファイル名</param>
        public void Load(string filename)
        {
            this.ColorList = XMLUtil.Deserialize<ModelList<ColorNameM>>(filename);
        }

    }
}
