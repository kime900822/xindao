using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace CashBorrowINFO.CS
{
    public static class Help
    {
        #region 服务器上传文件

        /// <summary> 
        /// 上传 
        /// </summary> 
        /// <param name="filename">要上传的本地文件名</param> 
        public static string Upload(string filename,string ftpURI,string ftpUserID, string ftpPassword)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpURI + fileInf.Name;
            FtpWebRequest reqFTP;
            

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Proxy = null;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary> 
        /// 下载 
        /// </summary> 
        /// <param name="filename">要上传的本地文件名</param> 
        public static string Download(string filename, string ftpURI, string ftpUserID, string ftpPassword)
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpURI + fileInf.Name;
            FtpWebRequest reqFTP;


            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Proxy = null;
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        /// <summary>
        /// 从FTP服务器下载文件，返回文件二进制数据
        /// </summary>
        public static byte[] DownloadFile(String ftpPath, String LocalPath,string ftpUserID,string ftpPassword)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpPath);
                ftpRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);//登陆ftp的用户名，密码
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse Response = (FtpWebResponse)ftpRequest.GetResponse();
                Stream Reader = Response.GetResponseStream();

                MemoryStream mem = new MemoryStream(1024 * 500);
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                int TotalByteRead = 0;
                while (true)
                {
                    bytesRead = Reader.Read(buffer, 0, buffer.Length);
                    TotalByteRead += bytesRead;
                    if (bytesRead == 0)
                        break;
                    mem.Write(buffer, 0, bytesRead);
                }
                if (mem.Length > 0)
                {
                    return mem.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ep)
            {
                throw ep;
            }
        }


        #endregion



        /// <summary>
        /// 二进制转图片
        /// </summary>
        /// <param name="streamByte"></param>
        /// <returns></returns>
        public static System.Drawing.Image ReturnPhoto(byte[] streamByte)
        {
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
            //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            //return img;
            //将内存流格式化为位图
            if (streamByte.Length == 0)
            {
                return null;
            }
            else {
                MemoryStream stream = new MemoryStream(streamByte); //内存流
                Bitmap bitmap = new Bitmap(stream);
                stream.Close();
                return bitmap;
            }

        }


        /// <summary>
        /// 图片转流
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] ReturnByte(Image image) {
            byte[] bt = null;

            if (!image.Equals(null))

            {

                using (MemoryStream mostream = new MemoryStream())

                {

                    Bitmap bmp = new Bitmap(image);

                    bmp.Save(mostream, System.Drawing.Imaging.ImageFormat.Bmp);//将图像以指定的格式存入缓存内存流

                    bt = new byte[mostream.Length];

                    mostream.Position = 0;//设置留的初始位置

                    mostream.Read(bt, 0, Convert.ToInt32(bt.Length));

                }

            }

            return bt;

            //FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);//可读

            ////将文件流中的数据存入内存字节组中
            //byte[] buffer = new byte[stream.Length];
            //stream.Read(buffer, 0, (int)stream.Length);
            //stream.Close();
            //return buffer;
        }


        /// <summary>
        /// 扣款
        /// </summary>
        /// <returns></returns>
        public static bool Debit(string u_sysid,string value) {

            return true;
        }



        /// <summary>
        /// MD5 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Key_64"></param>
        /// <param name="Iv_64"></param>
        /// <returns></returns>
        public static string Decode(string data, string Key_64, string Iv_64)

        {

            string KEY_64 = Key_64;// "VavicApp";密钥

            string IV_64 = Iv_64;// "VavicApp"; 向量

            try

            {

                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);

                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                byte[] byEnc;

                byEnc = Convert.FromBase64String(data); //把需要解密的字符串转为8位无符号数组

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

                MemoryStream ms = new MemoryStream(byEnc);

                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);

                StreamReader sr = new StreamReader(cst);

                return sr.ReadToEnd();

            }

            catch (Exception x)

            {

                return x.Message;

            }

        }


        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Key_64"></param>
        /// <param name="Iv_64"></param>
        /// <returns></returns>
        public static string Encode(string data, string Key_64, string Iv_64)

        {

            string KEY_64 = Key_64;// "VavicApp";

            string IV_64 = Iv_64;// "VavicApp";

            try

            {

                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);

                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

                int i = cryptoProvider.KeySize;

                MemoryStream ms = new MemoryStream();

                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

                StreamWriter sw = new StreamWriter(cst);

                sw.Write(data);

                sw.Flush();

                cst.FlushFinalBlock();

                sw.Flush();

                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

            }

            catch (Exception x)

            {

                return x.Message;

            }

        }


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="URL">下载文件地址</param>
        /// <param name="Filename">下载后的存放地址</param>
        /// <param name="Prog">用于显示的进度条</param>
        public static int DownloadFile(string URL, string filename, System.Windows.Forms.ProgressBar prog)
        {
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }

                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);

                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);
                }
                so.Close();
                st.Close();
                return 1;
            }
            catch (System.Exception)
            {
                throw;
                return 0;
            }
        }


        public static long GetFileSize(string sFullName)        {            long lSize = 0;            if (File.Exists(sFullName))                lSize = new FileInfo(sFullName).Length;            return lSize;        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }


    }


}
