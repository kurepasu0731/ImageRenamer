using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Reflection;
using System.IO;

namespace ImageCutterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string filepath = args[0];
                string parentdirpath = System.IO.Directory.GetParent(filepath).ToString();
                //フォルダ名を切り出し
                string[] patharray = filepath.Split('\\');
                string filename = patharray[patharray.GetLength(0) - 1];

                //出力先フォルダに付けるヘッダ
                string dstFolderHeader = " ";
                string dst_dirname = parentdirpath + "\\" + dstFolderHeader + filename;
                //画像に付ける名前
                string imageHeader = "cap";
                //出力画像のフォーマット
                string dst_format = "png";
                //開始インデックス
                int beginIndex = 0;


                //保存フォルダを作成(存在する場合は上書きする)
                // DANGER: System.IO.File.Create will overwrite the file if it already exists.
                System.IO.DirectoryInfo dstdir = System.IO.Directory.CreateDirectory(dst_dirname);

                //画像ファイルの取得
                string[] paths = System.IO.Directory.GetFiles(filepath, "*.jpg");

                //入力画像のフォーマットを検索
                //ファイル名だけ取り出す
                string[] dirs = paths[0].Split('\\');
                string imagefilename = dirs[dirs.GetLength(0) - 1];
                //拡張子だけ除く
                string[] imagefilenameSplit = imagefilename.Split('.');
                string src_format = imagefilenameSplit[1];
                Console.WriteLine("入力画像フォーマット：" + src_format);

                int imagenum = paths.Length;
                Console.WriteLine("画像数: " + imagenum);

                int currentnum = beginIndex;
                foreach (string imagepath in paths)
                {
                    //画像
                    Bitmap src_image = new Bitmap(imagepath);
                    //画像をリネームして保存
                    string newimagefilepath = dst_dirname + "\\" + imageHeader + currentnum + "." + dst_format;
                    Console.WriteLine("--" + newimagefilepath);

                    //各フォーマットで保存
                    if (dst_format.Equals("png"))
                        src_image.Save(newimagefilepath, System.Drawing.Imaging.ImageFormat.Png);
                    else if (dst_format.Equals("jpg"))
                        src_image.Save(newimagefilepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    else if (dst_format.Equals("bmp"))
                        src_image.Save(newimagefilepath, System.Drawing.Imaging.ImageFormat.Bmp);

                    src_image.Dispose();
                    currentnum++;
                }
            }
        }
    }
}
