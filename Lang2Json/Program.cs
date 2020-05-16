using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lang2Json
{
    class Program
    {
        /// <summary>
        /// main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            foreach (string item in FindFlile(args))
            {
                FileConvert(item);
            }
        }
        /// <summary>
        /// 查找文件，参数为空，则返回自身文件夹的文件
        /// </summary>
        /// <param name="filePaths">文件路径数组</param>
        /// <returns></returns>
        static string[] FindFlile(string[] filePaths)
        {
            List<string> list = new List<string>();
            if (filePaths.Length > 0)
            {
                foreach (string item in filePaths)
                {
                    list.Add(item);
                }
            }
            else
            {
                string[] files = Directory.GetFiles(".");
                foreach (string item in files)
                {
                    int split = item.LastIndexOf(".");
                    if (split > 0 && item.Substring(split) == ".lang")
                    {
                        list.Add(item);
                    }
                }
            }
            return list.ToArray();
        }
        /// <summary>
        /// 文件转换
        /// </summary>
        /// <param name="path">文件路径</param>
        static void FileConvert(string path)
        {
            string langText = File.ReadAllText(path);
            string jsonPath = path.Substring(0, path.Length - 4) + "json";
            File.WriteAllText(jsonPath, TextConvert(langText));
        }
        /// <summary>
        /// 字符转换
        /// </summary>
        /// <param name="langText">lang文本字符串</param>
        /// <returns>转换完成的字符串</returns>
        static string TextConvert(string langText)
        {
            //按换号符分割字符串并遍历处理
            string[] lines = langText.Split('\n');
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.Append(Environment.NewLine);
            for (int i = 0; i < lines.Length; i++)
            {
                //注释处理
                if (!lines[i].Contains('#'))
                {
                    if (lines[i].Trim().Length > 0)
                    {
                        sb.Append(LineConvert(lines[i], i == lines.Length - 2));
                    }
                }
                else
                {
                    string[] annotationSplit = lines[i].Split('#');
                    if (annotationSplit[0].Trim().Length <= 0)
                    {
                        sb.Append(lines[i]);
                    }
                    else
                    {
                        sb.Append(LineConvert(annotationSplit[0], i == lines.Length - 2));
                        sb.Append("#" + annotationSplit[1]);
                    }
                }
            }
            sb.Append("}");
            return sb.ToString();
        }
        /// <summary>
        /// 行转换，不规范则注释
        /// </summary>
        /// <param name="line">行字符串</param>
        /// <param name="isLast">是否是最后的元素</param>
        /// <returns>转换完成的字符串</returns>
        static string LineConvert(string line, bool isLast)
        {
            StringBuilder sb = new StringBuilder();
            string[] split = line.Split('=');
            if (split.Length == 2)
            {
                sb.Append("\"");
                sb.Append(split[0].Trim());
                sb.Append("\"");
                sb.Append(":");
                sb.Append("\"");
                sb.Append(split[1].Trim());
                sb.Append("\"");
                if (!isLast) sb.Append(",");
                sb.Append(Environment.NewLine);
                return sb.ToString();
            }
            else
            {
                return "#" + line;
            }
        }
    }
}
