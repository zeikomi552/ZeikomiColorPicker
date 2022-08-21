using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;

namespace ZeikomiColorPicker.Models
{
    public class ColorPickManagerM
    {
        /// <summary>
        /// 座標位置の色を取得
        /// </summary>
        /// <param name="corsor_pos">カーソル位置</param>
        /// <param name="basesize">元画像のサイズ(scaleと掛けて100になるようにすること)</param>
        /// <param name="scale">拡大率</param>
        /// <returns>色</returns>
        public static BitmapSource GetCursorBgImage(Point corsor_pos, int basesize = 25, int scale = 4)
        {
            int resize = basesize * scale;

            //1x1サイズのBitmap作成
            using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(
                basesize, basesize, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = System.Drawing.Graphics.FromImage(bitmap))
                {
                    //画面全体をキャプチャして指定座標の1ピクセルだけBitmapにコピー
                    bmpGraphics.CopyFromScreen(corsor_pos.X - (basesize / 2), corsor_pos.Y - (basesize / 2), 0, 0, new System.Drawing.Size(resize, resize));

                    //Penオブジェクトの作成(幅1黒色)
                    Pen p = new Pen(Color.Black, 1);

                    // 十字線を引く
                    bmpGraphics.DrawLine(p, (basesize / 2), 0, (basesize / 2), basesize);
                    bmpGraphics.DrawLine(p, 0, (basesize / 2), basesize, (basesize / 2));

                    // Bitmapのハンドルを取得し、
                    var hBitmap = bitmap.GetHbitmap();

                    // CreateBitmapSourceFromHBitmap()で System.Windows.Media.Imaging.BitmapSource に変換する
                    System.Windows.Media.Imaging.BitmapSource bitmapsource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                            hBitmap,
                                            IntPtr.Zero,
                                            Int32Rect.Empty,
                                            BitmapSizeOptions.FromEmptyOptions());

                    return bitmapsource;
                }
            }
        }

        /// <summary>
        /// 座標位置の色を取得
        /// </summary>
        /// <param name="corsor_pos">カーソル位置</param>
        /// <returns>色</returns>
        public static System.Windows.Media.Color GetPixelColor(Point corsor_pos)
        {
            //1x1サイズのBitmap作成
            using (System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(
                1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (var bmpGraphics = System.Drawing.Graphics.FromImage(bitmap))
                {
                    //画面全体をキャプチャして指定座標の1ピクセルだけBitmapにコピー
                    bmpGraphics.CopyFromScreen(corsor_pos.X, corsor_pos.Y, 0, 0, new System.Drawing.Size(1, 1));
                    //ピクセルの色取得
                    System.Drawing.Color color = bitmap.GetPixel(0, 0);
                    //WPF用にSystem.Windows.Media.Colorに変換して返す
                    return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
                }
            }
        }
    }
}
