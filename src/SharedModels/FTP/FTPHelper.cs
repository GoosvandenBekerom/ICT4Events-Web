using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SharedModels.Debug;

namespace SharedModels.FTP
{
    public static class FtpHelper
    {
        public static string ServerAddress => Properties.Settings.Default.FTPAddress;
        public static string ServerHardLogin => "ftp://"+ Properties.Settings.Default.FTPUser + ":"+ Properties.Settings.Default.FTPPassword +"@"+ Properties.Settings.Default.FTPAddress;
        private static NetworkCredential DefaultCredentials => new NetworkCredential(Properties.Settings.Default.FTPUser, Properties.Settings.Default.FTPPassword);

        /// <summary>
        /// Attempts to create a new directory at the given path on the FTP server
        /// </summary>
        /// <param name="path">Path of the directory to create</param>
        /// <returns>True if the directory has been created</returns>
        public static bool CreateDirectory(string path)
        {
            try
            {
                var requestDir = (FtpWebRequest)WebRequest.Create(new Uri($"ftp://{ServerAddress}/{path}"));
                requestDir.Credentials = DefaultCredentials;
                requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestDir.UsePassive = true;
                requestDir.UseBinary = true;
                requestDir.KeepAlive = false;
                var response = (FtpWebResponse)requestDir.GetResponse();
                var ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();

                return true;
            }
            catch (WebException ex)
            {
                var response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                    return true;
                }
                Logger.Write(ex.Message);
                response.Close();
                return false;
            }
        }

        /// <summary>
        /// Uploads a file to the FTP server
        /// </summary>
        /// <param name="infilepath">Local filepath of file</param>
        /// <param name="outfilepath">Target filepath on FTP server</param>
        /// <returns></returns>
        public static bool UploadFile(string infilepath, string outfilepath)
        {
            using (var request = new WebClient())
            {
                request.Credentials = DefaultCredentials;
                try
                {
                    request.UploadFileAsync(new Uri($"ftp://{ServerAddress}/{outfilepath}"), "STOR", infilepath);
                    return true;
                }
                catch (WebException ex)
                {
                    Logger.Write(ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Downloads and saves a file locally from the FTP server
        /// </summary>
        /// <param name="infilepath">Location of the file on the server</param>
        /// <param name="outfilepath">Target location on local machine</param>
        /// <returns></returns>
        public static bool DownloadFile(string infilepath, string outfilepath)
        {
            using (var request = new WebClient())
            {
                request.Credentials = DefaultCredentials;
                try
                {
                    request.DownloadFileAsync(new Uri($"ftp://{ServerAddress}/{infilepath}"), outfilepath);
                    return true;
                }
                catch (WebException ex)
                {
                    Logger.Write(ex.Message);
                    return false;
                }
                catch (IOException ex)
                {
                    Logger.Write(ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Downloads a file from the FTP server and stores it in memory
        /// </summary>
        /// <param name="filepath">The filepath of the file on the server</param>
        /// <returns>Binary data of the downloaded file</returns>
        public static byte[] DownloadFile(string filepath)
        {
            try
            {
                var request = (FtpWebRequest) WebRequest.Create(new Uri($"ftp://{ServerAddress}/{filepath}"));
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = DefaultCredentials;

                using (var response = (FtpWebResponse) request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();
                    var buffer = new byte[responseStream.Length + 16];
                    var bytesRead = 0;
                    var bytesToRead = (int) responseStream.Length;

                    do
                    {
                        var readCount = responseStream.Read(buffer, bytesRead, 16);
                        bytesRead += readCount;
                        bytesToRead -= readCount;

                    } while (bytesToRead > 0);

                    return buffer;
                }
            }
            catch (WebException e)
            {
                Logger.Write(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Deletes a file from the FTP server
        /// </summary>
        /// <param name="filepath">Filepath of the file to delete</param>
        /// <returns>True if the file was successfully deleted</returns>
        public static bool DeleteFile(string filepath)
        {
            try
            {
                var request = (FtpWebRequest) WebRequest.Create(new Uri($"ftp://{ServerAddress}/{filepath}"));
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                request.Credentials = DefaultCredentials;
                using (var response = (FtpWebResponse) request.GetResponse())
                {
                    Console.WriteLine(response.StatusDescription);
                    return true;
                }
            }
            catch (WebException e) // Usually this is a file not found error which can safely be ignored
            {
                Logger.Write(e.Message);
                return false;
            }
        }
    }
}
